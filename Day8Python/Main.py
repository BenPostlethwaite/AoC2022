with open("data.txt", "r") as f:
  lines = f.readlines()
bestCoord = []
bestScore = 0

for y in range(len(lines)):
  lines[y] = lines[y].replace("\n", "")
  for x in range(len(lines[y])):
    height = int(lines[y][x])
    coord = [y, x]

    yPlus = 0
    yMinus = 0
    xPlus = 0
    xMinus = 0
    for lookY in range(y + 1, len(lines), 1):
      if int(height) <= int(lines[lookY][x]):
        yPlus += 1
        break
      else:
        yPlus += 1

    for lookY in range(y - 1, -1, -1):
      if int(height) <= int(lines[lookY][x]):
        yMinus += 1
        break
      else:
        yMinus += 1

    for lookX in range(x + 1, len(lines[0]), 1,):
      if int(height) <= int(lines[y][lookX]):
        xPlus+=1
        break
      else:
        xPlus += 1

    for lookX in range(x - 1, -1, -1):
      if int(height) <= int(lines[y][lookX]):
        xMinus +=1
        break
      else:
        xMinus += 1

    score = yPlus * yMinus * xPlus * xMinus
    #print(f"coord {coord} got a score of {score}")
    #print(f"xplus: {xPlus} xminus: {xMinus} yplus: {yPlus} yMinus: {yMinus}")
    #print("\n")

    if score > bestScore:
      bestScore = score

print(f"Best score = {bestScore}")
