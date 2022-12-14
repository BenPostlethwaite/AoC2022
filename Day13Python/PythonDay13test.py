def GetData(fileName):
    lines = []
    data = []
    with open(fileName, "r") as file:
        for line in file:
            lines.append(line)
    for i in range(len(lines)):
        if lines[i] != "\n":            
            data.append(lines[i].replace("\n", ""))
    return (data)

def GetNums(line):
    data = []
    item = ""
    i = 0
    while i <(len(line)):
        c = line[i]
        if c == "[":
           
            item = GetNums(line[i+1:])
            
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
            if i >= len(line)-1:
                data.append(item)
            
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

def TestRightOrder(line1, line2):
    if type(line1) == int:
        line1 = [line1]
    elif type(line2) == int:
        line2 = [line2]

    smallestLine = min(len(line1),len(line2))
    for i in range(smallestLine):

        if type(line1[i]) == int and type(line2[i]) != int:
            line1[i] = [line1[i]]
            outcome = TestRightOrder(line1[i], line2[i])
            if outcome != None:
                return (outcome)

        elif type(line1[i]) != int and type(line2[i]) == int:
            line2[i] == [line2[i]]
            outcome = TestRightOrder(line1[i], line2[i])
            if outcome != None:
                return (outcome)

        elif type(line1[i]) != int and type(line2[i]) != int:
            outcome = TestRightOrder(line1[i], line2[i])
            if outcome != None:
                return (outcome)
        
        elif line1[i] < line2[i]:
            return (True)
        elif line1[i] > line2[i]:
            return (False)

    if smallestLine == len(line1) and len(line1) != len(line2):
        return (True)
    elif smallestLine == len(line2) and len(line1) != len(line2):
        return (False)
    else:
        return None

    
data = GetData("data.txt")

correctLine = []
lines = [[[2]], [[6]]]
for i in range(len(data)):
    lines.append(GetNums(data[i][1:-1]))

for bubblePass in range(len(lines)-1):
    for i in range(len(lines)-bubblePass-1):
        if (TestRightOrder(lines[i], lines[i+1]) == False):
            lines[i], lines[i+1] = lines[i+1], lines[i]

key = 1
for i in range(len(lines)):
    if str(lines[i]).strip('[').strip(']') == "2" or str(lines[i]).strip('[').strip(']') == "6":
        key *= (i+1)

    #print(lines[i])
    
print(key)
input()