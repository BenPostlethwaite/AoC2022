linesWithSpaces = []
lines = []
data = []
with open("test.txt", "r") as file:
    for line in file:            
        linesWithSpaces.append(line)
for i in range(0,len(linesWithSpaces)-1,2):
    lines.append(linesWithSpaces[i])
    lines.append(linesWithSpaces[i+1])

with open("toRun.py", "a") as file:
    file.write("""def TestRightOrder(line1, line2):
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
        return None""")

    file.write("\n")
    file.write("lines = []")
    file.write("\n")
    for i in range(len(lines)):
        stringArray = lines[i].replace('\n', '')
        line = (f"lines.append({stringArray})")
        file.write(line)
        file.write("\n")

    file.write("""correctLine = []
lines.append("[[2]]")
lines.append("[[6]]]")

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
input()""")