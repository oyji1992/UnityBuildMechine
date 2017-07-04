using System.Text;
using UnityEditor;

namespace UniGameTools.BuildMechine.BuildActions
{
    public class BuildAction_SetScriptingDefineSymbols : BuildAction
    {
        public string[] Symbols;
        public BuildTargetGroup BuildTargetGroup;

        public BuildAction_SetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup, params string[] symbols)
        {
            this.Symbols = symbols;
            this.BuildTargetGroup = buildTargetGroup;
        }

        public override BuildState OnUpdate()
        {
            if (this.Symbols == null)
            {
                this.Symbols = new[] { "" };
            }

            var sb = new StringBuilder();

            foreach (var symbol in this.Symbols)
            {
                sb.Append(symbol + ";");
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(this.BuildTargetGroup, sb.ToString());

            return BuildState.Success;
        }

        public override BuildProgress GetProgress()
        {
            return null;
        }
    }
}