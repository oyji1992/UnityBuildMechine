using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_CompanyName : BuildAction
    {
        public string CompanyName;

        public BuildAction_CompanyName()
        {

        }

        public BuildAction_CompanyName(string companyName)
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