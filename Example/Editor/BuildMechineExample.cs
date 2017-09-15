using System;
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
            // Build Windows
            .AddActions(new BuildAction_BuildProjectWindowsStandalone("Normal", "game.exe", "Build/Windows/", x64: true))
            // Build Android
            .AddActions(new BuildAction_BuildProjectAndroid("Normal", "Build/", Path.Combine(Directory.GetCurrentDirectory(), "xxxx.keystore"), "xxxxx", "123456", "123456"))
            // Build IOS
            .AddActions(new BuildAction_BuildProjectIOS("normal", "build"))
            // Send Email
            .AddActions(EmailSettingExample.EmailBuildSuccess)
            // Terminal执行shell
            .AddActions(new BuildAction_CommandLine("/Applications/Utilities/Terminal.app/Contents/MacOS/Terminal", Path.Combine(Directory.GetCurrentDirectory(), "../Build"), "BuildAdHoc.sh"))
            // 命令行执行shell
            .AddActions(new BuildAction_CommandLine("/bin/sh", Path.Combine(Directory.GetCurrentDirectory(), "../Build"), "BuildAdHoc.sh"))
            // Bat
            .AddActions(new BuildAction_CommandLine(@"Assets\BuildMechine\Example\Bat\mkdir.bat", Directory.GetCurrentDirectory()))

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