namespace GeneticProgramming
{
    public class TestMutator : IMutator
    {
        private Random Random;

        public TestMutator(Random random)
        {
            Random = random;
        }

        public List<Genome> MutatePopulation(List<Genome> population, int populationSize)
        {
            List<Genome> newPopulation = population.GetRange(0, population.Count / 10); // Only mutate the top 10% of the population
            while (newPopulation.Count < populationSize)
            {
                if (Random.NextDouble() < 0.5) // TODO: Make this a parameter that gets passed in (crossoverRate)
                    newPopulation.Add(CrossoverGenome(population[Random.Next(population.Count)], population[Random.Next(population.Count)]));
                else{
                    newPopulation.Add(MutateGenome(population[Random.Next(population.Count)]));
                }
            }
            return newPopulation;
        }

        public Genome MutateGenome(Genome genome)
        {
            List<string> newProgram = new List<string>(genome.Program);
            int index = Random.Next(newProgram.Count);
            newProgram[index] = genome.RandomJoyKeywordOrInt();
            return new Genome(newProgram);
        }

        public Genome CrossoverGenome(Genome g1, Genome g2)
        {
            List<string> newProgram = new List<string>(g1.Program);
            int removeIndex = Random.Next(g1.Program.Count);
            int addIndex = Random.Next(g2.Program.Count);
            newProgram.RemoveRange(removeIndex, newProgram.Count - removeIndex);
            newProgram.AddRange(g2.Program.GetRange(addIndex, g2.Program.Count - addIndex));
            return new Genome(newProgram);
        }
    }
}