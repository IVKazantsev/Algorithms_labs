str = input("Введите строку: ")
stack = []
for i in range(len(str)):
    if (i != 0 and len(stack) > 0):
        if(stack[len(stack)-1] == "(" and str[i] == ")" or stack[len(stack)-1] == "[" and str[i] == "]" or stack[len(stack)-1] == "{" and str[i] == "}"):
            stack.pop()
        else:
            stack.append(str[i])
    else:
        stack.append(str[i])

if(len(stack) == 0):
    print("Строка существует")
else:
    print("Строка не существует")
input()
