namespace GeneticProgramming
{
    public interface IMutator
    {
        List<Genome> MutatePopulation(List<Genome> population, int populationSize);
    }
}