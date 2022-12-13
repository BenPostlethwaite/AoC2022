def GetData(fileName):
    lines = []
    data = []
    with open(fileName, "r") as file:
        for line in file:
            lines.append(line)
    for i in range(0,len(lines)-1,3):

        pair = [lines[i].replace("\n", ""),lines[i+1].replace("\n","")]
        data.append(pair)
    return (data)

def GetNums(line):
    data = []
    item = ""
    i = 0
    while i <(len(line)):
        c = line[i]
        if c == "[":
           
            item = GetNums(line[i+1:-1])
            
            indents = 1
            for index in range(i+1,len(line)):
                testFor = line[index]
                if testFor == ']':
                    indents -= 1
                if testFor == '[':
                    indents += 1
                if indents == 0:
                    break
            i = index
            
        elif c == ']':
            if item != "":
                if type(item) == str:
                    item = int(item)
                data.append(item)
            return(data)
        elif c == ',':
            if type(item) == str:
                item = int(item)

            data.append(item)
            item = ''
        else:
            item+=c
            if i == len(line)-1:
                data.append(int(item))
        i+=1
    """
    if item != "":
        if type(item) == str:
            item = int(item)
        data.append(item)"""
    return(data)


data = GetData("test.txt")
for pair in data:
    line1string = pair[0][1:-1]
    line2string = pair[1][1:-1]
    line1 = GetNums(line1string)
    line2 = GetNums(line2string)
    print(line1)
    print(line2)
    print("");



input()
