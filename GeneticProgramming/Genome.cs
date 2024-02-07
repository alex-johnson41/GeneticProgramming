namespace GeneticProgramming
{
    public class Genome
    {
        public List<string> Program { get; private set; } //Program to be executed
        public float Score = -1;                            //Fitness score of the program

        //Constructor used when creating a random genome
        public Genome(int maxProgramLength){
            Program = JoyUtils.RandomJoyProgram(maxProgramLength);
        }

        //Constructor used when using a mutated genome
        public Genome(List<string> program){
            Program = program;
        }   

        public string ProgramToString(){
            return string.Join(" ", Program);
        }
    }
}