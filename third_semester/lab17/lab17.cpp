#include <iostream>
#include <string>
#include <vector>

struct Elem { // Класс элементов дерева
    int data; // Содержание
    Elem* left; // Правый потомок
    Elem* right; // Левый потомок
    Elem* parent; // Родитель
};

Elem* MAKE(int data, Elem* p) {  // Инициализация вершины
    Elem* q = new Elem; // Создаем объект
    q->data = data; // Присваеваем значение
    q->left = nullptr; // Нулевые потомки
    q->right = nullptr;
    q->parent = p; // Родитель, к-го мы указали
    return q;
}

void ADD(int data, Elem*& root) { // Добавление элемента
    Elem* v = root;
    while ((data < v->data && v->left != nullptr) || (data > v->data && v->right != nullptr))
        if (data < v->data) v = v->left;
        else v = v->right;
    if (data == v->data) return;
    Elem* u = MAKE(data, v);
    if (data < v->data) v->left = u;
    else v->right = u;
}

void ADD_ONE(int data, Elem*& root, bool left) {  // Функция добавления элемента
    Elem* v = root;
    Elem* u = MAKE(data, v);
    if (left) v->left = u;
    else v->right = u;
}

void ADD_ALL(std::string& str, int& i, Elem*& root) { // Функция заполнения всего дерева
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

Elem* SEARCH(int data, Elem* v) { // v - элемент, от которого начинаем поиск
    if (v == nullptr) { std::cout << "Element is not found" << std::endl; return v; }
    if (v->data == data) { std::cout << "Element is found" << std::endl; return v; }
    if (data < v->data) return SEARCH(data, v->left);
    else return SEARCH(data, v->right);
}

void DELETE(int data, Elem*& root) {
    Elem* u = SEARCH(data, root);
    if (u == nullptr) return;
    if (u->left == nullptr && u->right == nullptr && u == root) { // Случай, когда в дереве только корень
        delete root;
        root = nullptr;
        return;
    }
    if (u->left == nullptr && u->right != nullptr && u == root) { // Если присутствует правый потомок, то 
        Elem* t = u->right;
        while (t->left != nullptr) t = t->left; // Идем до конца влево от правого потомка и присваеваем удаляемому объекту его значение
        u->data = t->data;
        u = t;
    }
    if (u->left != nullptr && u->right == nullptr && u == root) { // Если присутствует левый потомок, то
        Elem* t = u->left;
        while (t->right != nullptr) t = t->right; // Идем до конца вправо от левого потомка и присваеваем удаляемому объекту его значение
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
    // Удаление оригинала элемента с места, откуда был дублирован удаленный элемент
    if (u->left == nullptr) t = u->right; // Если слева от текущего элемента пусто, то присваеваем временной переменной правое значение
    else t = u->left; // И наоборот
    if (u->parent->left == u) u->parent->left = t; // Если текущий элемент стоит слева, то присваиваем значение потомка, который точно равен nullptr
    else u->parent->right = t; // Переносим в u значение t
    if (t != nullptr) t->parent = u->parent; // Если t - не ноль, то присваеваем u значение своего потомка
    delete u; // Удаляем
    std::cout << "and deleted" << std::endl;
}

void CLEAR(Elem*& v) { // Полная очистка дерева
    if (v == nullptr) return;
    CLEAR(v->left);
    CLEAR(v->right);
    delete v;
    v = nullptr;
}

void PRINT(Elem* v) { // Вывод в линейно-скобочной записи
    if (v == nullptr) return;
    std::cout << v->data;
    if ((v->left != nullptr) || (v->right != nullptr)) {
        std::cout << "(";
        if (v->left != nullptr) PRINT(v->left);
        std::cout << ",";
        if (v->right != nullptr) PRINT(v->right);
        std::cout << ")";
    }
}

void MENU(Elem* root) {
    std::cout << "Menu" << std::endl;

    int operation;
    while (true) {
        std::cout << "Select operation:" << std::endl;
        std::cout << "Add(1) Delete(2) Search(3) Exit(4)" << std::endl;
        std::cin >> operation;
        switch (operation) {
        case 1: {
            int value;
            std::cout << "What value to add: ";
            std::cin >> value;
            ADD(value, root);
            break;
        }
        case 2: {
            int value;
            std::cout << "What value to delete: ";
            std::cin >> value;
            DELETE(value, root);
            break;
        }
        case 3: {
            int value;
            std::cout << "What value to search: ";
            std::cin >> value;
            Elem* e = nullptr;
            e = SEARCH(value, root);
            break;
        }
        case 4: return;
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

    MENU(root);
    std::cout << "Bin tree after operations: "; 
    PRINT(root);
    CLEAR(root);

    //8(3(1,6(4,7)),10(,14(13,)))
    //6(1(,4(3,5)),7(,9(8,12)))
    return 0;
}