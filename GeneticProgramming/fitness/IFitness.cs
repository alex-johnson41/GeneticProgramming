namespace GeneticProgramming
{
    public interface IFitness
    {
        float PassingScore { get; set;}
        float CalculateScore(Genome genome);
    }
}