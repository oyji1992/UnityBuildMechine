using System;
using System.IO;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    /// <summary>
    /// Delete Dir/Files
    /// </summary>
    public class BuildAction_Delete : BuildAction
    {
        public string Path;

        public BuildAction_Delete(string path)
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
                else
                {
                    Context.Set(string.Format("Delete {0}", Path), "Fail");
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