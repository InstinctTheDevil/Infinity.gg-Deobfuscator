using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static GHOSTDBDSTRINGDECRYPT.Protections.Strings;
using static System.Console;

namespace GHOSTDBDSTRINGDECRYPT
{
    internal class Program
    {
        public static int removedantidedot = 0;


  
    
        static void Main(string[] args)
        {
            Title = "StalkyGhostface tools deobfuscator";
            Write("path: ");
            string path = ReadLine();
            path = path.Replace("\"", string.Empty);
            ModuleDefMD module = ModuleDefMD.Load(path);
            ExecuteStringEnc(module);
            Protections.Junk.Execute(module);
           
            module.Write(path.Replace(".exe", "_decrypted.exe"));
            WriteLine("success");
            Console.ReadLine();
        }
    }
}
