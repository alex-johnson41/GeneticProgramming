using System.Text;

namespace GeneticProgramming
{
    public class TriangleAreaFitness : IFitness // Fitness class for calculating the area of a triangle
    {
        public float PassingScore { get; set; }
        private Dictionary<List<int>, int> testCases = new Dictionary<List<int>, int>() // Keys are the input values, values are the expected output
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
            PassingScore = 0f; // Lower scores are better
        }

        // Score is calculated by the follwing formula:
        // sum(abs(expected - actual) for each test case)
        public float CalculateScore(Genome genome)
        {
            string programString = JoyUtils.FormatProgram(genome.ProgramToString(), testCases);
            try
            {
                string output = JoyUtils.RunJoy(programString);
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
    }
}