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
lines = []
lines.append([1,1,3,1,1])
lines.append([1,1,5,1,1])
lines.append()
lines.append([[1],[2,3,4]])
lines.append([[1],4])
lines.append()
lines.append([9])
lines.append([[8,7,6]])
lines.append()
lines.append([[4,4],4,4])
lines.append([[4,4],4,4,4])
lines.append()
lines.append([7,7,7,7])
lines.append([7,7,7])
lines.append()
lines.append([])
lines.append([3])
lines.append()
lines.append([[[]]])
lines.append([[]])
lines.append()
lines.append([1,[2,[3,[4,[5,6,7]]]],8,9])
correctLine = []
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
input()