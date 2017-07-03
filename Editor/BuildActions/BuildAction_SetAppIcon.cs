using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    /// <summary>
    /// 设置图标
    /// </summary>
    public class BuildAction_SetAppIcon : BuildAction
    {
        public BuildTargetGroup Group;
        public string Path;

        public BuildAction_SetAppIcon(string path, BuildTargetGroup group)
        {
            Group = @group;
            this.Path = path;
        }

        public override BuildState OnUpdate()
        {
            var tex = (Texture2D)AssetDatabase.LoadAssetAtPath(Path, typeof(Texture2D));

            if (tex == null)
            {
                Debug.LogError("Icon Not Found : " + Path);
                return BuildState.Failure;
            }

            var count = PlayerSettings.GetIconSizesForTargetGroup(this.Group).Length;

            var textures = new List<Texture2D>();
            for (int i = 0; i < count; i++)
            {
                textures.Add(tex);
            }

            PlayerSettings.SetIconsForTargetGroup(this.Group, textures.ToArray());

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}