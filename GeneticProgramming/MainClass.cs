namespace GeneticProgramming
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int populationSize = 5000;
            int maxProgramLength = 150;
            int numSimulators = 5; 

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < numSimulators; i++)
            {
                Simulator sim = new Simulator(populationSize,
                                maxProgramLength,
                                new JoyFitness(fuelCostTestCases),
                                new JoyMutator(new Random()),
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
            { new List<int> {9, 12, 15, 18, 21}, 15},
            { new List<int> {6, 9, 12, 15, 18}, 10},
            { new List<int> {3, 6, 9, 12, 15}, 5},
            { new List<int> {10, 20, 30, 40, 50}, 38},
            { new List<int> {1, 4, 7, 10, 13}, 0},
            { new List<int> {8, 11, 14, 17, 20}, 10},
            { new List<int> {5, 8, 11, 14, 17}, 5},
            { new List<int> {2, 5, 8, 11, 14}, 0},
            { new List<int> {15, 25, 35, 45, 55}, 47},
            { new List<int> {18, 21, 24, 27, 30}, 30},
            { new List<int> {3, 5, 19}, 2},
            { new List<int> {57, 34, 2}, 24},
            { new List<int> {7, 1, 46}, 11},
            { new List<int> {1}, -2},
            { new List<int> {2}, -2},
            { new List<int> {3}, -1},
            { new List<int> {4}, -1},
            { new List<int> {5}, -1},
            { new List<int> {6}, 0},
            { new List<int> {7}, 0},
            { new List<int> {8}, 0},
            { new List<int> {9}, 1},
            { new List<int> {10}, 1},
            { new List<int> {11}, 1},
            { new List<int> {12}, 2},
            { new List<int> {30, 15, 66}, 31 },
            { new List<int> {70, 74, 42, 3, 2, 71, 59, 8, 62, 11}, 109 },
            { new List<int> {30, 75, 49, 30, 63, 65, 33, 90, 62}, 146 },
            { new List<int> {9, 76, 20, 94, 37, 83, 63, 50}, 125 },
            { new List<int> {93, 40, 22, 22}, 50 },
            { new List<int> {94, 31, 86, 70, 73}, 106 },
            { new List<int> {47, 78, 16, 84, 79, 45}, 103 },
            { new List<int> {16, 95, 97, 78, 49, 96, 2}, 128 },
            { new List<int> {16, 75, 70, 76, 77, 10}, 94 },
            { new List<int> {100, 35, 69, 75, 66, 10, 4, 5}, 103 },
            { new List<int> {89, 70, 88, 66, 73, 72, 28, 67, 65, 38}, 195 },
            { new List<int> {10, 27, 78}, 32 },
            { new List<int> {44, 95, 29, 68, 36, 64}, 97 },
            { new List<int> {54, 54, 51, 11, 31, 16, 3}, 58 },
            { new List<int> {17, 44, 80, 84, 6, 100, 66, 96, 89}, 173 },
            { new List<int> {8, 38, 54, 48, 99, 49, 53}, 100 },
            { new List<int> {9, 77, 8, 98}, 54 },
            { new List<int> {99, 74, 13, 61, 56, 75, 31, 91, 59, 47}, 178 },
            { new List<int> {37, 44, 66, 25, 42, 26, 73, 21, 25, 53}, 114 },
            { new List<int> {70, 25, 66, 35, 74, 72, 89}, 127 },
            { new List<int> {55, 93, 80, 26, 100, 90, 88, 79}, 185 },
            { new List<int> {19, 70, 39, 77, 68, 14, 16, 84, 54, 36}, 136 },
            { new List<int> {93, 31, 27, 4, 18, 37, 85, 64, 48, 3}, 115 },
            { new List<int> {20, 99, 37, 5, 3, 67, 5}, 62 },
            { new List<int> {41, 82, 12, 55, 40, 99, 30}, 104 },
            { new List<int> {42, 35, 33, 22, 57}, 52 },
            { new List<int> {13, 11, 24, 27, 28, 68, 82}, 68 },
            { new List<int> {13, 11, 23, 41, 35, 83, 69, 48, 49}, 102 },
            { new List<int> {12, 48, 4}, 15 },
            { new List<int> {55, 56, 9, 77, 30, 55, 50}, 94 },
            { new List<int> {70, 69, 49, 20, 84, 87, 32, 15, 58}, 141 },
            { new List<int> {53, 33, 90, 87, 35, 53, 46, 47, 53}, 144 },
            { new List<int> {43, 77, 73, 64, 43, 36, 53, 5}, 112 },
            { new List<int> {80, 49, 96, 96, 84}, 124 },
            { new List<int> {73, 59, 32, 62, 54, 87, 20, 30, 12}, 122 },
            { new List<int> {83, 56, 68, 41, 2, 39}, 81 },
            { new List<int> {86, 51, 38, 64, 92, 76, 79}, 145 },
            { new List<int> {76, 61, 95, 39, 21, 52, 12, 34}, 112 },
            { new List<int> {64, 53, 9, 1, 20, 64, 78}, 80 },
            { new List<int> {75, 37, 12, 81, 49, 5}, 73 },
            { new List<int> {46, 71, 78, 4, 11, 13, 99, 82, 39}, 127 },
            { new List<int> {71, 21, 28, 22}, 38 },
            { new List<int> {82, 40, 94, 37, 65, 48, 28, 35, 68, 31}, 152 },
            { new List<int> {55, 10, 61, 26, 61, 57}, 76 },
            { new List<int> {18, 73, 20, 45, 1, 90, 21, 92, 97, 55}, 148 },
            { new List<int> {72, 53, 40}, 48 },
            { new List<int> {56, 15, 64, 68, 31, 3, 26, 67, 20}, 95 },
            { new List<int> {42, 10, 36, 94}, 52 },
            { new List<int> {9, 48, 72, 52, 6}, 52 },
            { new List<int> {21, 62, 32, 36, 20, 1, 63, 39, 63}, 92 },
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