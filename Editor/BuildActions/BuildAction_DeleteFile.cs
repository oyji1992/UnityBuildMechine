using System.IO;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_DeleteFile : BuildAction
    {
        public string Path;

        public BuildAction_DeleteFile(string path)
        {
            Path = path;
        }

        public override BuildState OnUpdate()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
            else
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }
            }

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}