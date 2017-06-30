using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public class BuildProjectStandalone : BuildAction
    {
        public string _projectName;

        public BuildProjectStandalone(string projectName)
        {
            _projectName = projectName;
        }

        public override void Build()
        {
            var listScene = new List<string>();
            for(var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var s = EditorBuildSettings.scenes[i];
                if(s.enabled)
                {
                    listScene.Add(s.path);
                    Debug.Log(s.path);
                }
            }

            var newFileName = _projectName + DateTime.Now.ToString("_yyyyMMddHHmm");
            var dir = Application.dataPath.Replace("/Assets", "");

            var path = string.Format("AutoBuild_{0}/{1}/game.exe", Path.GetFileName(dir), newFileName);


            EditorPrefs.SetString("BuildMechine.ProjectPath", Path.Combine(dir, path));

            var res = BuildPipeline.BuildPlayer(listScene.ToArray(),
                                                path, BuildTarget.StandaloneWindows64,
                                                BuildOptions.None);


            Debug.LogFormat("打包至 {0} 结果 {1}", path, res);

            this.State = BuildState.Succeed;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}