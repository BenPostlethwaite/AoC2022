using System.ComponentModel;
namespace Day19Charp;
class Program
{
    
// --- Day 19: Not Enough Minerals ---
// Your scans show that the lava did indeed form obsidian!

// The wind has changed direction enough to stop sending lava droplets toward you, so you and the elephants exit the cave. As you do, you notice a collection of geodes around the pond. Perhaps you could use the obsidian to create some geode-cracking robots and break them open?

// To collect the obsidian from the bottom of the pond, you'll need waterproof obsidian-collecting robots. Fortunately, there is an abundant amount of clay nearby that you can use to make them waterproof.

// In order to harvest the clay, you'll need special-purpose clay-collecting robots. To make any type of robot, you'll need ore, which is also plentiful but in the opposite direction from the clay.

// Collecting ore requires ore-collecting robots with big drills. Fortunately, you have exactly one ore-collecting robot in your pack that you can use to kickstart the whole operation.

// Each robot can collect 1 of its resource type per minute. It also takes one minute for the robot factory (also conveniently from your pack) to construct any type of robot, although it consumes the necessary resources available when construction begins.

// The robot factory has many blueprints (your puzzle input) you can choose from, but once you've configured it with a blueprint, you can't change it. You'll need to work out which blueprint is best.

// For example:

// Blueprint 1:
//   Each ore robot costs 4 ore.
//   Each clay robot costs 2 ore.
//   Each obsidian robot costs 3 ore and 14 clay.
//   Each geode robot costs 2 ore and 7 obsidian.

// Blueprint 2:
//   Each ore robot costs 2 ore.
//   Each clay robot costs 3 ore.
//   Each obsidian robot costs 3 ore and 8 clay.
//   Each geode robot costs 3 ore and 12 obsidian.
// (Blueprints have been line-wrapped here for legibility. The robot factory's actual assortment of blueprints are provided one blueprint per line.)

// The elephants are starting to look hungry, so you shouldn't take too long; you need to figure out which blueprint would maximize the number of opened geodes after 24 minutes by figuring out which robots to build and when to build them.

// Using blueprint 1 in the example above, the largest number of geodes you could open in 24 minutes is 9. One way to achieve that is:

// == Minute 1 ==
// 1 ore-collecting robot collects 1 ore; you now have 1 ore.

// == Minute 2 ==
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.

// == Minute 3 ==
// Spend 2 ore to start building a clay-collecting robot.
// 1 ore-collecting robot collects 1 ore; you now have 1 ore.
// The new clay-collecting robot is ready; you now have 1 of them.

// == Minute 4 ==
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 1 clay-collecting robot collects 1 clay; you now have 1 clay.

// == Minute 5 ==
// Spend 2 ore to start building a clay-collecting robot.
// 1 ore-collecting robot collects 1 ore; you now have 1 ore.
// 1 clay-collecting robot collects 1 clay; you now have 2 clay.
// The new clay-collecting robot is ready; you now have 2 of them.

// == Minute 6 ==
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 2 clay-collecting robots collect 2 clay; you now have 4 clay.

// == Minute 7 ==
// Spend 2 ore to start building a clay-collecting robot.
// 1 ore-collecting robot collects 1 ore; you now have 1 ore.
// 2 clay-collecting robots collect 2 clay; you now have 6 clay.
// The new clay-collecting robot is ready; you now have 3 of them.

// == Minute 8 ==
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 3 clay-collecting robots collect 3 clay; you now have 9 clay.

// == Minute 9 ==
// 1 ore-collecting robot collects 1 ore; you now have 3 ore.
// 3 clay-collecting robots collect 3 clay; you now have 12 clay.

// == Minute 10 ==
// 1 ore-collecting robot collects 1 ore; you now have 4 ore.
// 3 clay-collecting robots collect 3 clay; you now have 15 clay.

// == Minute 11 ==
// Spend 3 ore and 14 clay to start building an obsidian-collecting robot.
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 3 clay-collecting robots collect 3 clay; you now have 4 clay.
// The new obsidian-collecting robot is ready; you now have 1 of them.

// == Minute 12 ==
// Spend 2 ore to start building a clay-collecting robot.
// 1 ore-collecting robot collects 1 ore; you now have 1 ore.
// 3 clay-collecting robots collect 3 clay; you now have 7 clay.
// 1 obsidian-collecting robot collects 1 obsidian; you now have 1 obsidian.
// The new clay-collecting robot is ready; you now have 4 of them.

// == Minute 13 ==
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 4 clay-collecting robots collect 4 clay; you now have 11 clay.
// 1 obsidian-collecting robot collects 1 obsidian; you now have 2 obsidian.

// == Minute 14 ==
// 1 ore-collecting robot collects 1 ore; you now have 3 ore.
// 4 clay-collecting robots collect 4 clay; you now have 15 clay.
// 1 obsidian-collecting robot collects 1 obsidian; you now have 3 obsidian.

// == Minute 15 ==
// Spend 3 ore and 14 clay to start building an obsidian-collecting robot.
// 1 ore-collecting robot collects 1 ore; you now have 1 ore.
// 4 clay-collecting robots collect 4 clay; you now have 5 clay.
// 1 obsidian-collecting robot collects 1 obsidian; you now have 4 obsidian.
// The new obsidian-collecting robot is ready; you now have 2 of them.

// == Minute 16 ==
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 4 clay-collecting robots collect 4 clay; you now have 9 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 6 obsidian.

// == Minute 17 ==
// 1 ore-collecting robot collects 1 ore; you now have 3 ore.
// 4 clay-collecting robots collect 4 clay; you now have 13 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 8 obsidian.

// == Minute 18 ==
// Spend 2 ore and 7 obsidian to start building a geode-cracking robot.
// 1 ore-collecting robot collects 1 ore; you now have 2 ore.
// 4 clay-collecting robots collect 4 clay; you now have 17 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 3 obsidian.
// The new geode-cracking robot is ready; you now have 1 of them.

// == Minute 19 ==
// 1 ore-collecting robot collects 1 ore; you now have 3 ore.
// 4 clay-collecting robots collect 4 clay; you now have 21 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 5 obsidian.
// 1 geode-cracking robot cracks 1 geode; you now have 1 open geode.

// == Minute 20 ==
// 1 ore-collecting robot collects 1 ore; you now have 4 ore.
// 4 clay-collecting robots collect 4 clay; you now have 25 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 7 obsidian.
// 1 geode-cracking robot cracks 1 geode; you now have 2 open geodes.

// == Minute 21 ==
// Spend 2 ore and 7 obsidian to start building a geode-cracking robot.
// 1 ore-collecting robot collects 1 ore; you now have 3 ore.
// 4 clay-collecting robots collect 4 clay; you now have 29 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 2 obsidian.
// 1 geode-cracking robot cracks 1 geode; you now have 3 open geodes.
// The new geode-cracking robot is ready; you now have 2 of them.

// == Minute 22 ==
// 1 ore-collecting robot collects 1 ore; you now have 4 ore.
// 4 clay-collecting robots collect 4 clay; you now have 33 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 4 obsidian.
// 2 geode-cracking robots crack 2 geodes; you now have 5 open geodes.

// == Minute 23 ==
// 1 ore-collecting robot collects 1 ore; you now have 5 ore.
// 4 clay-collecting robots collect 4 clay; you now have 37 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 6 obsidian.
// 2 geode-cracking robots crack 2 geodes; you now have 7 open geodes.

// == Minute 24 ==
// 1 ore-collecting robot collects 1 ore; you now have 6 ore.
// 4 clay-collecting robots collect 4 clay; you now have 41 clay.
// 2 obsidian-collecting robots collect 2 obsidian; you now have 8 obsidian.
// 2 geode-cracking robots crack 2 geodes; you now have 9 open geodes.
// However, by using blueprint 2 in the example above, you could do even better: the largest number of geodes you could open in 24 minutes is 12.

// Determine the quality level of each blueprint by multiplying that blueprint's ID number with the largest number of geodes that can be opened in 24 minutes using that blueprint. In this example, the first blueprint has ID 1 and can open 9 geodes, so its quality level is 9. The second blueprint has ID 2 and can open 12 geodes, so its quality level is 24. Finally, if you add up the quality levels of all of the blueprints in the list, you get 33.

// Determine the quality level of each blueprint using the largest number of geodes it could produce in 24 minutes. What do you get if you add up the quality level of all of the blueprints in your list?

