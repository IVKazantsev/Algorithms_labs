a = 1
x = int(input("Введите x: "))
while (a <= x):
    b = a
    while (b <= x):
        c = b
        while (c <= x):
            print(c)
            c*=7
        b*=5
    a*=3

