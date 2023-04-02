using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Robit_Game
{
    public class OverWorld
    {
        int Location;
        public int Story;
        public string[][] DialogLookup;
        static WorldNode[] Map;
        public OverWorld()
        {
            Map = new WorldNode[41];
            Location = 0;
            Story = 0;
            InitializeMap();
            InitializeDialogue();
        }
        public void Translate(int Direction)
        {
            if (Map[Location].NeighborStoryReq[Direction] <= Story && Map[Location].Neighbors[Direction] >= 0)
            {
                Location = Map[Location].Neighbors[Direction];
            }
        }
        public string PrintDescription()
        {
            int x = 0;
            while (Story >= Map[Location].DescriptionState[x+1] && x < Map[Location].Description.Length-1)
            {
                x++;
            }
            return Map[Location].Description[x];

        }
        public bool IsStoryTrigger()
        {
            return Story == Map[Location].StoryTrigger;
        }
        public void InitializeDialogue()
        {
            DialogLookup = new string[100][];
            DialogLookup[0] = new string[] { "K-813: \"You're finally awake. I thought you were a goner!\"", "Abel: \"What are you talking about?\""};
        }
        public string[] UpdateStory()
        {
            Story++;
            return DialogLookup[Story-1];
        }
        public string[] FetchNeighbors()
        {
            string[] NeighborNames = new string[4];
            for (int x = 0; x < 4; x++)
            {
                if (Map[Location].Neighbors[x]==-1)
                {
                    NeighborNames[x] = "-";
                }
                else
                {
                    NeighborNames[x] = Map[Map[Location].Neighbors[x]].Name;
                }
            }
            return NeighborNames;
        }
        void InitializeMap()
        {
            Map[0]  = new WorldNode { Neighbors = new int[] { 1,  -1, -1,  2 }, NeighborStoryReq = new int[] {   0,   0,   0,   2 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Clinic",                          DescriptionState = new int[] { 0, 1, 2, 999 },             Description = new string[] { "First description", "Later description", "Latest description", "Error" }, StoryTrigger = 100 };
            Map[1]  = new WorldNode { Neighbors = new int[] { -1, -1,  0, -1 }, NeighborStoryReq = new int[] { 100, 100,   0, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Surgery",                         DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Surgery Description", "Error"} };
            Map[2]  = new WorldNode { Neighbors = new int[] {  3,  0, 28, 40 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Bitsburg Square - North",         DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[3]  = new WorldNode { Neighbors = new int[] {  4, -1,  2, 39 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Bitsburg Outskirts - North",      DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[4]  = new WorldNode { Neighbors = new int[] {  5, -1,  3, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Scrap-Forge Border",              DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[5]  = new WorldNode { Neighbors = new int[] { -1, -1,  4,  6 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Security Checkpoint",             DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[6]  = new WorldNode { Neighbors = new int[] { -1,  5, -1,  7 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Crucible 20",                     DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[7]  = new WorldNode { Neighbors = new int[] { -1,  6, -1,  8 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Unloading Bay 20",                DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[8]  = new WorldNode { Neighbors = new int[] { 9,   7, 21, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Rail Station 20",                 DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[9]  = new WorldNode { Neighbors = new int[] { 10, 11,  8, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Rail Station 1",                  DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };
            Map[10] = new WorldNode { Neighbors = new int[] { -1,  9, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Rockford Station",                DescriptionState = new int[] { 0, 999 },                   Description = new string[] { "Test", "Error" } };

            Map[11] = new WorldNode { Neighbors = new int[] { 12, 20, -1,  9 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Unloading Bay 1",                 DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[12] = new WorldNode { Neighbors = new int[] { 13, 19, 11, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Track Junction",                  DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[13] = new WorldNode { Neighbors = new int[] { 14, -1, 12, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Smithville Outskirts",            DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } }; 
            Map[14] = new WorldNode { Neighbors = new int[] { 15, 17, 13, 18 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Smithville Square",               DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[15] = new WorldNode { Neighbors = new int[] { 16, -1, 14, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Forge Capitol Entrance",          DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[16] = new WorldNode { Neighbors = new int[] { -1, -1, 15, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Smith's Domain",                  DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[17] = new WorldNode { Neighbors = new int[] { -1, -1, -1, 14 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Guy's Drone Repair",              DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[18] = new WorldNode { Neighbors = new int[] { -1, 14, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Chuck's Fresh Forges",            DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[19] = new WorldNode { Neighbors = new int[] { -1, -1, -1, 12 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Monorail Trail",                  DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[20] = new WorldNode { Neighbors = new int[] { -1, -1, -1, 11 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Crucible 1",                      DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };

            Map[21] = new WorldNode { Neighbors = new int[] {  8, 22, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Railhouse",                       DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[22] = new WorldNode { Neighbors = new int[] { -1, 23, -1, 21 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Railhouse Outskirts",             DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[23] = new WorldNode { Neighbors = new int[] { 24, -1, 33, 22 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Sheet Metal Peak",                DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[24] = new WorldNode { Neighbors = new int[] { 25, -1, 23, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Crash Site",                      DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[25] = new WorldNode { Neighbors = new int[] { -1, 26, 24, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Mech Graveyard",                  DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[26] = new WorldNode { Neighbors = new int[] { 27, -1, -1, 25 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Scrap Wastes Outskirts",          DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[27] = new WorldNode { Neighbors = new int[] { 28, -1, 26, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Bitsburg Outskirts - South",      DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[28] = new WorldNode { Neighbors = new int[] {  2, 29, 27, 32 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Bitsburg Square - South",         DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[29] = new WorldNode { Neighbors = new int[] { -1, 30, -1, 28 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Shady Alley",                     DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[30] = new WorldNode { Neighbors = new int[] { -1, 31, -1, 29 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Shadier Alley",                   DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };

            Map[31] = new WorldNode { Neighbors = new int[] { -1, -1, -1, 30 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Shadiest Alley",                  DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[32] = new WorldNode { Neighbors = new int[] { -1, 28, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Sneed's Shop",                    DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[33] = new WorldNode { Neighbors = new int[] { 23, -1, 34, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Collection Site",                 DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[34] = new WorldNode { Neighbors = new int[] { 33, 35, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Auto Mountain",                   DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[35] = new WorldNode { Neighbors = new int[] { 36, -1, 38, 34 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Rust Valley",                     DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[36] = new WorldNode { Neighbors = new int[] { 37, -1, 35, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "???",                             DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[37] = new WorldNode { Neighbors = new int[] { -1, -1, 36, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Pit of Trials Construction Site", DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[38] = new WorldNode { Neighbors = new int[] { 35, -1, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Dead-End",                        DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[39] = new WorldNode { Neighbors = new int[] { -1,  3, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Rusty's Place",                   DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
            Map[40] = new WorldNode { Neighbors = new int[] { -1,  2, -1, -1 }, NeighborStoryReq = new int[] { 100, 100, 100, 100 }, EnemyTable = new int[] { 15, 15, 15, 15 }, Name = "Mayor's Office",                  DescriptionState = new int[] { 0, 999 }, Description = new string[] { "Test", "Error" } };
        }
        public class WorldNode
        {
            public int[] Neighbors;
            public int[] NeighborStoryReq;
            public int[] EnemyTable;
            public string[] Description;
            public int[] DescriptionState;
            public string Name;
            public int StoryTrigger;
        }
    }
}
