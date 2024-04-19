namespace GeneticProgramming;

public class JoyFitness : IFitness
{
    public float PassingScore { get; set; }
    public Dictionary<List<int>, float> TestCases = new Dictionary<List<int>, float>();

    public JoyFitness(Dictionary<List<int>, float> testCases, float passingScore = 0f)
    {
        PassingScore = passingScore;
        TestCases = testCases;
    }

    public List<float> CalculatePopulationScores(List<IGenome> population, int maxProgramLength)
        {
            string output = JoyUtils.RunJoy(JoyUtils.FormatPopulation(population, TestCases));
            List<string> filteredOutput = FilterOutput(output);
            List<float> scores = CalculateScores(filteredOutput);
            return scores;
        }

    private List<float> CalculateScores(List<string> filteredOutput)
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < filteredOutput.Count - TestCases.Count + 1; i += TestCases.Count)
        {
            List<string> testCaseResults = filteredOutput.GetRange(i, TestCases.Count);
            float score = CalculateSingleScore(testCaseResults);
            scores.Add(score);
        }
        return scores;
    }

    // Calculated as sum(abs(expected - actual) for each test case)
    private float CalculateSingleScore(List<string> testCaseResults)
    {   
        float score = 0;
        try
        {
            List<float> resultList = testCaseResults.Where(s => !string.IsNullOrEmpty(s))
                                                .Select(float.Parse)
                                                .ToList();
            if (resultList.Any(float.IsNaN))
                return -1;
            for (int i = 0; i < resultList.Count; i++)
                score += Math.Abs(resultList[i] - TestCases.ElementAt(i).Value);
        }
        catch (FormatException)
        {
            return -1;
        }
        return score;
    }

    // When running with a large population, messages from what I think is
    // a garbage collector in joy sometimes get put in with the output
    private List<string> FilterOutput(string output)
    {
        return output.Split(Environment.NewLine)
                     .Where(s => !string.IsNullOrEmpty(s) && !s.Contains("nodes"))
                     .ToList();
    }
}