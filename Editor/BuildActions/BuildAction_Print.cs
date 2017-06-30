using UnityEngine;

public class BuildAction_Print : BuildAction
{
    public string Msg;

    public BuildAction_Print(string msg)
    {
        this.Msg = msg;
    }

    public override void Build()
    {
        Debug.Log(Msg);
        this.State = BuildState.Succeed;
    }

    public override BuildProgress GetProgress()
    {
        return null;
    }
}