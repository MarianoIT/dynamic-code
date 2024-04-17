using Microsoft.CSharp;

using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace DynamicCodeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();

            //Agregar todas las referencias necesarias para ejecutar el codigo
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Linq.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("System.Data.dll");
            cp.ReferencedAssemblies.Add("System.Xml.dll");
            cp.ReferencedAssemblies.Add("mscorlib.dll");

            cp.GenerateExecutable = true;
            cp.GenerateInMemory = true;

            var source = Encoding.ASCII.GetString(Convert.FromBase64String(Code.value));

            CompilerResults cr = provider.CompileAssemblyFromSource(cp, source);

            if (cr.Errors.Count > 0)
            {
                Console.WriteLine("Errors building {0} into {1}", source, cr.PathToAssembly);
                foreach (CompilerError ce in cr.Errors)
                {
                    Console.WriteLine("  {0}", ce.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Source built successfully.");

                Assembly assembly = cr.CompiledAssembly;
                assembly.EntryPoint.Invoke(null, new object[] { new string[] { } });
            }

        }
    }
}
