namespace GeneticProgramming
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int populationSize = 5000;
            int maxProgramLength = 75;
            int numSimulators = 10; 

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < numSimulators; i++)
            {
                Simulator sim = new Simulator(
                    populationSize,
                    maxProgramLength,
                    new JoyFitness(fibonacciNumbers),
                    new JoyMutator(
                        0.5f,  // Survival percentage
                        0.7f,  // Crossover percentage
                        0.25f, // Mutation percentage
                        0.05f  // Deletion percentage
                        ),     // The last three floats must add to 1
                    i
                    );
                Thread thread = new Thread(() => sim.Run(null));
                threads.Add(thread);
                thread.Start();
            }

            // Wait for all threads to finish
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        // Keys are the input values, values are the expected output
        private static Dictionary<List<int>, float> triangleAreaTestCases = new Dictionary<List<int>, float>()
        {
            { new List<int> { 2, 3 }, 3 },
            { new List<int> { 4, 5 }, 10 },
            { new List<int> { 6, 7 }, 21 },
            { new List<int> { 2, 5 }, 5 },
            { new List<int> { 1, 8 }, 4 },
            { new List<int> { 9, 4 }, 18 },
        };

        private static Dictionary<List<int>, float> fuelCostTestCases = new Dictionary<List<int>, float>()
        {
            { new List<int> {77, 68, 31, 34, 62, 48, 49}, 106f },
            { new List<int> {83}, 25f },
            { new List<int> {19, 50}, 18f },
            { new List<int> {6, 56, 38, 85, 44, 87}, 91f },
            { new List<int> {46, 52, 36}, 38f },
            { new List<int> {17, 100, 90, 37}, 72f },
            { new List<int> {99, 6, 39, 65}, 61f },
            { new List<int> {82, 70, 45, 15}, 62f },
            { new List<int> {12, 84, 23, 32, 100, 93}, 101f },
            { new List<int> {93, 32, 15, 91, 99, 88, 32}, 134f },
            { new List<int> {35, 44, 22, 86, 16, 51}, 70f },
            { new List<int> {62}, 18f },
            { new List<int> {44}, 12f },
            { new List<int> {39}, 11f },
            { new List<int> {11}, 1f },
            { new List<int> {58, 10, 23, 54}, 39f },
            { new List<int> {71, 62, 19}, 43f },
            { new List<int> {89}, 27f },
            { new List<int> {62, 11, 35, 66, 97, 36, 10}, 89f },
            { new List<int> {74, 40, 91, 18, 87}, 92f },
            { new List<int> {76, 63, 41, 20}, 57f },
            { new List<int> {63}, 19f },
            { new List<int> {36, 38, 8, 31, 64, 44}, 59f },
            { new List<int> {64, 35, 92, 6, 56, 37, 10}, 83f },
            { new List<int> {41, 98, 65, 68, 81, 40, 65}, 135f },
            { new List<int> {90, 66, 60, 49, 9, 65}, 100f },
            { new List<int> {27, 39, 47, 31, 95}, 68f },
            { new List<int> {12, 8, 99, 44}, 45f },
            { new List<int> {94, 66}, 49f },
            { new List<int> {60, 47, 43, 20, 41, 39, 13}, 71f },
            { new List<int> {26, 88, 48, 20, 53}, 66f },
            { new List<int> {56, 9, 10, 23}, 23f },
            { new List<int> {46, 65, 37, 6, 59, 7}, 59f },
            { new List<int> {33, 40, 89, 41, 45}, 71f },
            { new List<int> {99, 20}, 35f },
            { new List<int> {76, 25, 59, 49, 54, 35}, 85f },
            { new List<int> {76, 94, 62}, 70f },
            { new List<int> {40, 73, 36, 69, 91, 49, 27}, 113f },
            { new List<int> {89, 6}, 27f },
            { new List<int> {48, 88, 93, 70, 84}, 117f },
            { new List<int> {27, 43, 79, 15, 85}, 72f },
            { new List<int> {79, 81}, 49f },
            { new List<int> {86, 56, 82, 96, 20, 43}, 113f },
            { new List<int> {15, 70, 42, 71, 16}, 60f },
            { new List<int> {8, 56, 53, 34, 75}, 63f },
            { new List<int> {57, 98, 57}, 64f },
            { new List<int> {46, 44, 61, 59}, 60f },
            { new List<int> {100, 52, 48, 22, 75, 99}, 119f },
            { new List<int> {92, 12, 18}, 34f },
            { new List<int> {29, 43, 99, 60, 70, 15, 95}, 121f }
        };

        private static Dictionary<List<int>, float> bouncyBallTestCases = new Dictionary<List<int>, float>()
        {
            { new List<int> {5, 3, 2}, 12.8f},
            { new List<int> {8, 4, 4}, 22.5f},
            { new List<int> {12, 6, 3}, 31.5f},
            { new List<int> {10, 7, 5}, 47.142699f},
            { new List<int> {6, 4, 4}, 24.074074f},
            { new List<int> {15, 9, 3}, 47.04f},
            { new List<int> {7, 5, 5}, 34.190753f},
            { new List<int> {9, 6, 4}, 36.111111f},
            { new List<int> {11, 8, 3}, 42.867768f},
            { new List<int> {13, 7, 5}, 41.371800f}

        };

        private static Dictionary<List<int>, int> meanTestCases = new Dictionary<List<int>, int>()
        {
            { new List<int> {4, 5, 6} , 5 },
            { new List<int> {1, 2, 3} , 2 },
            { new List<int> {1, 1, 1} , 1 },
            { new List<int> {1, 2, 3, 4, 5} , 3 },
            { new List<int> {8, 12} , 10 },
            { new List<int> {3, 8, 4}, 5},
            { new List<int> {2, 2, 6, 2}, 3},
            { new List<int> {10, 7, 4}, 21},
            { new List<int> {11, 5}, 8},
            { new List<int> {10, 10, 10, 10, 10, 10, 10, 10, 10, 10}, 10},
            
        };

        private static Dictionary<List<int>, float> fibonacciNumbers = new Dictionary<List<int>, float>()
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