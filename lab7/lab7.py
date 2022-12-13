#№7 Шелла
#Элемент сравнивается с другим на определенном интервале, который уменьшается
#Интервал делится на два

def shellSort(x):
    gap = len(x) // 2
    while gap > 0:
        for i in range(gap, len(x)):
            curValue = x[i] #текущее значение
            curI = i #текущий индекс

            #Пока индекс больше значения интервала и
            #сравниваемый элемент больше текущего
            while curI >= gap and x[curI - gap] > curValue:
                #Меняем местами
                x[curI] = x[curI - gap]
                curI -= gap
                x[curI] = curValue

        gap //= 2
    return x

x = [9, 15, 1, 24, 6, 3212]
shellSort(x)
print(x)
