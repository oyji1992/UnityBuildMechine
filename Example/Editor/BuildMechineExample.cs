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

        BuildMechine.SetPipeline(
            new BuildAction_Print("Start Build Mechine"),
            new BuildAction_Print("Building"),
            new BuildAction_ForceCompilingScript(),
            new BuildAction_WaitScriptCompile(),
            new BuildAction_Print("Build Finished")
        );
    }
}