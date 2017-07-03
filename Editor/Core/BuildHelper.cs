using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    public static class BuildHelper
    {
        public static void SetBuildScenes(params string[] sceneNames)
        {
            if (sceneNames == null)
            {
                return;
            }

            var scenes = EditorBuildSettings.scenes;

            for (int index = 0; index < scenes.Length; index++)
            {
                var scene = scenes[index];
                scene.enabled = false;

                if (sceneNames.Any(r => scene.path.Replace(".unity", "").EndsWith(r, StringComparison.OrdinalIgnoreCase)))
                {
                    scene.enabled = true;

                }
            }

            EditorBuildSettings.scenes = scenes;
        }


        public static string GetProjectDir()
        {
            return Application.dataPath.Remove(Application.dataPath.Length - 6, 6);
        }

        public static string GetDataPath(string path)
        {
            var filename = Path.GetFileNameWithoutExtension(path);
            var directory = Path.GetDirectoryName(path);

            var result = Path.Combine(directory ?? "", filename ?? "");
            result += "_Data";

            return result;
        }

        public static void SetAppIcon(string path)
        {
            const BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone;

            var tex = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));

            var count = PlayerSettings.GetIconSizesForTargetGroup(buildTargetGroup).Length;

            var textures = new List<Texture2D>();
            for (int i = 0; i < count; i++)
            {
                textures.Add(tex);
            }

            PlayerSettings.SetIconsForTargetGroup(buildTargetGroup, textures.ToArray());
        }


        public static void SetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup,
            string[] symbols)
        {
            var sb = new StringBuilder();

            foreach (var symbol in symbols)
            {
                sb.Append(symbol + ";");
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, sb.ToString());
        }

        /// <summary>
        /// 增加当前的版本号
        /// </summary>
        public static void AddBuildNum()
        {
            var buildFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            var buildRecordFile = Path.Combine(buildFilePath, "BuildNumber.txt");
            var buildNumber = 0;
            if (!File.Exists(buildRecordFile))
            {
                File.WriteAllText(buildRecordFile, buildNumber.ToString());
            }

            buildNumber = Convert.ToInt32(File.ReadAllText(buildRecordFile).Trim());
            buildNumber++;

            File.WriteAllText(buildRecordFile, buildNumber.ToString());

            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 获得当前的版本号
        /// </summary>
        /// <returns></returns>
        public static int GetBuildNum()
        {
            var buildFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
            var buildRecordFile = Path.Combine(buildFilePath, "BuildNumber.txt");
            var buildNumber = 0;
            if (!File.Exists(buildRecordFile))
            {
                File.WriteAllText(buildRecordFile, buildNumber.ToString());
            }

            buildNumber = Convert.ToInt32(File.ReadAllText(buildRecordFile).Trim());

            return buildNumber;
        }

        public static TimeSpan CalDuration(long nowTick, long startTime, long endTime)
        {
            if (startTime == endTime) return TimeSpan.Zero;

            if (startTime > endTime)
            {
                return TimeSpan.FromTicks(nowTick - startTime);
            }
            return TimeSpan.FromTicks(endTime - startTime);
        }
    }
}