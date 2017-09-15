using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetIOSAutoSign : BuildAction
    {
        public bool AppleEnableAutomaticSigning;
        public string AppleDeveloperTeamId;

        public BuildAction_SetIOSAutoSign(bool autoSign, string teamId)
        {
            AppleEnableAutomaticSigning = false;
            AppleDeveloperTeamId = teamId;
        }

        public override BuildState OnUpdate()
        {
            PlayerSettings.iOS.appleEnableAutomaticSigning = AppleEnableAutomaticSigning;
            PlayerSettings.iOS.appleDeveloperTeamID = AppleDeveloperTeamId;


            return BuildState.Success;
        }
    }
}