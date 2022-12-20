def GetData(fileName):
    with open(fileName, "r") as f:
        coords = f.read()

    coords = coords.split('\n')

    for i in range(len(coords)):
        coords[i] = coords[i].split(',')
        for j in range(len(coords[i])):
            coords[i][j] = int(coords[i][j])
    return coords



def BFS(PcurrentCoord):
    queue = []
    queue.append(PcurrentCoord)
    visitedCoords.append(PcurrentCoord)

    while queue != []:
        s = queue.pop(0)
        if minX <= s[0] <= maxX and minY <= s[1] <= maxY and minZ <= s[2] <= maxZ:
            neighbours = []
            neighbours.append([s[0]+1,s[1],s[2]])
            neighbours.append([s[0]-1,s[1],s[2]])
            neighbours.append([s[0],s[1]+1,s[2]])
            neighbours.append([s[0],s[1]-1,s[2]])
            neighbours.append([s[0],s[1],s[2]+1])
            neighbours.append([s[0],s[1],s[2]-1])

            for neighbour in neighbours:
                if (visitedCoords.count(neighbour) == 0):
                    
                    if coords.count(neighbour) > 0:
                        foundFaces.append([s,neighbour])
                    else:
                        visitedCoords.append(neighbour)
                        queue.append(neighbour)

             


coords = GetData("data.txt")

minX = 9999999999999999999
maxX = 0
minY = 9999999999999999999
maxY = 0
minZ = 9999999999999999999
maxZ = 0

visitedCoords = []
foundFaces = []
connections = 0


for coord in coords:
    if coord[0] < minX:
        minX = coord[0]
    if coord[0] > maxX:
        maxX = coord[0]
    
    if coord[1] < minY:
        minY = coord[1]
    if coord[1] > maxY:
        maxY = coord[1]
    
    if coord[2] < minZ:
        minZ = coord[2]
    if coord[2] > maxZ:
        maxZ = coord[2]

minX -=1
minY -=1
minZ -=1
maxX +=1
maxY +=1
maxZ +=1

currentCoord = [minX, minY, minZ]       
BFS(currentCoord)
print(foundFaces.count)
input()
"""

for coord1 in coords:
    for coord2 in coords:
        diffs = []
        for i in range(3):
            diffs.append(coord2[i]-coord1[i])
        if diffs.count(0) == 2:
            if diffs.count(1) == 1 or diffs.count(-1) == 1:
                connections += 1
print((len(coords)*6)-(connections))      

"""
        

        

