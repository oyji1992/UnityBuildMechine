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
            var buildRecordFile = Path.Combine("ProjectSettings", "BuildNumber.txt");

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
            var buildRecordFile = Path.Combine("ProjectSettings", "BuildNumber.txt");
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