using System;
using System.Collections.Generic;
using UnityEditor;
using UniGameTools.BuildMechine;
using UniGameTools.BuildMechine.BuildActions;
using UnityEngine;

public class BuildMechineExample
{
    [MenuItem("Tools/BuildMechine/Example")]
    public static void Build()
    {
        BuildMechine.NewPipeline()
            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            //            .AddActions(Actions)
            .Run();
    }

    public static BuildAction[] Actions = new BuildAction[]
    {
        new BuildAction_Print("Start Build Mechine"),
        // new BuildAction_ForceCompilingScript(),
        // new BuildAction_WaitScriptCompile(),
        // new BuildAction_Print("Build 1"),
        // new BuildAction_ForceCompilingScript(),
        // new BuildAction_WaitScriptCompile(),
        // new BuildAction_Error(),
        // new BuildAction_Print("Build 2"),
        new BuildAction_IncreaseBuildNum(),
        new BuildAction_Delete("Assets/SDK2"),
        new BuildAction_CopyDir("Assets/SDK", "Assets/SDK2"),
        new BuildAction_CopyFile("Assets/SDK/123.txt", "Assets/321.txt"),
        new BuildAction_Delete("Assets/321.txt"),
        new BuildAction_Delete("Assets/SDK2"),
        new BuildAction_SaveAndRefresh(),
        new BuildAction_SetBundleId("cn.test.test"), 
        new BuildAction_BuildProjectAndroid("Build/"), 
        new BuildAction_Print("Build Succeed"), 
    };

    private class BuildAction_Error : BuildAction
    {
        public override BuildState OnUpdate()
        {
            return BuildState.Failure;
        }
    }

    private class BuildAction_Exception : BuildAction
    {
        public override BuildState OnUpdate()
        {
            throw new NotImplementedException();
        }
    }
}