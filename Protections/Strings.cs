using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace GHOSTDBDSTRINGDECRYPT.Protections
{
    internal class Strings
    {
        public static void NopInstructions(params Instruction[] instructions)
        {
            foreach (var instruction in instructions)
                instruction.OpCode = OpCodes.Nop;
        }
        public static int countFixed = 0;
        public static void ExecuteStringEnc(ModuleDefMD moduleDefMD)
        {

            foreach (var typeDef in moduleDefMD.GetTypes())
            {
                foreach (var methodDef in typeDef.Methods.Where(x => x.HasBody && x.Body.Instructions.Count > 3))
                {
                    var instructions = methodDef.Body.Instructions;
                    for (int i = 0; i < instructions.Count; i++)
                    {

                        if (instructions[i].OpCode == OpCodes.Call
                            && instructions[i].Operand.ToString().Contains("Encoding::get_UTF8")
                            && instructions[i + 1].OpCode == OpCodes.Ldstr
                            && instructions[i + 2].OpCode == OpCodes.Call
                            && instructions[i + 3].OpCode == OpCodes.Callvirt)
                        {
                            Write("[+] detected string ");
                            string decryptedStr = Encoding.UTF8.GetString(Convert.FromBase64String(instructions[i + 1].Operand.ToString()));
                            instructions[i].OpCode = OpCodes.Ldstr;
                            instructions[i].Operand = decryptedStr;
                            NopInstructions(instructions[i + 1], instructions[i + 2], instructions[i + 3]);
                            Write("- decrypted string, value: " + decryptedStr + "\n");
                            countFixed++;
                        }
                    }
                }
            }
            WriteLine("decrypted " + countFixed);
        }
    }
}
