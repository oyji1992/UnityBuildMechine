using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_BuildProjectIOS : BuildAction
    {
        public string BuildPath;
        public string ProjectName;

        public BuildAction_BuildProjectIOS(string projectName, string buildPath)
        {
            ProjectName = projectName;
            BuildPath = buildPath;
        }

        public override BuildState OnUpdate()
        {
            PlayerSettings.iOS.buildNumber = BuildHelper.GetBuildNum().ToString();

            var listScene = BuildHelper.GetAllScenesInBuild();

            var path = Path.Combine(BuildPath, ProjectName);

            var dir = Path.GetDirectoryName(path);

            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            EditorPrefs.SetString("BuildMechine.ProjectPath", path);

            var res = BuildPipeline.BuildPlayer(listScene.ToArray(), path, BuildTarget.iOS, BuildOptions.None);

            if (string.IsNullOrEmpty(res) == false)
            {
                // 在Unity5.4.2以后的版本，修改plist会出错
                if (res.Contains("End tag cannot appear in this state.") == false)
                {
                    throw new Exception("Build Fail : " + res);
                }
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