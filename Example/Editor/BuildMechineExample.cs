using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UniGameTools.BuildMechine;
using UniGameTools.BuildMechine.BuildActions;

public class BuildMechineExample
{
    [MenuItem("Tools/BuildMechine/Example")]
    public static void Build()
    {
        BuildMechine.NewPipeline()
            .SetOnFailure(EmailSettingExample.EmailBuildFail)
            .AddActions(EmailSettingExample.EmailBuildStart)
            //            .AddActions(new BuildAction_SetScriptingDefineSymbols(BuildTargetGroup.Standalone, "FancyDream", "Wait"))
            //            .AddActions(new BuildAction_SetScriptingDefineSymbols(BuildTargetGroup.Standalone, "FancyDream", "Wait2"))
            //            .AddActions(new BuildAction_SetScriptingDefineSymbols(BuildTargetGroup.Standalone, "FancyDream", "Wait3"))
            .AddActions(new BuildAction_CommandLine(@"Assets\BuildMechine\Example\Bat\mkdir.bat", Directory.GetCurrentDirectory()))
            .AddActions(new BuildAction_CommandLine(@"Assets\BuildMechine\Example\Bat\error.bat", Directory.GetCurrentDirectory()))
            .AddActions(EmailSettingExample.EmailBuildSuccess)
            //            .AddActions(new BuildAction_SetScriptingDefineSymbols(BuildTargetGroup.Standalone, "Wait"))
            //            .AddActions(new BuildAction_Print("Start Build Mechine"))
            //            .AddActions(new BuildAction_SetScriptingDefineSymbols(BuildTargetGroup.Standalone, "Wait2"))
            //            .AddActions(new BuildAction_Print("Start Build Mechine"))
            //            .AddActions(new BuildAction_SetScriptingDefineSymbols(BuildTargetGroup.Standalone, "Wait3"))
            //            .AddActions(new BuildAction_Print("Start Build Mechine"))
            //            .AddActions(new BuildAction_IncreaseBuildNum())
            //            .AddActions(new BuildAction_SaveAndRefresh(),
            //                        new BuildAction_SetBundleId("cn.test.test")
            //new BuildAction_BuildProjectAndroid("Build/"),
            //new BuildAction_BuildProjectWindowsStandalone("game", "Build/Windows/", x64: false),
            //new BuildAction_BuildProjectWindowsStandalone("game", "Build/Windows/", x64: true)
            //                        )
            .Run();
    }
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