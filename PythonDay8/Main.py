with open("test.txt", "r") as f:
    lines = f.readlines()
seencoords = []
for y in range(0,len(lines),1):
    lines[y] = lines[y].replace("\n", "")
    line = lines[y]

    max = int(0)
    for x in range(len(line)):
        c = line[x]

        if int(c) > max:
            seencoords.append([y,x])
            max = int(c)

for y in range(len(lines)-1,0,-1):
    lines[y] = lines[y].replace("\n", "")
    line = lines[y]

    max = int(0)
    for x in range(len(line)):
        c = line[x]

        if int(c) > max:
            seencoords.append([y,x])
            max = int(c)


for x in range(0,len(line[0]),1):
    max = 0

    for y in range(len(lines)):
        c = line[y][x]  

        if int(c) > max:
            seencoords.append([y,x])
            max = int(c)

for x in range(len(line[0])-1,0,-1):
    max = 0

    for y in range(len(lines)):
        c = line[y][x]  

        if int(c) > max:
            seencoords.append([y,x])
            max = int(c)
    


print(seencoords)
    