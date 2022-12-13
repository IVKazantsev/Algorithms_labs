# №10 Слиянием ("Разделяй и властвуй")
# Массив делится примерно на 2 равные части
# Каждый из подмассивов делится на равные части, пока не останется 1 элемент в каждом
# 1-элементные объединяются в 2-элементные и сортируются
# объединяются в 4-элементные и тд.
# Сложность O(nlog(n))

def merge(left, right):
    sortedMas = []
    leftI = rightI = 0

    # Длины подмассивов
    leftLength, rightLength = len(left), len(right)

    for i in range(leftLength + rightLength):
        if leftI < leftLength and rightI < rightLength: #Если мы находимся в подмассиве
            # Сравниваем первые элементы в начале каждого из списков
            # Добавляем меньший элемент среди первых элементов подмассивов в отсортированный массив
            if left[leftI] <= right[rightI]:
                sortedMas.append(left[leftI])
                leftI += 1
            else:
                sortedMas.append(right[rightI])
                rightI += 1

        # Если достигнут конец какого-либо из подмассивов, то элементы этого подмассива добавляем в отсортированный
        elif leftI == leftLength:
            sortedMas.append(right[rightI])
            rightI += 1
        elif rightI == rightLength:
            sortedMas.append(left[leftI])
            leftI += 1
            
    return sortedMas

def mergeSort(x):
    # Возвращаем, если один элемент
    if len(x) <= 1:
        return x

    # Ищем середину массива
    mid = len(x) // 2

    # Сортируем и объединяем подмассивы
    left = mergeSort(x[:mid]) #Срез массива с 0 до mid
    right = mergeSort(x[mid:]) #Срез массива с mid до -1

    # Объединяем отсортированные подмассивы
    return merge(left, right)


x = [9, 15, 1, 24, 3212, 6]
print(mergeSort(x))
