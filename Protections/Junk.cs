using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace GHOSTDBDSTRINGDECRYPT.Protections
{
    internal class Junk
    {
        public static IList<TypeDef> ts = null;
        public static IList<TypeDef> lmao = null;

        public static void addtolist(TypeDef typo)
        {
            if (ts == null)
            {
                ts = new List<TypeDef>();
            }
            ts.Add(typo);
        }
        public static int removedantidedot;
        public static void Execute(ModuleDefMD moduleDefMD) {
            KillFakeAttribs(moduleDefMD);
            KillJunkTypes(moduleDefMD);
           RemoveAntiDe4Dot(moduleDefMD);
        
        }

        public static void KillJunkTypes(ModuleDefMD moduleDefMD)
        {
            ts.Clear();
            foreach (var t in moduleDefMD.Types)
            {
                if (t.Namespace.StartsWith("俺ム仮"))
                {

                    WriteLine("Found Junk");
                    addtolist(t);
                }
            }
            foreach (var t in ts)
            {
                moduleDefMD.Types.Remove(t);

            }
        }
        public static void KillFakeAttribs(ModuleDefMD moduleDefMD)
        {
            List<string> fakeObfuscators = new List<string>
            {
                "DotNetPatcherObfuscatorAttribute", "DotNetPatcherPackerAttribute", "DotfuscatorAttribute", "ObfuscatedByGoliath", "dotNetProtector", "PoweredByAttribute", "AssemblyInfoAttribute", "BabelAttribute", "CryptoObfuscator.ProtectedWithCryptoObfuscatorAttribute", "Xenocode.Client.Attributes.AssemblyAttributes.ProcessedByXenocode",
                "NineRays.Obfuscator.Evaluation", "YanoAttribute", "SmartAssembly.Attributes.PoweredByAttribute", "NetGuard", "SecureTeam.Attributes.ObfuscatedByCliSecureAttribute", "Reactor", "ZYXDNGuarder", "CryptoObfuscator", "MaxtoCodeAttribute", ".NETReactorAttribute",
                "BabelObfuscatorAttribute"
            };
            foreach (TypeDef type in moduleDefMD.Types)
            {
                bool IsFake = fakeObfuscators.Contains(type.Namespace);
                if (IsFake)
                {
                    addtolist(type);
                    Console.WriteLine("found fake attrib");
                }
            }
            foreach (var t in ts)
            {
                moduleDefMD.Types.Remove(t);
            }


        }
        public static void RemoveAntiDe4Dot(ModuleDefMD moduleDefMD)
        {
            foreach (TypeDef type in (from t in moduleDefMD.GetTypes()
                                      where t.FullName.Contains("Form") && t.HasInterfaces && t.Interfaces.Count == 2
                                      select t).ToArray<TypeDef>())
            {
                moduleDefMD.Types.Remove(type);
                removedantidedot++;
            }

        }
    }
}
