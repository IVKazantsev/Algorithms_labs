#№6 Посредством выбора
#Список делится на отсортированный и неотсортированный
#В неотсортированном ищется самый маленький и помещается в конец отсортированного

def selectionSort(x):
    for i in range(len(x)):
        min_i = i
        #Выбор наименьшего значения из неотсортированного списка
        for j in range(i + 1, len(x)):
            if x[j] < x[min_i]:
                min_i = j
        #Меняем местами минимальный с последним в отсортированном
        x[min_i], x[i] = x[i], x[min_i]
    return x

x = [9, 15, 1, 24, 6, 3212]
selectionSort(x)
print(x)
