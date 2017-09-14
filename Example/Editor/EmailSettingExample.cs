using System.Collections.Generic;
using UniGameTools.BuildMechine;
using UniGameTools.BuildMechine.BuildActions;

public class EmailSettingExample
{
    private static BuildAction_Email.EmailActionSetting Default = new BuildAction_Email.EmailActionSetting()
    {
        MyAddress = "buildmechine@126.com",
        smtpServer = "smtp.126.com",
        smtpPort = 25, // SSL:(465、994) / 25
        Subject = "${projectname} - #${buildnum} - Start",
        Body = "${mechinestate}",
        CredentialsAddress = "buildmechine@126.com",
        CredentialsPassword = "buildmechine000",
        TargetAddress = new List<string>()
        {
            // "xxxx@xxx.com" // myemail
        }
    };

    public static BuildAction_Email EmailBuildStart = new BuildAction_Email(new BuildAction_Email.EmailActionSetting(Default)
    {
        Subject = "${projectname} - #${buildnum} - Start",
        Body = "${mechinestate}",
    });

    public static BuildAction_Email EmailBuildSuccess = new BuildAction_Email(new BuildAction_Email.EmailActionSetting(Default)
    {
        Subject = "${projectname} - #${buildnum} - Finished",
        Body = "${log}",
    });

    public static BuildAction EmailBuildFail = new BuildAction_Email(new BuildAction_Email.EmailActionSetting(Default)
    {
        Subject = "${projectname} - #${buildnum} - Build Fail",
        Body = "Error Message : ${error}\n${log}",
    });
}