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
        else { std::cout << "incorrect data!" << std::endl; incorr = 1;  } // Некорректный ввод
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

void CLEAR(Elem*& v) {
    if (v == nullptr) return;
    CLEAR(v->left); // Рекурсивно удаляем все элементы
    CLEAR(v->right);
    delete v;
    v = nullptr;
}

void PRE(Elem* v) {
    if (v == nullptr)
        return;
    std::cout << v->data << std::endl;
    PRE(v->left);
    PRE(v->right);
}

void IN(Elem* v) {
    if (v == nullptr)
        return;
    IN(v->left);
    std::cout << v->data << std::endl;
    IN(v->right);
}

void POST(Elem* v) {
    if (v == nullptr)
        return;
    POST(v->left);
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

    //8(3(1,6(4,7)),10(,14(13,)))
    //1(2(,3(4,5)),6(7(,8(,9)),10(11,)))
    return 0;
}