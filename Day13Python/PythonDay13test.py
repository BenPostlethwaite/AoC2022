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

data = GetData("test.txt")
for pair in data:
    line1string = pair[0]
    line2string = pair[1]
    line1 = line1string.strip('').split(',')
    line2 = line2string.strip('').split(',')
        
    print(f"{line1} {line2}")    
            
#https://www.tutorialspoint.com/convert-a-string-representation-of-list-into-list-in-python





input()
