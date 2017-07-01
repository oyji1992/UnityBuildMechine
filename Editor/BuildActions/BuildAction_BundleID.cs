using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_BundleID : BuildAction
    {
        public string BundleID;

        public BuildAction_BundleID()
        {

        }

        public BuildAction_BundleID(string id)
        {
            this.BundleID = id;
        }

        public override BuildState OnUpdate()
        {
#if UNITY_5_6_OR_NEWER
            PlayerSettings.applicationIdentifier = BundleID;
#else
            PlayerSettings.bundleIdentifier = BundleID;
            PlayerSettings.iPhoneBundleIdentifier = BundleID;
#endif
            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}