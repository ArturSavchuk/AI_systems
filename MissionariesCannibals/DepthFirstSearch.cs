using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionariesCannibals
{
    //depth limited search
    class DepthFirstSearch
    {
        public State exec(State initialState)
        {

            //init depth limit
            int limit = 20;
            return recursiveDLS(initialState, limit);
        }

        private State recursiveDLS(State state, int limit)
        {
            if (state.isGoal())
            {
                return state;
            }
            else if (limit == 0)
            {
                return null;
            }
            else
            {
                List<State> successors = state.generateSuccessors();
                foreach (State child in successors)
                {
                    State result = recursiveDLS(child, limit - 1);
                    if (null != result)
                    {
                        return result;
                    }
                }
                return null;
            }
        }
    }
}
