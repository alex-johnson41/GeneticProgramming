namespace GeneticProgramming
{
    public interface IMutator
    {
        List<IGenome> CreateInitialPopulation(int populationSize, int maxProgramLength);
        List<IGenome> MutatePopulation(List<IGenome> population, int populationSize, int maxProgramLength);
    }
}