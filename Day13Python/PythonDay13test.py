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
    numString = ""
    for i in range(len(line)):
        c = line[i]
        if c == "[":
            GetNums(line[i+1:-1])
            
        elif c == ']':
            return(data)
        elif c == ',':
            data.append(int(numString))
            numString = ''
        else:
            numString+=c
            if i == len(data):
                data.append(int(numString))
    return(data)




    


data = GetData("test.txt")
for pair in data:
    line1string = pair[0]
    line2string = pair[1]
    line1 = GetNums(line1string) 
    #print(f"{line1} {line2}")    
            
#https://www.tutorialspoint.com/convert-a-string-representation-of-list-into-list-in-python





input()
