using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace UniGameTools.BuildMechine.BuildActions
{
    /// <summary>
    /// 不支持SSL
    /// </summary>
    public class BuildAction_Email : BuildAction
    {
        [System.Serializable]
        public class EmailActionSetting
        {
            public EmailActionSetting()
            {
            }

            public EmailActionSetting(EmailActionSetting setting)
            {
                this.MyAddress = setting.MyAddress;
                this.TargetAddress = setting.TargetAddress;
                this.Subject = setting.Subject;
                this.Body = setting.Body;
                this.smtpServer = setting.smtpServer;
                this.smtpPort = setting.smtpPort;
                this.CredentialsPassword = setting.CredentialsPassword;
                this.CredentialsAddress = setting.CredentialsAddress;
            }

            public string MyAddress;
            public List<string> TargetAddress;
            public string Subject;
            public string Body;
            public string smtpServer;
            public int smtpPort;
            public string CredentialsAddress;
            public string CredentialsPassword;
            // public bool UseSSL;
        }

        public EmailActionSetting Settings;

        public BuildAction_Email(EmailActionSetting setting)
        {
            this.Settings = setting;
        }

        public override BuildState OnUpdate()
        {
            Mechine.Parse(ref this.Settings.Subject);
            Mechine.Parse(ref this.Settings.Body);

            var mail = new MailMessage();

            mail.From = new MailAddress(this.Settings.MyAddress);
            foreach (var address in this.Settings.TargetAddress)
            {
                mail.To.Add(address);
            }
            mail.Subject = this.Settings.Subject;
            mail.Body = this.Settings.Body;

            var smtpServer = new SmtpClient(this.Settings.smtpServer);
            smtpServer.Port = this.Settings.smtpPort;
            smtpServer.Credentials = new NetworkCredential(this.Settings.CredentialsAddress, this.Settings.CredentialsPassword);
            smtpServer.EnableSsl = false;

            smtpServer.Send(mail);

            return BuildState.Success;
        }
    }
}