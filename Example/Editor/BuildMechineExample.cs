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
        BuildMechine.SetPipeline_BatchMode(
            new BuildAction_Print("Start Build Mechine"),
            new BuildAction_ForceCompilingScript(),
            // new BuildAction_WaitScriptCompile(),
            new BuildAction_Print("Build 1"),
            new BuildAction_ForceCompilingScript(),
            // new BuildAction_WaitScriptCompile(),
            // new BuildAction_Error(),
            new BuildAction_Print("Build 2"),
            new BuildAction_IncreaseBuildNum()
        );
    }
}