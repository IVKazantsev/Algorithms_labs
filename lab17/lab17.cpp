#include <iostream>
#include <string>
#include <vector>

struct Elem { // Класс элементов дерева
    int data; // Содержание
    Elem* left; // Правый потомок
    Elem* right; // Левый потомок
    Elem* parent; // Родитель
};

Elem* MAKE(int data, Elem* p) {  // Инициализация вершины дерева
    Elem* q = new Elem; // Создаем объект
    q->data = data; // Присваеваем значение
    q->left = nullptr; // Нулевые потомки
    q->right = nullptr;
    q->parent = p; // Родитель, к-го мы указали
    return q;
}

// Для корректной работы меню
void ADD(int data, Elem*& root) { // Добавление элемента по правилу бинарного дерева поиска (идем влево от корня, если элемент меньше и вправо, если больше)
    Elem* v = root; // Задаём корень
    while ((data < v->data && v->left != nullptr) || (data > v->data && v->right != nullptr)) // Цикл прохода к нужному месту в зависимости от величины числа
        if (data < v->data) v = v->left; 
        else v = v->right;
    if (data == v->data) return; // Выходим из функции, если такой элемент уже существует
    Elem* u = MAKE(data, v); // Создаем элемент со своими значением, родителем и нулевыми потомками
    if (data < v->data) v->left = u; // Задаём вершину слева для родителя, если значение меньше 
    else v->right = u; // И справа, если больше
}

void ADD_ONE(int data, Elem*& root, bool left) {  // Функция добавления элемента по линейно-скобочной записи (сами указываем, с какой стороны ставить элемент)
    Elem* v = root; // 
    Elem* u = MAKE(data, v);
    if (left) v->left = u;
    else v->right = u;
}

void ADD_ALL(std::string& str, int& i, Elem*& root) { // Функция заполнения всего дерева (такая же, как и в 15, 16 лабе)
    int value = 0, incorr = 0;
    if (root == nullptr) {
        if ((str[i] >= '0') && (str[i] <= '9')) {  //Пока текущий элемент - число
            value = value * 10 + str[i] - '0'; i++; // Из строки в число (Увеличиваем на разряд + число - 0 по аски), делаем i++
        }
        else { std::cout << "incorrect data!" << std::endl; incorr = 1; } // Некорректный ввод
        root = MAKE(value, nullptr); // Создаем корень со значение 
    }
    while (str[i] != '\0') // Пока не конец строки
        switch (str[i]) {
        case '(': {
            i++; value = 0;
            while ((str[i] >= '0') && (str[i] <= '9')) { // Обрабатываем число
                value = value * 10 + str[i] - '0'; i++;
            }
            if (value != 0) { // Если попалось число
                ADD_ONE(value, root, true); // Добавляем левое число в дерево
                if (str[i] == '(') ADD_ALL(str, i, root->left);
            }
            break;
        }
        case ',': {
            i++; value = 0;
            while ((str[i] >= '0') && (str[i] <= '9')) { // Обрабатываем число
                value = value * 10 + str[i] - '0'; i++;
            }
            if (value != 0) {
                ADD_ONE(value, root, false); // Добавляем правое число в дерево
                if (str[i] == '(') ADD_ALL(str, i, root->right);
            }
            break;
        }
        case ')': { i++; return; }
        default: { if (incorr == 0) std::cout << "incorrect data!" << std::endl; return; } // Некорректный ввод
        }
}

// Функция поиска элемента по бинарному дереву поиска
Elem* SEARCH(int data, Elem* v) { // v - элемент, от которого начинаем поиск
    if (v == nullptr) { std::cout << "Element is not found" << std::endl; return v; } //  Если текущего элемента нет, то выходим с ненайденным числом
    if (v->data == data) { std::cout << "Element is found" << std::endl; return v; } //  Если значение текущего элемента совпала с нужным нам, то выходим с найденным число
    if (data < v->data) return SEARCH(data, v->left); // Рекурсивно идем влево, если нужное нам значение меньше текущего
    else return SEARCH(data, v->right); // Также для правого
}

