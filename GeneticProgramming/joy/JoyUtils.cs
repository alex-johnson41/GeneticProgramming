using System.Diagnostics;
using System.Reflection;
using System.Text;


namespace GeneticProgramming
{
    public class JoyUtils
    {
        public static string RunJoy(string program)
        {
            string results = "";
            string joyFilePath = Path.GetTempFileName();
            File.WriteAllText(joyFilePath, program + Environment.NewLine);
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"" + GetProjectDirectory() + "/joy/joy_long_inp " + joyFilePath + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    results = reader.ReadToEnd();
                }
            }
            File.Delete(joyFilePath);
            return results;
        }

        public static string FormatProgram(string program, Dictionary<List<int>, float> testCases)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var testCase in testCases)
            {
                stringBuilder.Append("[] unstack ");
                foreach (var input in testCase.Key)
                {
                    stringBuilder.Append($"{input} ");
                }
                stringBuilder.Append($"{program}.\n");
            }
            return stringBuilder.ToString();
        }

        private static string GetProjectDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);
            return Path.GetDirectoryName(path);
        }

        //Generates a random Joy program
        public static List<string> RandomJoyProgram(int maxProgramLength)
        {
            Random random = new Random();
            List<string> program;
            while (true)
            {
                program = new List<string>();
                for (int i = 0; i <= random.Next(1, maxProgramLength + 1); i++) //Random length up to maxProgramLength
                {
                    program.Add(RandomJoyKeywordOrInt());
                }
                program.Add(RandomJoyKeywordOrInt(0)); //Ensure last element is a keyword, not number
                if (!SyntaxErrors(program))
                    break;
            }
            return program;
        }

        public static string RandomJoyKeywordOrInt(float numberRate = 0.3f)
        {
            string[] joyKeywords = {
                "+", "-", "*", "/", "pow", "neg", "dup", "swap", "sqrt",
                "[", "]", "map", "concat", "rollup", "rotate", "rem",
                "rolldown", "rollupd", "abs", "ceil", "floor", "max", "min",
                "stack", "popd", "choice", "or", "xor", "and", "not", "ifte",
                "div", "sign", "max", "min", "first", "rest", "cons", "swons",
                "at", "of", "size", "small", "null", ">=", "<=", "=", ">", "<",
                "!=", "has", "in", "branch"
            };
            string[] zeroToNine = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random random = new Random();
            double randomNumber = random.NextDouble(); 
            if (randomNumber <= numberRate) 
                return zeroToNine[random.Next(0, zeroToNine.Length)];
            else
                return joyKeywords[random.Next(0, joyKeywords.Length)];
        }

        // Returns true if the program has unmatched brackets or the last token is a number
        // (When the last token is a number, no output is produced as the last action was pushing to the stack)
        public static bool SyntaxErrors(List<string> program)
        {
            int bracketCount = 0;
            foreach (string token in program)
            {
                if (token == "[")
                    bracketCount++;
                else if (token == "]")
                    bracketCount--;
                if (bracketCount < 0)
                    return true;
            }
            if (int.TryParse(program.Last(), out _))
                return true;
            return !(bracketCount == 0);
        }

        public static string FormatPopulation(List<IGenome> population, Dictionary<List<int>, float> testCases)
        {
            StringBuilder bigProgram = new StringBuilder(population.Count);
            foreach (IGenome genome in population)
            {
                bigProgram.Append(FormatProgram(genome.ProgramToString(), testCases));
            }
            return bigProgram.ToString();
        }
    }
}