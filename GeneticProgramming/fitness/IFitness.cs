namespace GeneticProgramming
{
    public interface IFitness
    {
        int PassingScore { get; set;}
        float CalculateScore(Genome genome);
    }
}