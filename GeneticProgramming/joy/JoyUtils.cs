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
                //Arguments = "-c \"./joy/joy " + joyFilePath + "\"",
                //Arguments = "-c \"pwd\"",
                Arguments = "-c \"" + GetProjectDirectory() + "/joy/joy " + joyFilePath + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            using (Process process = Process.Start(start))
            {
                using StreamReader reader = process.StandardOutput;
                results = reader.ReadToEnd();
            }
            File.Delete(joyFilePath);
            if (results.Contains("run time error:") || results.Contains("expected") || results == "")
                throw new FormatException("Program failed to run");
            return results;
        }

        public static string FormatProgram(string program, Dictionary<List<int>, int> testCases)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var testCase in testCases)
            {
                foreach (var input in testCase.Key)
                {
                    stringBuilder.Append($"{input} ");
                }
                stringBuilder.Append($"{program}.\n");
            }
            return stringBuilder.ToString();
        }

        static string GetProjectDirectory()
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
            string[] zeroToNine = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            List<string> program = new List<string>();
            Random random = new Random();
            for (int i = 0; i < random.Next(1, maxProgramLength + 1); i++) //Random length up to maxProgramLength
            {
                program.Add(RandomJoyKeywordOrInt());
            }
            return program;
        }

        public static string RandomJoyKeywordOrInt()
        {
            string[] joyKeywords = {
                "+", "-", "*", "/", "%", "**", "neg", "dup", "swap",
                "pop", "clear", "[", "]", "map", "concat", "rollup",
                "rolldown", "rollupd"
            };
            string[] zeroToNine = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random random = new Random();
            double randomNumber = random.NextDouble(); //TODO: Make this a parameter that gets passed in (numberRate)
            if (randomNumber <= 0.3) // 30% chance of adding a number
            {
                int numberIndex = random.Next(0, zeroToNine.Length);
                return zeroToNine[numberIndex];
            }
            else // 70% chance of adding a joy keyword
            {
                return joyKeywords[random.Next(0, joyKeywords.Length)];
            }
        }
    }
}