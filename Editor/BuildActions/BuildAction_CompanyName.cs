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

        public override void Build()
        {
            PlayerSettings.companyName = CompanyName;
            State = BuildState.Succeed;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}