using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionariesCannibals
{
    class Program
    {
        private static void executeDLS(State initialState)
        {
            DepthFirstSearch search = new DepthFirstSearch();
            State solution = search.exec(initialState);
            printSolution(solution);
        }
        private static void printSolution(State solution)
        {
            if (null == solution)
            {
                Console.WriteLine("\nNo solution found.");
            }
            else
            {
                Console.WriteLine("\nSolution (cannibalLeft,missionaryLeft,boat,cannibalRight,missionaryRight): ");
                List<State> path = new List<State>();
                State state = solution;
                while (null != state)
                {
                    path.Add(state);
                    state = state.getParentState();
                }

                int depth = path.Count - 1;
                for (int i = depth; i >= 0; i--)
                {
                    state = path[i];
                    if (state.isGoal())
                    {
                        Console.WriteLine(state.ToString());
                    }
                    else
                    {
                        Console.WriteLine(state.ToString() + " -> ");
                    }
                }
                Console.WriteLine("\nDepth: " + depth);
            }
        }
        static void Main(string[] args)
        {
            //init state with start conditions 

            State initialState = new State(3, 3, Position.LEFT, 0, 0);

            executeDLS(initialState);
        }
    }
}
