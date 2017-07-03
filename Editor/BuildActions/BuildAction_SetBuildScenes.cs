using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetBuildScenes : BuildAction
    {
        public string[] sceneNames;

        public BuildAction_SetBuildScenes(params string[] sceneNames)
        {
            this.sceneNames = sceneNames;
        }

        public override BuildState OnUpdate()
        {
            if (SetBuildScenes(sceneNames) == false)
            {
                return BuildState.Failure;
            }

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }

        public static bool SetBuildScenes(params string[] sceneNames)
        {
            if (sceneNames == null)
            {
                Debug.LogError("Empty Scenes List");
                return false;
            }

            var scenes = EditorBuildSettings.scenes;

            var missingScenes = sceneNames.Where(r => scenes.Any(s => CompareScenes(s.path, r)) == false).ToList();

            if (missingScenes.Count > 0)
            {
                foreach (var missingScene in missingScenes)
                {
                    Debug.LogError("Miss Scene in BuildSetting : " + missingScene);
                }

                return false;
            }

            for (int index = 0; index < scenes.Length; index++)
            {
                var scene = scenes[index];
                scene.enabled = false;

                if (sceneNames.Any(r => CompareScenes(scene.path, r)))
                {
                    scene.enabled = true;
                }
            }

            EditorBuildSettings.scenes = scenes;

            return true;
        }

        private static bool CompareScenes(string path, string sceneName)
        {
            if (path.EndsWith(string.Format("{0}.unity", sceneName), StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}