using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_BuildProjectAndroid : BuildAction
    {
        public string KeyAliasPass;
        public string KeyStorePass;
        public string BuildPath;
        public string ProjectName;

        public BuildAction_BuildProjectAndroid(string projectName, string buildPath, string keyAliasPass = "", string keyStorePass = "")
        {
            KeyAliasPass = keyAliasPass;
            KeyStorePass = keyStorePass;
            BuildPath = buildPath;
            ProjectName = projectName;
        }

        public override BuildState OnUpdate()
        {
            PlayerSettings.keyaliasPass = KeyAliasPass;
            PlayerSettings.keystorePass = KeyStorePass;

            Debug.LogWarning("Build Scenes : ");
            Debug.LogWarning("============================");
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
            Debug.LogWarning("============================");

            // projectName_yyyyMMddHHmm
            var newFileName = ProjectName + DateTime.Now.ToString("_yyyyMMddHHmm");
            var dir = Application.dataPath.Replace("/Assets", "");

            var path = string.Format("{0}/{1}.apk", BuildPath, newFileName);

            path = Path.Combine(dir, path);
            dir = Path.GetDirectoryName(path);

            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            EditorPrefs.SetString("BuildMechine.ProjectPath", path);

            var res = BuildPipeline.BuildPlayer(listScene.ToArray(), path, BuildTarget.Android, BuildOptions.None);

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