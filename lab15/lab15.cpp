#include <iostream>
#include <string>

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

void ADD_ONE(int data, Elem*& root, bool left) {  // Функция добавления элемента
    Elem* v = root; // Элементу v присваиваем корень
    Elem* u = MAKE(data, v); // Создаем элемент с текущим значением и корнем v
    if (left) v->left = u; // Записываем влево, если удовлетворяет нашей булевой переменной left
    else v->right = u; // Иначе вправо
}

void ADD_ALL(std::string& str, int& i, Elem*& root) { // Функция заполнения всего дерева
    int value = 0, incorr = 0; // Переменные значения и проверки на некорректный ввод (нужна, чтобы не выводить строку дважды)
    if (root == nullptr) { // Обработка корня
        if ((str[i] >= '0') && (str[i] <= '9')) {  // Если текущий элемент - число
            value = value * 10 + str[i] - '0'; i++; // Из строки в число (Увеличиваем на разряд + число - 0 по аски), делаем i++
        }
        else { std::cout << "incorrect data!" << std::endl; incorr = 1;  } // Если не число, то некорректный ввод
        root = MAKE(value, nullptr); // Создаем корень со значением
    }
    while (str[i] != '\0') // Пока не конец строки
        switch (str[i]) { // Обрабатываем различные символы '(', ')', ','
        case '(': { // Если открывающая скобка, то увеличиваем итератор и смотрим, если следующий символ - цифра, то обрабатываем и увеличиваем итератор, чтобы обработать полностью число 
            i++; value = 0;
            while ((str[i] >= '0') && (str[i] <= '9')) { // Пока текущий элемент - число
                value = value * 10 + str[i] - '0'; i++; // Из строки в число (Увеличиваем на разряд + число - 0 по аски), делаем i++
            }
            if (value != 0) { // Если попалось число
                ADD_ONE(value, root, true); // Добавляем левое число в дерево, т.к. после '(' идет левое число
                if (str[i] == '(') ADD_ALL(str, i, root->left); // Если потом снова попалась '(', то делаем рекурсию
            }
            break;
        }
        case ',': { // Если встретилась запятая, то делаем то же самое, что и в предыдущем кейсе, но записываем число в правого потомка
            i++; value = 0;
            while ((str[i] >= '0') && (str[i] <= '9')) {
                value = value * 10 + str[i] - '0'; i++;
            }
            if (value != 0) {
                ADD_ONE(value, root, false);
                if (str[i] == '(') ADD_ALL(str, i, root->right);
            }
            break;
        }
        case ')': { i++; return; } // При закрывающей скобке просто идем на следующий шаг
        default: { if (incorr == 0) std::cout << "incorrect data!" << std::endl; return; } // Некорректный ввод
        }
}

void CLEAR(Elem*& v) { // Удаление дерева полностью до элемента v
    if (v == nullptr) return;
    CLEAR(v->left); // Рекурсивно удаляем все элементы
    CLEAR(v->right);
    delete v;
    v = nullptr;
}

void PRE(Elem* v) { // Прямой обход
    if (v == nullptr)
        return;
    std::cout << v->data << std::endl; // Сначала записываем корень, всех левых потомков, затем правых
    PRE(v->left);
    PRE(v->right);
}

void IN(Elem* v) { // Центральный обход
    if (v == nullptr)
        return;
    IN(v->left); // Сначала записываем всех левых потомков, корень, затем правых потомков
    std::cout << v->data << std::endl;
    IN(v->right);
}

void POST(Elem* v) { // Концевой обход
    if (v == nullptr)
        return;
    POST(v->left); // Сначала записываем всех левых потомков, правых, затем корень
    POST(v->right);
    std::cout << v->data << std::endl;
}

int main() {
    std::string str;
    std::cout << "Bin tree: ";
    std::cin >> str;

    int value = 0, i = 0;
    Elem* root = nullptr;
    ADD_ALL(str, i, root); // Заполняем дерево

    //Обходы
    if ((root->left != 0) || (root->right != 0)) {
        std::cout << "Прямой обход:" << std::endl;
        PRE(root);
        std::cout << "Центральный обход:" << std::endl;
        IN(root);
        std::cout << "Концевой обход:" << std::endl;
        POST(root);
    }
    CLEAR(root); // Очистка динамической памяти

    // Примеры ввода
    //8(3(1,6(4,7)),10(,14(13,)))
    //1(2(,3(4,5)),6(7(,8(,9)),10(11,)))
    return 0;
}
