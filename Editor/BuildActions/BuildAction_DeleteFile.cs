using System.IO;
using UnityEngine;

namespace GameEditor
{
    public class BuildAction_DeleteFile : BuildAction
    {
        public string FilePath;

        public BuildAction_DeleteFile(string filePath)
        {
            FilePath = filePath;
        }

        public override void Build()
        {
            var dir = Application.dataPath.Replace("/Assets", "");
            var path = Path.Combine(dir, FilePath);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                File.Delete(Path.Combine(dir, FilePath));
            }

            this.State = BuildState.Succeed;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}