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

        public BuildAction_BuildProjectAndroid(string buildPath, string keyAliasPass = "", string keyStorePass = "")
        {
            KeyAliasPass = keyAliasPass;
            KeyStorePass = keyStorePass;
            BuildPath = buildPath;
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
            var apkName = string.Format("{0}_build{1}_{2:yyyyMMddHHmm}.apk",
                PlayerSettings.productName,
                BuildHelper.GetBuildNum(),
                DateTime.Now);

            BuildHelper.AddBuildNum();

            var path = Path.Combine(BuildPath, apkName);

            var dir = Path.GetDirectoryName(path);

            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            EditorPrefs.SetString("BuildMechine.ProjectPath", path);

            var res = BuildPipeline.BuildPlayer(listScene.ToArray(), path, BuildTarget.Android, BuildOptions.None);

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