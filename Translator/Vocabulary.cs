using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Translator
{
    class Vocabulary
    {

        public HashSet<string> GetVariables(string code)
        {
            var result = new HashSet<string>();

            int startVarBlock = code.IndexOf("var");
            int endVarBlock = code.IndexOf("begin");

            string varBlock = code.Substring(startVarBlock + 3, endVarBlock - (startVarBlock + 3) );
            varBlock = varBlock.Trim();

            string[] declarationLines = varBlock.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < declarationLines.Length; i++)
            {
                if (!declarationLines[i].Contains("integer"))
                {
                    throw new Exception("Data type expected!");                    
                }

                //int startTypeWord = declarationLines.
                //declarationLines[i].Remove(0, "integer".Length);
                declarationLines[i] = declarationLines[i].Substring(0, declarationLines[i].Length - "integer".Length);
                declarationLines[i] = declarationLines[i].Trim();
                declarationLines[i] = declarationLines[i].Replace(":", "");

                var variables = declarationLines[i].Split(',');
                foreach (var j in variables)
                {
                    //result.Add(j.Trim());
                    var tmp = j.Trim();
                    if (result.Contains(j))
                    {
                        throw new Exception("name already in use - " + tmp);
                    }

                    result.Add(tmp);
                }
            }

            return result;
        }

        public Dictionary<string, string> GetConstantString(string code)
        {
            var result = new Dictionary<string, string>();
            
            var pattern = @"('[^']*')";
            Regex reg = new Regex(pattern);

            var matches = reg.Matches(code);
            
            for (int i = 0; i < matches.Count; i++)
            {
                if (!result.ContainsKey(matches[i].Value))
                {
                    var str = matches[i].Value.Replace("'", "");
                    result.Add(str, "string" + i);
                }
            }

            return result;
        }
    }
}
