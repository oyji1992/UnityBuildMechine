using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_ProductName : BuildAction
    {
        public string ProductName;

        public BuildAction_ProductName()
        {

        }

        public BuildAction_ProductName(string productName)
        {
            this.ProductName = productName;
        }

        public override void OnEnter()
        {
            PlayerSettings.productName = ProductName;
            State = BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}