using System.Diagnostics;


namespace GeneticProgramming
{
    public class FitnessUtils
    {
        public static string RunJoy(string program)
        {
            string results = "";
            string joyFilePath = "joy/program.joy";
            File.WriteAllText(joyFilePath, program + Environment.NewLine);
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = "-c \"./joy/joy " + joyFilePath + "\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            using (Process process = Process.Start(start))
            {
                using StreamReader reader = process.StandardOutput;
                results = reader.ReadToEnd();
            }
            if (results.Contains("run time error:") || results.Contains("expected") || results == "")
                throw new FormatException("Program failed to run");
            return results;
        }
    }
}