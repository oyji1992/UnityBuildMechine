using UnityEngine;

namespace GameEditor
{
    public class BuildActionGroup : BuildAction
    {
        public BuildAction[] Actions;
        public int CurrentIndex;

        public BuildActionGroup(params BuildAction[] actions)
        {
            Actions = actions;
        }

        public override void Build()
        {
            CurrentIndex = 0;

            Actions[CurrentIndex].Build();
            Debug.Log("Do " + Actions[CurrentIndex]);
        }

        public override void Update()
        {
            if (Actions[CurrentIndex].State == BuildState.Succeed)
            {
                CurrentIndex++;

                if (CurrentIndex < Actions.Length)
                {
                    Actions[CurrentIndex].Build();
                    Debug.Log("Do " + Actions[CurrentIndex]);
                }
                else
                {
                    State = BuildState.Succeed;
                }
            }
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}