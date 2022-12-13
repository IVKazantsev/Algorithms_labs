def hashfunc(string):  # Хэш-функция
    key = 0
    for i in range(len(string)):
        if string[i] != '\n':
            key += ord(string[i])
    return key // 37


class Elem:  # Класс элементов
    def __init__(self, key, value):  # Конструктор
        self.key = key
        self.value = value


f = open('input.txt')  # Открываем файл чтения
p = list(f.read())  # Преобразуем в список
i = 0  # Итератор для while
q = Elem(0, '')  # Создаем элемент класса Elem
hashTable = []  # Задаем хэш-таблицу
while i < len(p):  # Сама хэш-таблица
    if p[i] == '\n' and q.value == "":  # Обработка первой строки
        q = Elem(hashfunc(p[:i]), ''.join(p[:i]))  # В качестве ключа даем hashfunc, значение - срез строк
        p = p[(i + 1):]  # Обрезаем массив
        i = 0  # Зануляем итератор, чтобы считать длину текущей строки
        hashTable += [0] * (q.key + 1 - len(hashTable))  # Увеличиваем длину массива до значения текущего ключа
        if hashTable[q.key] == 0:  # Записываем на место ключа элемент, если там пусто
            hashTable[q.key] = q
        else:  # Обработка коллизий (записываем на следующее свободное место)
            j = 0
            while hashTable[q.key + j] != 0:
                j += 1
            if hashTable[q.key + j] == 0:
                hashTable[q.key + j] = q
    elif p[i] == '\n':
        q = Elem(hashfunc(p[:(i)]), ''.join(p[:(i)]))  # В качестве ключа даем hashfunc, значение - срез строки
        p = p[(i + 1):]  # Обрезаем массив
        i = 0  # Зануляем итератор, чтобы считать длину текущей строки
        hashTable += [0] * (q.key + 1 - len(hashTable))
        if hashTable[q.key] == 0:
            hashTable[q.key] = q
        else:  # Обработка коллизий (записываем на следующее свободное место)
            j = 0
            while hashTable[q.key + j] != 0:
                j += 1
            if hashTable[q.key + j] == 0:
                hashTable[q.key + j] = q
    elif i == len(p) - 1:
        q = Elem(hashfunc(p[:(i + 1)]), ''.join(p[:(i + 1)]))  # В качестве ключа даем hashfunc, значение - срез строки
        hashTable += [0] * (q.key + 1 - len(hashTable))
        if hashTable[q.key] == 0:
            hashTable[q.key] = q
        else:  # Обработка коллизий (записываем на следующее свободное место)
            j = 0
            while hashTable[q.key + j] != 0:
                j += 1
            if hashTable[q.key + j] == 0:
                hashTable[q.key + j] = q
        break
    i += 1  # Увеличиваем итератор для следующего цикла while

print(hashTable)

p.clear()
f.close()

g = open('output.txt', 'w')
g.write('key\tvalue\n')
for i in range(len(hashTable)):
    if hashTable[i] != 0:
        g.write(str(hashTable[i].key) + '\t' + hashTable[i].value + '\n')

g.close()
