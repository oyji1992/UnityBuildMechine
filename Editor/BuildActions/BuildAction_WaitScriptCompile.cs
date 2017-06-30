using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_WaitScriptCompile : BuildAction
    {
        private int CurrentProgress = 0;
        private double StartTime;

        private double WaitCompileFinishedTime;

        public override void Build()
        {
            StartTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += UpdateFunction;
        }

        private void UpdateFunction()
        {
            if (CurrentProgress == 0 && StartTime + 10 < EditorApplication.timeSinceStartup)
            {
                Debug.Log("跳过了等待编译");
                this.State = BuildState.Succeed;
                EditorApplication.update -= UpdateFunction;
                return;
            }

            if (CurrentProgress == 0 && EditorApplication.isCompiling == true)
            {
                CurrentProgress = 1;

            }

            if (CurrentProgress == 1 && EditorApplication.isCompiling == false)
            {
                WaitCompileFinishedTime = EditorApplication.timeSinceStartup;
                CurrentProgress = 2;

            }
            if (CurrentProgress == 2 && EditorApplication.timeSinceStartup > WaitCompileFinishedTime + 5.0f)
            {
                this.State = BuildState.Succeed;
                EditorApplication.update -= UpdateFunction;
                return;
            }
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}