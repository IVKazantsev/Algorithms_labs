def hashfunc(string):  # Хэш-функция
    key = 0 # Определяем ключ
    for i in range(len(string)):
        if string[i] != '\n':
            key += ord(string[i]) # ключ будет состоять из суммы всех символов из строки по аски, деленной на 37 целочисленно
    return key // 37


class Elem:  # Класс элементов (будут состоять из ключа и значения)
    def __init__(self, key, value):  # Конструктор
        self.key = key
        self.value = value


f = open('input.txt')  # Открываем файл чтения
p = list(f.read())  # Преобразуем в список
i = 0  # Итератор для while
q = Elem(0, '')  # Создаем элемент класса Elem
hashTable = []  # Задаем хэш-таблицу
while i < len(p):  # Сама хэш-таблица
    if i == len(p) - 1: # Обработка последнего слова. Проделываем то же самое, что и в предыдущих
        q = Elem(hashfunc(p[:(i + 1)]), ''.join(p[:(i + 1)]))  # В качестве ключа даем hashfunc, значение - срез строки до текущего элемента, чтобы исключить другие слова
        hashTable += [0] * (q.key + 1 - len(hashTable)) # Увеличиваем длину массива хэш-таблицы до значения текущего ключа, чтобы хватило места
        if hashTable[q.key] == 0: # Записываем на место ключа элемент, если там пусто
            hashTable[q.key] = q
        else:  # Обработка коллизий (записываем на следующее свободное место)
            j = 0
            while hashTable[q.key + j] != 0:
                j += 1
            if hashTable[q.key + j] == 0:
                hashTable[q.key + j] = q
        break
    elif p[i] == '\n':  # Обработка всех слов, кроме последнего. Делаем то же самое, что и для последней строки за исключением новых закомментированных строк кода
        q = Elem(hashfunc(p[:i]), ''.join(p[:i]))
        p = p[(i + 1):]  # Обрезаем массив на записанное слово
        i = 0  # Зануляем итератор, чтобы считать длину текущей строки (так как делаем срез, длина массива уменьшается)
        hashTable += [0] * (q.key + 1 - len(hashTable))
        if hashTable[q.key] == 0:
            hashTable[q.key] = q
        else:
            j = 0
            while hashTable[q.key + j] != 0:
                j += 1
            if hashTable[q.key + j] == 0:
                hashTable[q.key + j] = q
    i += 1  # Увеличиваем итератор для следующего цикла while
p.clear() # Очищаем массив
f.close() # И закрываем файл

g = open('output.txt', 'w') # Открываем файл, в который будем записывать хэш-таблицу
g.write('key\tvalue\n') # Заголовок таблицы
for i in range(len(hashTable)): # Проходим по длине хэш-таблицы и записываем через табуляцию ключ и значение
    if hashTable[i] != 0:
        g.write(str(hashTable[i].key) + '\t' + hashTable[i].value + '\n')
g.close() # Закрываем файл записи
