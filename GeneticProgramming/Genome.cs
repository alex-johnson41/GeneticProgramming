namespace GeneticProgramming
{
    public class Genome
    {
        public List<string> Program { get; private set; } //Program to be executed
        public float Score = -1;                            //Fitness score of the program

        //Constructor used when creating a random genome
        public Genome(int maxProgramLength){
            Program = RandomJoyProgram(maxProgramLength);
        }

        //Constructor used when using a mutated genome
        public Genome(List<string> program){
            Program = program;
        }   

        public string ProgramToString(){
            return string.Join(" ", Program) + ".\n";
        }

        //Generates a random Joy program
        private List<string> RandomJoyProgram(int maxProgramLength)
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

        public string RandomJoyKeywordOrInt()
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