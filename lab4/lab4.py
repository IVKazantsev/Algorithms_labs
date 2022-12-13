#№4 Прочёсыванием
#Берутся первый и последний элементы "расчёски".
#Уменьшаем размеры "расчёски"   

def combSort(x):
    #length - Длина расчески
    length = len(x)
    #swap - произошла ли сортировка для данных элементов
    swap = True
    while length > 1 or swap:
        #1.247 - оптимальное значение фактора уменьшения для сортировки прочёсыванием
        length = max(1, int(length / 1.247))
        swap = False
        #Цикл сортировки элементов на концах расчески
        for i in range(len(x) - length):
            j = i + length
            if x[i] > x[j]:
                x[i], x[j] = x[j], x[i]
                swap = True
    return x

x = [9, 15, 1, 24, 6, 3212]
combSort(x)
print(x)
