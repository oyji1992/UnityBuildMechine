using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildActionGroup : BuildAction
    {
        public BuildAction[] Actions;
        public int CurrentIndex;

        public BuildActionGroup(params BuildAction[] actions)
        {
            Actions = actions;
        }

        public override void OnEnter()
        {
            CurrentIndex = 0;

            Actions[CurrentIndex].OnEnter();
            Debug.Log("Do " + Actions[CurrentIndex]);
        }

        public override void OnUpdate()
        {
            if (Actions[CurrentIndex].State == BuildState.Success)
            {
                CurrentIndex++;

                if (CurrentIndex < Actions.Length)
                {
                    Actions[CurrentIndex].OnEnter();
                    Debug.Log("Do " + Actions[CurrentIndex]);
                }
                else
                {
                    State = BuildState.Success;
                }
            }
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}