using System;
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
        public string KeyAliasName;
        public string KeyStorePath;

        public BuildAction_BuildProjectAndroid(string projectName, string buildPath, string keyStorePath = "", string keyAliasName = "", string keyAliasPass = "", string keyStorePass = "")
        {
            this.KeyStorePath = keyStorePath;
            this.KeyAliasName = keyAliasName;
            ProjectName = projectName;
            KeyAliasPass = keyAliasPass;
            KeyStorePass = keyStorePass;
            BuildPath = buildPath;
        }

        public override BuildState OnUpdate()
        {
            PlayerSettings.Android.keystoreName = KeyStorePath;
            PlayerSettings.Android.keyaliasName = KeyAliasName;
            PlayerSettings.Android.keyaliasPass = KeyAliasPass;
            PlayerSettings.Android.keystorePass = KeyStorePass;

            PlayerSettings.Android.bundleVersionCode = BuildHelper.GetBuildNum();

            var listScene = BuildHelper.GetAllScenesInBuild();

            // projectName_yyyyMMddHHmm
            var apkName = string.Format("{0}_build{1}_{2:yyyyMMddHHmm}.apk",
                ProjectName,
                BuildHelper.GetBuildNum(),
                DateTime.Now);

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