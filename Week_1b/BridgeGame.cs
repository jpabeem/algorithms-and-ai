using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1b
{
    class BridgeGame
    {
        public int seconds { get; set; }
        public bool[] gameState { get; set; }
        public IDictionary<int, int> possibilities { get; set; }


        public BridgeGame()
        {
            seconds = 30;
            gameState = new bool[5];
            possibilities = new Dictionary<int, int>()
           {
                // init different types of possibilities
               { 0, 1 },
               { 1, 3 },
               { 2, 6 },
               { 3, 8 },
               { 4, 12 }
           };
        }

        public void MakeMove(int firstMemberIndex, int secondMemberIndex)
        {
            if (firstMemberIndex != -1)
            {
                int removeableSeconds = possibilities.Keys.ElementAt(firstMemberIndex);

            }

            if (secondMemberIndex != -1)
            {

            }
        }
    }
}