    static void Main(string[] args)
    {
        List<BluePrint> blueprints = GetBluePrints("test.txt");
        Dictionary<string, int> resources = new Dictionary<string, int>(){
            {"ore", 0},
            {"clay", 0},
            {"obsidian", 0},
            {"geode", 0}
        };

        foreach (BluePrint blueprint in blueprints)
        {
            Console.WriteLine(MaxGeodes(blueprint, 24, resources));
        }      
    }
    
    public static int MaxGeodes(BluePrint bluePrint, int minutes, Dictionary<string, int> resources)
    {
        List<string> resourceNames = new List<string>(){ "ore", "clay", "obsidian", "geode" };
        int maxGeodes = 0;
        List<State> endStates = new List<State>();

        // Define the initial search state
        var initialState = new State(resources, new Dictionary<string, int>(){{"ore", 1}, {"clay", 0}, {"obsidian", 0}, {"geode", 0}}, minutes);
        var queue = new Queue<State>();
        queue.Enqueue(initialState);
        

        // Perform breadth-first search to explore the space of possible action sequences
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.minutes > 0)
            {
                State mineState = new State(state.resources, state.robots, state.minutes-1);
                foreach (string resource in resourceNames)
                {
                    mineState.resources[resource] += mineState.robots[resource];
                }

                //first priority is geode robot
                if (state.resources["ore"] >= bluePrint.robotCosts["geode"]["ore"] &&
                        state.resources["clay"] >= bluePrint.robotCosts["geode"]["clay"] &&
                        state.resources["obsidian"] >= bluePrint.robotCosts["geode"]["obsidian"])
                {
                    //create a deep copy of state
                    var newState = new State(mineState.resources, mineState.robots, mineState.minutes);
                    //subtract cost of robot from resources
                    newState.resources = new Dictionary<string, int>(){
                        {"ore", mineState.resources["ore"] - bluePrint.robotCosts["geode"]["ore"]},
                        {"clay", mineState.resources["clay"] - bluePrint.robotCosts["geode"]["clay"]},
                        {"obsidian", mineState.resources["obsidian"] - bluePrint.robotCosts["geode"]["obsidian"]},
                        {"geode", mineState.resources["geode"]}
                    };
                    //add new robot to robots
                    newState.robots = new Dictionary<string, int>(){
                        {"ore", mineState.robots["ore"]},
                        {"clay", mineState.robots["clay"]},
                        {"obsidian", mineState.robots["obsidian"]},
                        {"geode", mineState.robots["geode"] + 1}
                    };
                    queue.Enqueue(newState);
                }
                else
                {                    
                    foreach (var resource in resourceNames.GetRange(0, 3))
                    {
                        if (state.resources["ore"] >= bluePrint.robotCosts[resource]["ore"] &&
                            state.resources["clay"] >= bluePrint.robotCosts[resource]["clay"])
                        {
                            //create a deep copy of state
                            var newState = new State(mineState.resources, mineState.robots, mineState.minutes);
                            //subtract cost of robot from resources
                            newState.resources = new Dictionary<string, int>(){
                                {"ore", mineState.resources["ore"] - bluePrint.robotCosts[resource]["ore"]},
                                {"clay", mineState.resources["clay"] - bluePrint.robotCosts[resource]["clay"]},
                                {"obsidian", mineState.resources["obsidian"]},
                                {"geode", mineState.resources["geode"]}
                            };
                            newState.robots = new Dictionary<string, int>(){
                                {"ore", mineState.robots["ore"]},
                                {"clay", mineState.robots["clay"]},
                                {"obsidian", mineState.robots["obsidian"]},
                                {"geode", mineState.robots["geode"]}
                            };
                            newState.robots[resource] += 1;
                            //add newState to queue
                            queue.Enqueue(newState);
                        }
                    }
                    queue.Enqueue(mineState);
                }
            }

