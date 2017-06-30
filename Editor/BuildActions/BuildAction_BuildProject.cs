using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_BuildProject : BuildAction
    {
        public override void Build()
        {
            //        var listScene = new List<string>();
            //        for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            //        {
            //            var s = EditorBuildSettings.scenes[i];
            //            if (s.enabled)
            //            {
            //                listScene.Add(s.path);
            //            }
            //        }

            //        NimaHelper.JsonLog("打包的场景", listScene);

            //        var Asset = AssetFactory.GetAssets<AkiBuildSetting>();

            //        if (Asset.BuildTarget == BuildTarget.Android)
            //        {
            //---------------BuildNumber.txt记录-----------------//
            BuildHelper.AddBuildNum();
            var buildNumber = BuildHelper.GetBuildNum();


            //            //---------------获得apk名字-----------------//
            Debug.Log("当前Build版本：" + buildNumber.ToString());
            //            Debug.Log("工作目录：" + Directory.GetCurrentDirectory());
            //            var s = PackageSetting.Load()[PackageSetting.BuildSettingKey_SdkScenePostfix];
            //            var newFileName = string.Format("{0}_v{1}_build{2}_{4}_{3}.apk",
            //                PlayerSettings.productName,
            //                PlayerSettings.bundleVersion,
            //                buildNumber,
            //                DateTime.Now.ToString("yyyyMMddHHmm"),
            //                s
            //                );
            //
            //
            //            //---------------打包-----------------//
            //            var path = string.Format("Build/{0}", newFileName);
            //            Debug.Log("打包路径" + path);
            //
            //            PlayerSettings.keystorePass = "123456";
            //            PlayerSettings.keyaliasPass = "123456";
            //
            //            BuildPipeline.BuildPlayer(listScene.ToArray(), path, BuildTarget.Android, BuildOptions.None);
            //
            //        }
            //        else if (Asset.BuildTarget == BuildTarget.iOS)
            //        {
            //            var path = string.Format("IOSBuild/IOS_nima_v{1}", PlayerSettings.productName, PlayerSettings.bundleVersion);
            //
            //            Debug.Log("打包路径" + path);
            //
            //            BuildPipeline.BuildPlayer(listScene.ToArray(), path, BuildTarget.iOS, BuildOptions.None);
            //        }
            //        else
            //        {
            //            Debug.Log("暂不支持打包该平台。请手动打包。 " + Asset.BuildTarget);
            //        }

            this.State = BuildState.Succeed;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}