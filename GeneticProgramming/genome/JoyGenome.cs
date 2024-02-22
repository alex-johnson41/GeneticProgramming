namespace GeneticProgramming
{
    public class JoyGenome : IGenome
    {
        public List<string> Program { get; private set; } //Program to be executed
        public float Score {get; set;} = -1f;             //Fitness score of the program

        //Constructor used when creating a random genome
        public JoyGenome(int maxProgramLength){
            Program = JoyUtils.RandomJoyProgram(maxProgramLength);
        }

        //Constructor used when using a mutated genome
        public JoyGenome(List<string> program){
            Program = program;
        }   

        public string ProgramToString(){
            return string.Join(" ", Program);
        }
    }
}