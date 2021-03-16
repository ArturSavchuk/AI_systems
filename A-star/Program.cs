using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_star
{
    public class AStarSearch
    {
        private State startState;
        public AStarSearch(string[,] start_placement)
        {
            startState = new State(start_placement);
        }
        public List<State> GenSuccessors(State s_parent)
        {
            List<State> successors = new List<State>();
            State parent = new State(s_parent);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (parent.placement[i, j] == "[   ]")
                    {
                        if (i - 1 >= 0)
                        {
                            State successor = new State(parent);
                            successor.EvaluatePathCost(successor.placement[i - 1, j]);
                            Tools.Swap(ref successor.placement[i, j], ref successor.placement[i - 1, j]);
                            successor.EvaluateHeuristic();
                            successor.EvaluateTotalCost();
                            successors.Add(successor);
                        }
                        if (j - 1 >= 0)
                        {
                            State successor = new State(parent);
                            successor.EvaluatePathCost(successor.placement[i, j - 1]);
                            Tools.Swap(ref successor.placement[i, j], ref successor.placement[i, j - 1]);
                            successor.EvaluateHeuristic();
                            successor.EvaluateTotalCost();
                            successors.Add(successor);
                        }
                        if (i + 1 < 2)
                        {
                            State successor = new State(parent);
                            successor.EvaluatePathCost(successor.placement[i + 1, j]);
                            Tools.Swap(ref successor.placement[i, j], ref successor.placement[i + 1, j]);
                            successor.EvaluateHeuristic();
                            successor.EvaluateTotalCost();
                            successors.Add(successor);
                        }
                        if (j + 1 < 3)
                        {
                            State successor = new State(parent);
                            successor.EvaluatePathCost(successor.placement[i, j + 1]);
                            Tools.Swap(ref successor.placement[i, j], ref successor.placement[i, j + 1]);
                            successor.EvaluateHeuristic();
                            successor.EvaluateTotalCost();
                            successors.Add(successor);
                        }
                    }
                }
            }


            successors = successors.OrderBy(s => s.TotalCost).ToList();
            return successors;
        }

        public void Print(State state)
        {
            Console.WriteLine("______________STATE__________");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(state.placement[i, j] + "    ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Heur =" + state.HeuristicValue + "    Path cost = " + state.PathCost);
            Console.WriteLine("Total cost = " + state.TotalCost);
            Console.WriteLine("_____________________________");
        }

        public bool CheckPlacementEquals(State state1, State state2)
        {
            bool equal = true;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state1.placement[i, j] != state2.placement[i, j])
                    {
                        equal = false;
                        break;
                    }
                }
            }
            return equal;
        }

        public bool CheckGoalReached(State currentState)
        {
            if (currentState.placement[0, 2] == "armchair" && currentState.placement[1, 2] == "cupboard")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckExist(List<State> explored, State statew)
        {
            bool exist = false;
            foreach (State s in explored)
            {
                if (CheckPlacementEquals(s, statew))
                {
                    return true;

                }
            }
            return exist;
        }

        public bool CheckValidMove(State CurrentState, State MoveState)
        {
            List<State> validMoves = GenSuccessors(CurrentState);
            if (!CheckExist(validMoves, MoveState))
            {
                return false;
            }
            return true;
        }

        public List<State> FindPath()
        {
            List<State> ExploredVertices = new List<State>();
            List<State> ActiveVertices = new List<State>();
            List<State> nextstates;

            State CurrentState = new State(startState);
            ExploredVertices.Add(CurrentState);

            List<State> path = new List<State>();

            while (!CheckGoalReached(CurrentState))
            {
                State parent = CurrentState;
                nextstates = GenSuccessors(CurrentState);
                ActiveVertices.AddRange(nextstates);

                for (int j = 0; j < ActiveVertices.Count; j++)
                {
                    State last = ActiveVertices[j];
                    if (CheckExist(ExploredVertices, last) == false)
                    {
                        CurrentState = last;
                        CurrentState.parent = parent;
                        ExploredVertices.Add(CurrentState);
                        ActiveVertices.RemoveAt(j);
                        break;
                    }
                }
                path.Add(CurrentState);
            }
            return GetResultedVertices(path);
        }

        public List<State> GetResultedVertices (List<State> PassedVertices)
        {
            List<State> ExpandedVertices = new List<State>();
            State state = PassedVertices[PassedVertices.Count - 1];
            while (state != null)
            {
                ExpandedVertices.Add(state);
                state = state.parent;
            }
            List<State> result_path = new List<State>();
            State pathstate = ExpandedVertices[0];
            result_path.Add(pathstate);
            for (int i = 1; i < ExpandedVertices.Count; i++)
            {
                if (CheckValidMove(pathstate, ExpandedVertices[i]))
                {
                    pathstate = ExpandedVertices[i];
                    result_path.Insert(0, pathstate);
                }
            }
            return result_path;
        }

        public void ShowResult(List<State> result_path)
        {
            foreach (var s in result_path)
            {
                Print(s);
                Console.WriteLine(" | \t\t");
                Console.WriteLine("\\ /\t\t");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[,] start_placement = new string[,]
            {
                {"table", "chair", "cupboard"},
                {"chair", "[   ]", "armchair"},
            };
            
            AStarSearch search = new AStarSearch(start_placement);
            search.ShowResult(search.FindPath());
        }
    }
 }
