using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1b
{
    public enum TorchSide
    {
        LEFT,
        RIGHT
    }

    class BridgeState : ICloneable
    {
        public List<int> leftSide { get; set; }
        public List<int> rightSide { get; set; }
        public List<string> moves { get; set; }

        public Tuple<int, int> lastMove { get; set; }

        public TorchSide torchSide { get; set; }
        public int elapsedTime { get; set; }

        public string id { get; set; }

        public BridgeState(string id)
        {
            this.id = id;
            leftSide = new List<int> { 1, 3, 6, 8, 12};
            rightSide = new List<int>();
            moves = new List<string>();
            torchSide = TorchSide.LEFT;
            elapsedTime = 0;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void MoveLeft()
        {
            //if (leftSide.Count <= 0)
            //    return;

            int fastestPlayer = GetSmallestFromRightSIde();

            string leftMove = "<-- player@speed " + fastestPlayer + ", ID: " + id; ;
            moves.Add(leftMove);

            Console.WriteLine("<-- player@speed {0} " + ", ID: " + id, fastestPlayer);

            rightSide.Remove(fastestPlayer);
            leftSide.Add(fastestPlayer);
            IncreaseElapsedTime(fastestPlayer);

            torchSide = TorchSide.LEFT;
        }

        public void MoveLeft(int player)
        {
            rightSide.Remove(player);
            leftSide.Add(player);
            IncreaseElapsedTime(player);

            torchSide = TorchSide.LEFT;
        }

        public void IncreaseElapsedTime(int time)
        {
            elapsedTime += time;
        }

        public int GetSmallestFromRightSIde()
        {
            return rightSide.Min();
        }

        public bool IsFlashLightAtHome()
        {
            return torchSide == TorchSide.LEFT;
        }

        public bool IsSolution()
        {
            return elapsedTime <= 30 && (leftSide.Count == 0 && rightSide.Count == 5);
        }
    }
}
