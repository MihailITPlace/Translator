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
        private Dictionary<string, string> strings = null;
        private HashSet<string> variables = null;

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

            variables = result;
            return result;
        }

        public Dictionary<string, string> GetConstantString(string code)
        {
            var result = new Dictionary<string, string>();

            //var pattern = @"\'(.*?)\'";
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

            strings = result;
            return result;
        }

        public List<string> GetOperators(string code)
        {
            var result = new List<string>();

            int startOperBlock = code.IndexOf("begin");
            int endVarBlock = code.IndexOf("end.");

            string operatorsBlock = code.Substring(startOperBlock + "begin".Length,
                endVarBlock - (startOperBlock + "begin".Length));


            var operators = operatorsBlock.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var i in operators)
            {
                var lineOperator = i.Trim();

                if (lineOperator == string.Empty)
                {
                    continue;
                }

                if (lineOperator.Contains(":="))
                {
                    result.Add(ParseAssignment(lineOperator));
                    //throw new NotImplementedException();
                    //continue;
                }

                string nameOperator = lineOperator.Substring(0, lineOperator.IndexOf('('));

                switch (nameOperator)
                {
                    case "write":
                        result.Add(ParseWrite(lineOperator, false));
                        break;
                    case "readln":
                        result.Add(ParseReadln(lineOperator));
                        break;
                    case "writeln":
                        result.Add(ParseWrite(lineOperator, true));
                        break;
                    default:
                        throw new Exception("Unknow name " + lineOperator);                        
                }
            }
            

            return result;
        }

        private string ParseAssignment(string lineOperator)
        {
            throw new NotImplementedException();
        }

        private string ParseReadln(string lineOperator)
        {
            //throw new NotImplementedException();
            Regex reg = new Regex(@"\((.*?)\)");
            var match = reg.Match(lineOperator);

            var tmp = match.Value.Replace("(", "");
            tmp = tmp.Replace(")", "");
            string result = "cinvoke scanf, '%d', " + tmp;
            return result;
        }

        private string ParseWrite(string lineOperator, bool newLine)
        {
            throw new NotImplementedException();
        }
    }
}
