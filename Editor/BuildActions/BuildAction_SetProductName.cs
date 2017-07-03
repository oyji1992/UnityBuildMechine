using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetProductName : BuildAction
    {
        public string ProductName;

        public BuildAction_SetProductName()
        {

        }

        public BuildAction_SetProductName(string productName)
        {
            this.ProductName = productName;
        }

        public override BuildState OnUpdate()
        {
            PlayerSettings.productName = ProductName;
            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}