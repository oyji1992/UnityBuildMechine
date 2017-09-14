using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_BuildProjectWindowsStandalone : BuildAction
    {
        public bool X64;
        public string ExeName;
        public string BuildPath;
        public string ProjectName;

        public BuildAction_BuildProjectWindowsStandalone(string projectName, string exeName, string buildPath, bool x64)
        {
            ProjectName = projectName;
            X64 = x64;
            ExeName = exeName;
            BuildPath = buildPath;
        }

        public override BuildState OnUpdate()
        {
            var listScene = BuildHelper.GetAllScenesInBuild();

            var fileName = ExeName;

            if (ExeName.EndsWith(".exe") == false)
            {
                fileName += ".exe";
            }

            var x = X64 ? "x64" : "x86";

            var dirName = string.Format("{0}_{3}_build{1}_{2:yyyyMMddHHmm}", this.ProjectName, BuildHelper.GetBuildNum(), DateTime.Now, x);

            var path = Path.Combine(BuildPath, dirName);

            path = Path.Combine(path, fileName);

            var buildTarget = X64 ? BuildTarget.StandaloneWindows64 : BuildTarget.StandaloneWindows;

            var res = BuildPipeline.BuildPlayer(listScene.ToArray(), path, buildTarget, BuildOptions.None);

            if (string.IsNullOrEmpty(res) == false)
            {
                throw new Exception("Build Fail : " + res);
            }

            Debug.LogFormat("打包至 {0} 结果 {1}", path, res);

            Context.Set("BuildPath", path);

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}