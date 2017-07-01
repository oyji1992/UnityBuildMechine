using System.IO;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_CopyFile : BuildAction
    {
        public string TargetPath;
        public string SrcPath;

        public BuildAction_CopyFile(string srcPath, string targetPath)
        {
            TargetPath = targetPath;
            SrcPath = srcPath;
        }

        public override BuildState OnUpdate()
        {
            var dir = Application.dataPath.Replace("/Assets", "");
            var src = Path.Combine(dir, SrcPath);
            var des = Path.Combine(dir, TargetPath);

            CopyFolder(src, des);
            return BuildState.Success;
        }


        private void CopyFolder(string src, string des)
        {
            Directory.CreateDirectory(des);

            if (Directory.Exists(src))
            {
                var files = Directory.GetFiles(src);
                foreach (var file in files)
                {
                    var name = Path.GetFileName(file);
                    File.Copy(file, des + "/" + name, true);
                }

                var dirs = Directory.GetDirectories(src);
                foreach (var dir in dirs)
                {
                    var name = Path.GetFileName(dir);
                    var desDir = des + "/" + name;

                    CopyFolder(dir, desDir);
                }
            }
            else
            {
                if (!File.Exists(src))
                {
                    return;
                }

                File.Copy(src, des, true);
            }
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}