            else if (state.resources["geode"] > maxGeodes)
            {
                maxGeodes = state.resources["geode"];
                endStates.Add(state);
            }
            else
            {
                endStates.Add(state);
            }
        }
        return maxGeodes;
    }
    public static List<BluePrint> GetBluePrints(string fileName)
    {
        List<BluePrint> blueprints = new List<BluePrint>();
        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                //remove first 2 words from line
                blueprints.Add(new BluePrint(line));
            }
        }
        return blueprints;
    }
}
class BluePrint
{
    public Dictionary<string, Dictionary<string, int>> robotCosts = new Dictionary<string, Dictionary<string, int>>();
    public BluePrint(string rawData)
    {
        robotCosts.Add("ore", new Dictionary<string, int>(){{"ore", 0}, {"clay", 0}, {"obsidian", 0}, {"geode", 0}});
        robotCosts.Add("clay", new Dictionary<string, int>(){ {"ore", 0}, {"clay", 0}, {"obsidian", 0}, {"geode", 0}});
        robotCosts.Add("obsidian", new Dictionary<string, int>(){ {"ore", 0}, {"clay", 0}, {"obsidian", 0}, {"geode", 0} });
        robotCosts.Add("geode", new Dictionary<string, int>(){ {"ore", 0}, {"clay", 0}, {"obsidian", 0}, {"geode", 0} });

        //remove all characters before and including : from rawData
        rawData = rawData.Substring(rawData.IndexOf(":") + 2);

        //remove last character from rawData
        rawData = rawData.Substring(0, rawData.Length - 1);

        string[] resourceNames = new string[] { "ore", "clay", "obsidian", "geode" };
        string[] resoureInfo = rawData.Split(". ");
        foreach (string resource in resoureInfo)
        {
            string[] words = resource.Split(" ");
            string resourceName = words[1];
            for (int i = 2; i < words.Length; i++)
            {
                foreach (string name in resourceNames)
                {
                    if (words[i].Contains(name))
                    {
                        robotCosts[resourceName][words[i]] = int.Parse(words[i - 1]);
                    }
                }
            }
        }
    }
}
class State
{
    public int minutes;
    public Dictionary<string, int> robots = new Dictionary<string, int>()
    {
        {"ore", 0},
        {"clay", 0},
        {"obsidian", 0},
        {"geode", 0}
    };
    public Dictionary<string, int> resources = new Dictionary<string, int>()
    {
        {"ore", 0},
        {"clay", 0},
        {"obsidian", 0},
        {"geode", 0}
    };
    public State(Dictionary<string, int> resources, Dictionary<string, int> robots, int minutes)
    {
        this.resources = resources;
        this.robots = robots;
        this.minutes = minutes;
    }
}