using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_BuildProjectAndroid : BuildAction
    {
        public string _projectName;

        public BuildAction_BuildProjectAndroid(string projectName)
        {
            _projectName = projectName;
        }

        public override void OnEnter()
        {

            PlayerSettings.keyaliasPass = string.IsNullOrEmpty(PlayerSettings.keyaliasPass) ? "123456" : PlayerSettings.keyaliasPass;
            PlayerSettings.keystorePass = string.IsNullOrEmpty(PlayerSettings.keystorePass) ? "123456" : PlayerSettings.keystorePass;

            var listScene = new List<string>();
            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var s = EditorBuildSettings.scenes[i];
                if (s.enabled)
                {
                    listScene.Add(s.path);
                    Debug.Log(s.path);
                }
            }

            var newFileName = PlayerSettings.productName + "_" + _projectName + DateTime.Now.ToString("_yyyyMMddHHmm");
            var dir = Application.dataPath.Replace("/Assets", "");

            var path = string.Format("AutoBuild_{0}/{1}/{2}.apk", Path.GetFileName(dir), newFileName, newFileName);

            path = Path.Combine(dir, path);
            dir = Path.GetDirectoryName(path);

            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            EditorPrefs.SetString("BuildMechine.ProjectPath", path);

            var res = BuildPipeline.BuildPlayer(listScene.ToArray(), path, BuildTarget.Android, BuildOptions.None);

            Debug.LogFormat("打包至 {0} 结果 {1}", path, res);

            this.State = BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}