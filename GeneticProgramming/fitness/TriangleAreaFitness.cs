using System.Text;

namespace GeneticProgramming
{
    public class TriangleAreaFitness : IFitness
    {
        public int PassingScore { get; set; }
        private Dictionary<List<int>, int> testCases = new Dictionary<List<int>, int>()
            {
                { new List<int> { 2, 3 }, 3 },
                { new List<int> { 4, 5 }, 10 },
                { new List<int> { 6, 7 }, 21 },
                { new List<int> { 2, 5 }, 5 },
                { new List<int> { 1, 8 }, 4 },
                { new List<int> { 9, 4 }, 18 },
            };

        public TriangleAreaFitness()
        {
            PassingScore = 0; // Lower scores are better
        }

        // Score is calculated by the follwing formula:
        // sum(abs(expected - actual) for each test case)
        public float CalculateScore(Genome genome)
        {
            string programString = CreateProgramString(genome.ProgramToString());
            try
            {
                string output = FitnessUtils.RunJoy(programString);
                List<float> results = output.Split(Environment.NewLine)
                                            .Where(s => !string.IsNullOrEmpty(s))
                                            .Select(float.Parse)
                                            .ToList();
                float score = 0;
                for (int i = 0; i < results.Count && i < testCases.Count; i++)
                {
                    score += Math.Abs(results[i] - testCases.ElementAt(i).Value);
                }
                return score;
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        private string CreateProgramString(string program)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var testCase in testCases)
            {
                stringBuilder.Append($"{testCase.Key[0]} {testCase.Key[1]} {program}"); // Append the input values to the start of the program
            }
            return stringBuilder.ToString();
        }
    }
}