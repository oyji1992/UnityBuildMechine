using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_ForceCompilingScript : BuildAction
    {
        public override BuildState OnUpdate()
        {
            MonoScript cMonoScript = null;

            foreach (var script in MonoImporter.GetAllRuntimeMonoScripts())
            {
                var assetPath = AssetDatabase.GetAssetPath(script);

                if (assetPath.StartsWith("Assets"))
                {
                    Debug.Log("Reimport : " + assetPath);
                    cMonoScript = script;
                    break;
                }
            }

            if (cMonoScript != null)
            {
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(cMonoScript));
            }
            else
            {
                Debug.LogError("No found any runtime script to reimport");
            }

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}