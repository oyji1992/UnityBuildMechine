using System;
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

        public override BuildState OnUpdate()
        {
            var buildState = Actions[CurrentIndex].OnUpdate();

            switch (buildState)
            {
                case BuildState.Success:
                    CurrentIndex++;

                    if (CurrentIndex >= Actions.Length)
                    {
                        return BuildState.Success;
                    }
                    break;
                case BuildState.None:
                case BuildState.Running:
                case BuildState.Failure:
                    return buildState;
            }

            return BuildState.Running;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}