namespace GeneticProgramming
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int populationSize = 1000;
            int maxProgramLength = 100; 
            Simulator sim = new Simulator(populationSize, 
                                          maxProgramLength, 
                                          new JoyFitness(fibonacciNumbers), 
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

        private static Dictionary<List<int>, int> fibonacciNumbers = new Dictionary<List<int>, int>()
        {
            { new List<int> { 0 }, 0 },
            { new List<int> { 1 }, 1 },
            { new List<int> { 2 }, 1 },
            { new List<int> { 3 }, 2 },
            { new List<int> { 4 }, 3 },
            { new List<int> { 5 }, 5 },
            { new List<int> { 6 }, 8 },
            { new List<int> { 7 }, 13 },
            { new List<int> { 8 }, 21 },
            { new List<int> { 9 }, 34 },
            { new List<int> { 10 }, 55 },
            { new List<int> { 11 }, 89 },
            { new List<int> { 12 }, 144 },
            { new List<int> { 13 }, 233 },
            { new List<int> { 14 }, 377 },
            { new List<int> { 15 }, 610 },
            { new List<int> { 16 }, 987 },
            { new List<int> { 17 }, 1597 },
            { new List<int> { 18 }, 2584 },
            { new List<int> { 19 }, 4181 },
            { new List<int> { 20 }, 6765 },
            { new List<int> { 21 }, 10946 },
            { new List<int> { 22 }, 17711 },
            { new List<int> { 23 }, 28657 },
            { new List<int> { 24 }, 46368 },
            { new List<int> { 25 }, 75025 },
            { new List<int> { 26 }, 121393 },
            { new List<int> { 27 }, 196418 },
            { new List<int> { 28 }, 317811 },
            { new List<int> { 29 }, 514229 },
            { new List<int> { 30 }, 832040 },
        };
    }
}