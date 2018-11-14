using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {
            Vocabulary v = new Vocabulary();

            StreamReader sr = new StreamReader("..\\..\\Inputs\\input.pas");
            var inputCode = sr.ReadToEnd();
            sr.Close();

            StreamReader headerStream = new StreamReader("..\\..\\Inputs\\header.txt");
            StreamReader bottomStream = new StreamReader("..\\..\\Inputs\\bottom.txt");

            string header = headerStream.ReadToEnd();
            string bottom = bottomStream.ReadToEnd();

            headerStream.Close();
            bottomStream.Close();
            
            var variables = v.GetVariables(inputCode);
            var strings = v.GetConstantString(inputCode);

            StreamWriter sw = new StreamWriter("..\\..\\Output\\output.asm");            

            sw.WriteLine(header);

            foreach (var i in variables)
            {
                sw.WriteLine(i + " dd ?");
            }

            foreach (var i in strings)
            {
                sw.WriteLine(i.Value + " db '" + i.Key + "',0");
            }

            sw.WriteLine(@"section '.code' code readable executable
start:");

            var qw = v.GetOperators(inputCode);

            foreach (var i in qw)
            {
                sw.WriteLine(i);
            }

            sw.WriteLine(bottom);

            sw.Close();

            //v.GetOperators(inputCode);
        }
    }
}
