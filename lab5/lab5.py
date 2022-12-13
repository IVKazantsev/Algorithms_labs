def insertSort(x):
    for i in range(1, len(x)):
        insertItem = x[i]
        j = i - 1
        while j >= 0 and x[j] > insertItem:
            x[j + 1] = x[j]
            j -= 1
        x[j + 1] = insertItem

x = [9, 15, 1, 24, 6, 3212]
insertSort(x)
print(x)
