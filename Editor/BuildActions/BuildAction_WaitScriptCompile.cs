using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    //    /// <summary>
    //    /// 等待编译完毕
    //    /// </summary>
    //    public class BuildAction_WaitScriptCompile : BuildAction
    //    {
    //        private int CurrentProgress = 0;
    //        private double StartTime;
    //
    //        private double WaitCompileFinishedTime;
    //
    //        private BuildState _currentState = BuildState.None;
    //
    //        private bool start = false;
    //
    //        public override BuildState OnUpdate()
    //        {
    //            if (start == false)
    //            {
    //                start = true;
    //                StartTime = EditorApplication.timeSinceStartup;
    //                EditorApplication.update += UpdateFunction;
    //            }
    //
    //
    //            return _currentState;
    //
    //
    //        }
    //
    //        private void UpdateFunction()
    //        {
    //            if (CurrentProgress == 0 && StartTime + 3 < EditorApplication.timeSinceStartup)
    //            {
    //                Debug.Log("跳过等待编译");
    //                EditorApplication.update -= UpdateFunction;
    //                this._currentState = BuildState.Success;
    //
    //            }
    //
    //            if (CurrentProgress == 0 && EditorApplication.isCompiling == true)
    //            {
    //                CurrentProgress = 1;
    //
    //            }
    //
    //            if (CurrentProgress == 1 && EditorApplication.isCompiling == false)
    //            {
    //                WaitCompileFinishedTime = EditorApplication.timeSinceStartup;
    //                CurrentProgress = 2;
    //
    //            }
    //            if (CurrentProgress == 2 && EditorApplication.timeSinceStartup > WaitCompileFinishedTime + 2.0f)
    //            {
    //                EditorApplication.update -= UpdateFunction;
    //                this._currentState = BuildState.Success;
    //
    //            }
    //        }
    //
    //        public override BuildProgress GetProgress()
    //        {
    //            return null;
    //        }
    //    }
}