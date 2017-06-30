using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_ChangePlayerSetting : BuildAction
    {
        //    private PackageSetting _packSetting;

        public override void OnEnter()
        {
            Debug.Log("加载PackageSetting文件");
            //        _packSetting = PackageSettingUtil.LoadFile();

            Debug.Log("更改bundleIdentifier");
            //        PlayerSettings.bundleIdentifier = _packSetting[PackageSetting.BuildSettingKey_BuildIdentify];

            Debug.Log("更改productName");
            //        PlayerSettings.productName = _packSetting[PackageSetting.BuildSettingKey_PackageName];

            Debug.Log("切换平台");
            //        var buildTarget = SwithBuildTarget(_packSetting[PackageSetting.BuildSettingKey_BuildTarget]);
            //        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

            Debug.Log("更改AppIcon");
            //        var buildTargetGroup = SwithBuildTargetGroup(_packSetting[PackageSetting.BuildSettingKey_BuildTarget]);
            //        var iconPath = _packSetting[PackageSetting.BuildSettingKey_PackageIcon];
            //        var tex = (Texture2D)AssetDatabase.LoadAssetAtPath(iconPath, typeof(Texture2D));
            //        var count = PlayerSettings.GetIconSizesForTargetGroup(buildTargetGroup).Length;

            //        List<Texture2D> textures = new List<Texture2D>();
            //        for (int i = 0; i < count; i++)
            //        {
            //            textures.Add(tex);
            //        }
            //        PlayerSettings.SetIconsForTargetGroup(buildTargetGroup, textures.ToArray());

            Debug.Log("更改场景设置");
            //
            //        var list = new List<string>()
            //        {
            //            "AdviceScene.unity",
            //            "LogoScene.unity",
            //            "LoginSceneNimaPortrait.unity",
            //            "EmptyScene.unity"
            //        };
            //
            //        var scenes = EditorBuildSettings.scenes;
            //
            //
            //        for (int index = 0; index < scenes.Length; index++)
            //        {
            //            var scene = scenes[index];
            //            scene.enabled = false;
            //
            //            if (list.Any(r => scene.path.EndsWith(r, StringComparison.OrdinalIgnoreCase)))
            //            {
            //                scene.enabled = true;
            //                Debug.Log(scene.path);
            //            }
            //        }
            //
            //        var sdkScenePrefixName = "SDKScene_";
            //        var needSceneName = sdkScenePrefixName + _packSetting[PackageSetting.BuildSettingKey_SdkScenePostfix];
            //        foreach (var buildScene in scenes)
            //        {
            //            if (buildScene.path.Contains(sdkScenePrefixName))
            //            {
            //                buildScene.enabled = buildScene.path.Contains(needSceneName);
            //            }
            //        }
            //        EditorBuildSettings.scenes = scenes;
            //        EditorApplication.SaveAssets();


            AssetDatabase.Refresh();
            Debug.Log("更改PlayerSettings成功");

            State = BuildState.Success;
        }

        BuildTarget SwithBuildTarget(string target)
        {
            var str = target.ToLower();
            switch (str)
            {
                case "android":
                    return BuildTarget.Android;
                case "iphone":
                case "ipad":
                case "ios":
#if UNITY_4
                return BuildTarget.iPhone;
#elif UNITY_5
                    return BuildTarget.iOS;
#endif
                default:
                    return BuildTarget.Android;
            }
        }


        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}