void DELETE(int data, Elem*& root) { // Функция удаления вершины из дерева
    Elem* u = SEARCH(data, root); // Ищем элемент, который нужно удалить
    if (u == nullptr) return; // Выходим, если его нет в дереве
    if (u->left == nullptr && u->right == nullptr && u == root) { // Случай, когда в дереве только корень
        delete root; // Просто удаляем корень
        root = nullptr; // На его место ставим nullptr
        return;
    }
    if (u->left == nullptr && u->right != nullptr && u == root) { // Если присутствует правый потомок, то 
        Elem* t = u->right;
        while (t->left != nullptr) t = t->left; // Идем до конца влево от правого потомка и присваеваем удаляемому объекту его значение, так как его значение может заменить удалённое
        u->data = t->data;
        u = t;
    }
    if (u->left != nullptr && u->right == nullptr && u == root) { // Если присутствует левый потомок, то
        Elem* t = u->left;
        while (t->right != nullptr) t = t->right; // Идем до конца вправо от левого потомка и присваеваем удаляемому объекту его значение, так как его значение может заменить удалённое
        u->data = t->data;
        u = t;
    }
    if (u->left != nullptr && u->right != nullptr) { // Если оба потомка присутствуют, то
        Elem* t = u->right;
        while (t->left != nullptr) t = t->left; // Идем до конца влево от правого потомка, присваеваем удаляемому объекту его значение, и переносим u на его место
        u->data = t->data;
        u = t;
    }
    Elem* t;
    // Удаление оригинала элемента с места, откуда был дублирован элемент, который нужно было удалить
    if (u->left == nullptr) t = u->right; // Если слева от текущего элемента пусто, то присваеваем временной переменной правое значение
    else t = u->left; // И наоборот
    if (u->parent->left == u) u->parent->left = t; // Если текущий элемент стоит слева, то присваиваем значение потомка, который точно равен nullptr
    else u->parent->right = t; // Переносим в u значение t
    if (t != nullptr) t->parent = u->parent; // Если t - не ноль, то присваеваем u значение своего потомка
    delete u; // Удаляем
    std::cout << "and deleted" << std::endl; // Выводим то, что элемент был удалён для корректной работы меню ("and", так как перед этим будет напечатано то, что элемент был найден)
}

void CLEAR(Elem*& v) { // Полная очистка дерева до элемента v
    if (v == nullptr) return;
    CLEAR(v->left);
    CLEAR(v->right);
    delete v;
    v = nullptr;
}

void PRINT(Elem* v) { // Вывод в линейно-скобочной записи
    if (v == nullptr) return;
    std::cout << v->data;
    if ((v->left != nullptr) || (v->right != nullptr)) { // Если есть потомки, то выводим '(', а затем смотрим, какой именно потомок присутствует
        std::cout << "(";
        if (v->left != nullptr) PRINT(v->left);
        std::cout << ",";
        if (v->right != nullptr) PRINT(v->right);
        std::cout << ")";
    }
}

void MENU(Elem* root) { // Функция меню
    std::cout << "Menu" << std::endl;

    int operation;
    while (true) { // Цикл, который прервется только при входе в операцию Exit
        std::cout << "Select operation:" << std::endl; // Печатаем меню и в зависимости от введенного числа совершаем определенные функции
        std::cout << "Add(1) Delete(2) Search(3) Exit(4)" << std::endl;
        std::cin >> operation;
        switch (operation) {
        case 1: {
            int value;
            std::cout << "What value to add: ";
            std::cin >> value;
            ADD(value, root); // Если нужно добавить элемент, то используем функцию ADD по правилу бинарного дерева поиска, чтоюы он встал на нужное место
            break;
        }
        case 2: {
            int value;
            std::cout << "What value to delete: ";
            std::cin >> value;
            DELETE(value, root); // Удаление нужной вершины дерева
            break;
        }
        case 3: {
            int value;
            std::cout << "What value to search: ";
            std::cin >> value;
            Elem* e = nullptr;
            e = SEARCH(value, root); // Поиск по дереву 
            break;
        }
        case 4: return; // Если пользователь ввел Exit, то выходим из цикла
        default: break;
        }
    }
}

int main() {
    std::string str;
    std::cout << "Bin tree: ";
    std::cin >> str;

    int value = 0, i = 0;
    Elem* root = nullptr;
    ADD_ALL(str, i, root); // Заполняем дерево

    MENU(root); // Выводим меню
    std::cout << "Bin tree after operations: "; 
    PRINT(root); // И печатаем дерево после всех соверщённых над ним операций
    CLEAR(root); // Очищаем дерево

    // Примеры ввода
    //8(3(1,6(4,7)),10(,14(13,)))
    //6(1(,4(3,5)),7(,9(8,12)))
    return 0;
}
