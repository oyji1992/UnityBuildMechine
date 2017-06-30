using UnityEditor;

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

    public override void Build()
    {
        PlayerSettings.productName = ProductName;
        State = BuildState.Succeed;
    }

    public override BuildProgress GetProgress()
    {
        return null;
    }
}