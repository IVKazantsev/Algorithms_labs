# Задача об арифметическом выражении
# 2+7*(3/9)-5=  -0.66666
# 2+7*3/9-5=    -0.66666
# 2+7*(3/9-5)=  -30.6666
# 2+7*(3-9/5)=  10.4
import sys

# Проверка на правильность скобок
def bracketsCheck(s): 
    mas = []
    flag = False
    for i in range(len(s)):
        if s[i] == '(':
            mas.append(i)
            flag = True
        if s[i] == ')': # При закрытии скобок если массив не пуст, то удаляем открытую скобку, иначе выражение неверно
            if mas:
                mas.pop()
            else:
                print("Скобки стоят неверно")
                return 0
    if (len(mas) == 0 and flag):
        return 2
    if (len(mas) == 0 and not flag): # Функция не заходила в if'ы, т.е. в выражении нет скобок
        return 1
    else:
        print("Скобки стоят неверно")
        return 0

# Поиск скобок в выражении и вывод индексов выражения в скобках
def brackets(mas): 
    start = 0
    end = 0
    i = 0
    while mas[i]!=')':
        if mas[i] == '(':
            start = i + 1
        i += 1
    end = i - 1
    return start, end

# Функция вычисления
def calc(mas, first, second):
    res = 0
    if mas[first + 1] == '+':
        res = float(mas[first]) + float(mas[second])
    if mas[first + 1] == '-':
        res = float(mas[first]) - float(mas[second])
    if mas[first + 1] == '*':
        res = float(mas[first]) * float(mas[second])
    if (mas[first + 1] == '/'):
        res = float(mas[first]) / float(mas[second])
    mas[first] = str(res)
    del mas[first+1:second+1] # Удаляем знак и второе число, т.к. результат останется на месте первого числа
    return mas

# Разделяем выражение на элементы из чисел и знаков и добавляем в массив
def createArray(s):
    mas = []
    i = 0
    while i<len(s):
        if (s[i].isdigit()): #Если текущее значение - число, то ищем конец этого числа, затем добавляем в конец массива полное число
            k = 0
            while s[i+k].isdigit():
                k += 1
            else:
                mas.append(s[i:i+k])
                i = i+k
        else: # Если текущий элемент не число, то добавляем его в конец массива
            mas.append(s[i])
            i += 1
    return mas

s = str(input())
mas = createArray(s)
x = bracketsCheck(s)

#Если выражение прошло проверку на скобки
if x:

    # Сначала Вычисляем скобки, чтобы сохранить порядок
    while '(' in mas:
        start, end = brackets(mas) # Индексы начада и конца скобок
        new_end = end # Новый индекс конца, чтобы удалять уже вычисленные выражения
        while new_end != start:
            k = 0 #Коэф., на который следует уменьшать выражение
            # Сначала вычислим * и /, чтобы сохранить порядок
            if (('*' in mas[start:end]) or ('/' in mas[start:end])):
                for i in range(start, end):
                    if i < len(mas):
                        # Производим вычисления. Перехватываем деление на ноль, чтобы не возникло ошибок
                        try:
                            if mas[i] == '*' or mas[i] == '/':
                                mas = calc(mas, i-1, i+1)
                                k += 2 # Прибавляем 2, т.к. нужно избавиться от числа и знака
                        except ZeroDivisionError:
                            print("Деление на ноль")
                            sys.exit(0) # Выход из программы при возникновении ошибки
                    else:
                        break
                new_end = new_end - k
            # Теперь вычислим + и -
            else:
                for i in range(start, new_end):
                    if i < len(mas):
                        if mas[i] == '+' or mas[i] == '-':
                            mas = calc(mas, i-1, i+1)
                            k += 2
                    else:
                        break
                new_end = new_end - k
        del mas[start+1]
        del mas[start-1]


    while len(mas) > 2: # >2, т.к. в массиве всегда остается число и "="
        k = 0
        # Сначала вычислим * и /, чтобы сохранить порядок
        if (('*' in mas) or ('/' in mas)):
            for i in range(len(mas)):
                if i < len(mas):
                    try:
                        if mas[i] == '*' or mas[i] == '/':
                            mas = calc(mas, i - 1, i + 1)
                            k += 1
                    except ZeroDivisionError:
                        print("Деление на ноль")
                        sys.exit(0)
                else:
                    break
        # Теперь вычислим + и -
        else:
            for i in range(len(mas)):
                if i < len(mas):
                    if mas[i] == '-' or mas[i] == '+':
                        mas = calc(mas, i - 1, i + 1)
                        k += 1
                else:
                    break
if len(mas) == 2:
    print('Ответ:', mas[0])
