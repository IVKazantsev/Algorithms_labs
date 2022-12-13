inp = open("input.txt")
size = 0  # Исходный размер
gap = 1  # Шаг деления файла
count = 0  # Число перенесенных чисел в файл
countA = 0
countB = 0
flag = True  # Флаг для чередования, в какой файл заливать числа
flagA = True
flagB = True

while inp.readline():  # Считываем размер файла
    size += 1
inp.close()

while gap < size:  # Разбиваем файлы на два подфайла
    out = open("input.txt")  # Открываем файлы в цикле, т.к. out.readline() происходит единожды за открытие файла
    out1 = open("A.txt", 'w')
    out2 = open("B.txt", 'w')
    count = 0
    for i in range(size):
        line = out.readline()
        count += 1
        if flag:
            out1.write(line)
        else:
            out2.write(line)
        if count == gap:
            count = 0
            flag = not flag
    out.close()
    out1.close()
    out2.close()

    inp1 = open("A.txt")
    inp2 = open("B.txt")
    out = open("input.txt", 'w')  # Меняем их местами, чтобы перезаписать

    # Пропустил if'ы
    i = 0
    while i < size:  # Проходим по циклу и записываем в файл в зависимости от того, какой у нас шаг
        countA = 0  # Количество записанных в файл вывода чисел из файла inp1
        countB = 0  # Количество записанных в файл вывода чисел из файла inp2
        a = inp1.readline()
        b = inp2.readline()
        if a.find('\n') <0:
            a+='\n'
        if b.find('\n') <0:
            b+='\n'
        while countA < gap and flagA and countB < gap and flagB:
            if b in ['', '\n', '"'] or a in ['', '\n', '"']:
                break
            if int(a) < int(b):
                out.write(str(a))  # Проблема в том, что записывается не следующее число, а это же самое
                if countA + 1 < gap:
                    a = inp1.readline()
                    if a.find('\n') < 0:
                        a += '\n'
                if a:
                    flagA = True
                else:
                    flagA = False
                countA += 1
            else:
                out.write(str(b))
                if countB + 1 < gap:
                    b = inp2.readline()
                    if b.find('\n') < 0:
                        b += '\n'
                if b:
                    flagB = True
                else:
                    flagB = False
                countB += 1  # countB += 1

        while countA < gap and flagA:
            if a in ['', '\n']:
                break
            out.write(str(a))
            if countA+1<gap:
                a = inp1.readline()
                if a.find('\n') < 0:
                    a += '\n'
            if a:
                flagA = True
            else:
                flagA = False
            countA += 1  # countA += 1

        while countB < gap and flagB:
            if b in ['', '\n']:
                break
            out.write(str(b))
            if countB+1<gap:
                b = inp2.readline()
                if b.find('\n') < 0:
                    b += '\n'
            if b:
                flagB = True
            else:
                flagB = False
            countB += 1
        i += 2*gap
    gap *= 2
    inp1.close()
    inp2.close()
    out.close()
