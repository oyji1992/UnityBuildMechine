using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    /// <summary>
    /// 提升一个Build号
    /// </summary>
    public class BuildAction_IncreaseBuildNum : BuildAction
    {
        public override BuildState OnUpdate()
        {
            BuildHelper.AddBuildNum();
            var buildNumber = BuildHelper.GetBuildNum();

            Debug.Log("当前Build版本：" + buildNumber);

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}