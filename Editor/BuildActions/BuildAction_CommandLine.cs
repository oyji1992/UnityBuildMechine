using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_CommandLine : BuildAction
    {
        public string Filename;
        public string WorkDir;
        public string Args;

        private static bool ExecuteProgram(string filename, string workDir, string args = "")
        {
            // http://stackoverflow.com/questions/5255086/when-do-we-need-to-set-useshellexecute-to-true

            var info = new ProcessStartInfo
            {
                FileName = filename,
                WorkingDirectory = workDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = args,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
                StandardOutputEncoding = Encoding.Default,
                StandardErrorEncoding = Encoding.Default
            };

            Process process = null;

            var rt = true;

            try
            {
                process = Process.Start(info);
                if (process != null)
                {
                    process.WaitForExit();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
                return false;
            }
            finally
            {
                if (process != null && process.HasExited)
                {
                    var output = process.StandardError.ReadToEnd();
                    if (output.Length > 0)
                    {
                        UnityEngine.Debug.LogError(output);
                    }

                    output = process.StandardOutput.ReadToEnd();
                    if (output.Length > 0)
                    {
                        UnityEngine.Debug.LogError(output);
                    }

                    rt = (process.ExitCode == 0);
                }
            }

            return rt;
        }

        public BuildAction_CommandLine(string filename, string workDir, string args = "")
        {
            this.Filename = filename;
            this.WorkDir = workDir;
            this.Args = args;
        }

        public override BuildState OnUpdate()
        {
            if (ExecuteProgram(Filename, WorkDir, Args) == false)
            {
                return BuildState.Failure;
            }

            return BuildState.Success;
        }
    }
}