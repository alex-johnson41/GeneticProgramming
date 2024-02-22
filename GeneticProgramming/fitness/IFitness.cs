namespace GeneticProgramming
{
    public interface IFitness
    {
        float PassingScore { get; set;}
        List<float> CalculatePopulationScores(List<IGenome> population, int maxProgramLength);
    }
}