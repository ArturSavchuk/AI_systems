using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star
{
    public class State : IComparable
    {
        public string[,] placement = new string[2, 3];

        public State parent;
        public State(string[,] state)
        {
            this.placement = Tools.Copy(state);
        }
        public State(State state)
        {
            this.placement = Tools.Copy(state.placement);
            this.PathCost = state.PathCost;
        }

        public int PathCost = 0;
        public int HeuristicValue = 0;
        public int TotalCost;

        public void EvaluatePathCost(string furniture)
        {
            if (furniture == "chair")
            {
                PathCost += 3;
            }
            if (furniture == "armchair")
            {
                PathCost += 4;
            }
            if (furniture == "table")
            {
                PathCost += 5;
            }
            if (furniture == "cupboard")
            {
                PathCost += 6;
            }
        }

        //HammingDistance
        public void EvaluateHeuristic()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (placement[i, j] == "cupboard")
                    {
                        HeuristicValue += Math.Abs(1 - i);
                        HeuristicValue += Math.Abs(2 - j);
                    }
                    if (placement[i, j] == "armchair")
                    {
                        HeuristicValue += Math.Abs(0 - i);
                        HeuristicValue += Math.Abs(2 - j);
                    }
                }
            }
        }

        public void EvaluateTotalCost()
        {
            TotalCost = PathCost + HeuristicValue;
        }

        public int CompareTo(object o)
        {
            State p = o as State;
            return TotalCost.CompareTo(p.TotalCost);
        }
    }
}
