#№9 Пирамидальная
#Делится на остортированный и неотсортированный
#Неотсортированный делает кучей, чтобы выделить больший элемент
#Создаем дерево с наибольшим элементом в вершине, помещаем его в отсортированный

#Вспомогательная функция дерева
def pyramid(x, pyramidSize, i):
    #Индекс наибольшего элемента считаем верхним индексом
    largI = i
    leftI = 2*i+1
    rightI = 2*i+2
    # Если индекс нижнего элемента слева допустимый, а элемент больше,
    # чем текущий наибольший, обновляем наибольший элемент
    if leftI < pyramidSize and x[leftI] > x[largI]:
        largI = leftI
    #Правый элемент
    if rightI < pyramidSize and x[rightI] > x[largI]:
        largI = rightI
    #Если наибольший элемент больше не верхний, меняем местами
    if largI != i:
        x[i], x[largI] = x[largI], x[i]
        #pyramid от нового верхнего элемента, чтобы убедиться, что он наибольший
        pyramid(x, pyramidSize, largI)
    return x

def pyramidSort(x):
    #Создаём дерево
    for i in range(len(x), -1, -1):
        pyramid(x, len(x), i)
    #Перемещаем вершину дерева в конец списка
    for i in range(len(x) - 1, 0, -1):
        x[i], x[0] = x[0], x[i]
        pyramid(x, i, 0)
    return x
        
x = [9, 15, 1, 24,3212, 6]
pyramidSort(x)
print(x)

