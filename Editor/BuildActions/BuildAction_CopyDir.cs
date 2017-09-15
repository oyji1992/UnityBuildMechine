using System;
using System.IO;

namespace UniGameTools.BuildMechine.BuildActions
{
    /// <summary>
    /// Copy Dir to Target Dir
    /// </summary>
    public class BuildAction_CopyDir : BuildAction
    {
        public string TargetPath;
        public string SrcPath;

        public BuildAction_CopyDir(string srcPath, string targetPath)
        {
            TargetPath = targetPath;
            SrcPath = srcPath;
        }

        public override BuildState OnUpdate()
        {
            CopyFolder(SrcPath, TargetPath);

            return BuildState.Success;
        }


        private void CopyFolder(string src, string des)
        {
            if (Directory.Exists(src) == false)
            {
                Context.Set("Error", "Directory not found : " + src);
                throw new Exception("Directory not found : " + src);
            }

            if (Directory.Exists(des) == false)
            {
                Directory.CreateDirectory(des);
            }

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

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}