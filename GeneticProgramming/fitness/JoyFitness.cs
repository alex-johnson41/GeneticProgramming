namespace GeneticProgramming;

public class JoyFitness : IFitness
{
    public float PassingScore { get; set; }
    public Dictionary<List<int>, int> TestCases = new Dictionary<List<int>, int>();

    public JoyFitness(Dictionary<List<int>, int> testCases, float passingScore = 0f)
    {
        PassingScore = passingScore;
        TestCases = testCases;
    }

    public List<float> CalculatePopulationScores(List<IGenome> population, int maxProgramLength)
        {
            // Need to: Sort population by length, break out population into chunks based on length / 40, then 
            // split each chunk into seperate programs every 40 tokens and 
            // create a function to read output based on the number of chunks

            // Done: Population is sent in sorted 
            // Done: Split population into chunks based on length / 40

            //PROBLEM: Parentheses get split up when splitting the program, causing errors
            //TEMP FIX: Remove parentheses support

            List<float> scores = new List<float>();
            List<List<IGenome>> splitPopulation = SplitPopulationByLength(population, maxProgramLength);
            int programChunks = splitPopulation.Count;
            for (int i = 0; i < programChunks; i ++)
            {
                List<IGenome> chunk = splitPopulation[i];
                if (chunk.Count != 0)
                {
                    string output = JoyUtils.RunJoy(JoyUtils.FormatPopulation(chunk, TestCases));
                    List<string> filteredOutput = FilterOutput(output);
                    List<float> chunkScores = CalculateScores(filteredOutput, i + 1);
                    scores.AddRange(chunkScores);
                }
            }
            return scores;
        }

    private List<float> CalculateScores(List<string> filteredOutput, int programChunks)
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < filteredOutput.Count - (TestCases.Count * programChunks) + 1; i += TestCases.Count * programChunks)
        {
            List<string> testCaseResults = filteredOutput.GetRange(i, TestCases.Count * programChunks);
            float score = CalculateSingleScore(testCaseResults, programChunks);
            scores.Add(score);
        }
        return scores;
    }

    // Calculated as sum(abs(expected - actual) for each test case)
    private float CalculateSingleScore(List<string> testCaseResults, int programChunks)
    {   
        float score = 0;
        if (HasErrors(testCaseResults))
            return -1;
        List<float> resultList = testCaseResults.Where(s => !string.IsNullOrEmpty(s))
                                                .Select(float.Parse)
                                                .ToList();
        for (int i = programChunks - 1; i < resultList.Count; i+= programChunks)
            score += Math.Abs(resultList[i] - TestCases.ElementAt(i / programChunks).Value);

        return score;
    }

    private bool HasErrors(List<string> testCaseResults)
    {
        try
        {
            List<float> resultList = testCaseResults.Where(s => !string.IsNullOrEmpty(s))
                                                .Select(float.Parse)
                                                .ToList();
            if (resultList.Any(float.IsNaN))
                return true;
        }
        catch (FormatException)
        {
            return true;
        }
        return false;
    }

    // When running with a large population, messages from what I think is
    // a garbage collector in joy sometimes get put in with the output
    private List<string> FilterOutput(string output)
    {
        return output.Split(Environment.NewLine)
                     .Where(s => !string.IsNullOrEmpty(s) && !s.Contains("nodes"))
                     .ToList();
    }

    // Splits the population into chunks based on the length of the programs by 40 tokens
    public List<List<IGenome>> SplitPopulationByLength(List<IGenome> population, int maxProgramLength)
    {
        List<List<IGenome>> splitPopulation = new List<List<IGenome>>();
        int numChunks = (maxProgramLength - 1) / 40 + 1;
        for (int i = 0; i < numChunks; i++)
        {
            int minLength = i * 40;
            int maxLength = (i + 1) * 40;
            List<IGenome> chunk = population.Where(genome => genome.Program.Count > minLength 
                                                          && genome.Program.Count <= maxLength).ToList();
            splitPopulation.Add(chunk);
        }
        return splitPopulation;
    }
}