using System;
using System.IO;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_CopyFile : BuildAction
    {
        public string Source;
        public string Target;

        public BuildAction_CopyFile(string src, string target)
        {
            Source = src;
            Target = target;
        }

        public override BuildState OnUpdate()
        {
            if (!File.Exists(Source))
            {
                Context.Set("Error", "File not found : " + Source);
                return BuildState.Failure;
            }

            File.Copy(Source, Target, true);

            return BuildState.Success;
        }
    }
}