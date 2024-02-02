namespace GeneticProgramming
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int populationSize = 1000;
            int maxProgramLength = 10;
            Simulator sim = new Simulator(populationSize, maxProgramLength, new TriangleAreaFitness(), new TestMutator(new Random()));
            sim.Run();

        }
    }
}

