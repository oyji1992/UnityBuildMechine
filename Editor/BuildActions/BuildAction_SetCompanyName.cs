using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetCompanyName : BuildAction
    {
        public string CompanyName;

        public BuildAction_SetCompanyName()
        {

        }

        public BuildAction_SetCompanyName(string companyName)
        {
            this.CompanyName = companyName;
        }

        public override BuildState OnUpdate()
        {
            PlayerSettings.companyName = CompanyName;
            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}