# Поразрядная сортировка
# Сначала числа сортируются по первой цифре, затем по след. разряду и т.д.
# Линейная сложность O(n)

def radixSort(x):
    n = len(x) # Задаем длину массива, т.к. часто будем к ней обращаться

    # Преобразуем массив из чисел в строки
    for i in range(n):
        x[i] = str(x[i])

    # Находим максимальную длину числа, чтобы приравнять длины остальных
    maxLength = 0
    for i in range(n):
        if len(x[i]) > maxLength:
            maxLength = len(x[i])

    # Добавляем незначящие нули там, где это необходимо
    for i in range(n):
        if maxLength > len(x[i]):
            while len(x[i]) != maxLength:
                x[i] = '0' + x[i]

    sortArray = [[] for i in range(10)]  # Задаем пустой массив из 10 массивов (кол-ва цифр) для сортировки
    k = maxLength # Текущий разряд
    while k != 0:
        # Сортировка
        for i in range(len(x)):
            value = int(x[i][k-1])  # Текущая цифра в разряде
            sortArray[value].append(x[i])  # Записываем число в сорт массив c номером, равным текущему значению разряда(К примеру, на 9 месте будет 0009, на 5 - 0015 и т.д.)
        print('sortArray = ', sortArray)

        # Чистим наш массив, чтобы вставить в него отсортированные значения, а затем чистим сортировочный массив
        x.clear() #Чистим наш массив со строками, чтобы засунуть числа
        for i in range(len(sortArray)):
            for j in range(len(sortArray[i])):
                x.append(sortArray[i][j])
        for i in range(len(sortArray)):
            sortArray[i].clear()
        k -= 1
        #print('x = ', x)

    # Преобразуем из строк в числа
    for i in range(len(x)):
        x[i] = int(x[i])
    return x
    
x = [9, 1000, 15, 1, 24, 6, 3212]
radixSort(x)
print(x)
