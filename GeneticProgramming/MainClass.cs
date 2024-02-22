namespace GeneticProgramming
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int populationSize = 1000;
            int maxProgramLength = 10; // Buggy with values > ~300
            Simulator sim = new Simulator(populationSize, 
                                          maxProgramLength, 
                                          new JoyFitness(triangleAreaTestCases), 
                                          new JoyMutator(new Random())
                                          );
            sim.Run(null);

        }

        // Keys are the input values, values are the expected output
        private static Dictionary<List<int>, int> triangleAreaTestCases = new Dictionary<List<int>, int>()
        {
            { new List<int> { 2, 3 }, 3 },
            { new List<int> { 4, 5 }, 10 },
            { new List<int> { 6, 7 }, 21 },
            { new List<int> { 2, 5 }, 5 },
            { new List<int> { 1, 8 }, 4 },
            { new List<int> { 9, 4 }, 18 },
        };
    }
}