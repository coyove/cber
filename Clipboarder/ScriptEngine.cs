using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Clipboarder
{
    public class ScriptEngine
    {
        const string CodeWrapper = @"
using System;
using System.Drawing;

namespace CBerScriptSpace {{
    class Rule {{
        public static char Exec(ref string rawText, ref string html, ref Image image, ref string url) {{
            {0}
        }}
    }}
}}
";

        private MethodInfo mExec;

        public void Compile(string code)
        {
            CompilerParameters CompilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
                GenerateExecutable = false,
                CompilerOptions = "/optimize",
            };

            CompilerParams.ReferencedAssemblies.AddRange(new string[] { "System.dll", "System.Drawing.dll" });

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams,
                string.Format(CodeWrapper, code));

            if (compile.Errors.HasErrors)
            {
                string text = "Failed to compile the rule: ";
                foreach (CompilerError ce in compile.Errors) text += Environment.NewLine + ce.ToString();
                MessageBox.Show(text);
                return;
            }

            Module module = compile.CompiledAssembly.GetModules()[0];
            mExec = module?.GetType("CBerScriptSpace.Rule")?.GetMethod("Exec");

            if (mExec == null) throw new Exception("shouldn't happen");
        }

        public char Exec(ref string rawText, ref string html, ref Image image, ref string url)
        {
            if (mExec == null) return '\0';
            var args = new object[] { rawText, html, image, url };
            char ans = '\0';
            try
            {
                ans = (char)mExec.Invoke(null, args);
            }
            catch (TargetInvocationException e)
            {
                MessageBox.Show("Exception from the rule script:" + Environment.NewLine +
                    e.InnerException.ToString());
                return ans;
            }
            rawText = args[0] as string;
            html = args[1] as string;
            image = args[2] as Image;
            url = args[3] as string;
            return ans;
        }

    }
}
