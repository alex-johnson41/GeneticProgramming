namespace GeneticProgramming
{
    public class JoyMutator : IMutator
    {
        private Random Random;

        public JoyMutator(Random random)
        {
            Random = random;
        }

        public List<IGenome> CreateInitialPopulation(int PopulationSize, int MaxProgramLength)
        {
            List<IGenome> population = new List<IGenome>();
            for (int i = 0; i < PopulationSize; i++)
            {
                population.Add(new JoyGenome(MaxProgramLength));
            }
            return population;
        }

        public List<IGenome> MutatePopulation(List<IGenome> population, int populationSize, int maxProgramLength)
        {
            List<IGenome> newPopulation = population.GetRange(0, population.Count / 10); // Only mutate the top 10% of the population
            while (newPopulation.Count < populationSize)
            {
                IGenome newGenome;
                if (Random.NextDouble() < 0.5) // TODO: Make this a parameter that gets passed in (crossoverRate)
                    newGenome = CrossoverGenome(population[Random.Next(population.Count)], population[Random.Next(population.Count)]);
                else{
                    newGenome = MutateGenome(population[Random.Next(population.Count)]);
                }
                if (!JoyUtils.SyntaxErrors(newGenome.Program) && newGenome.Program.Count <= maxProgramLength)
                    newPopulation.Add(newGenome);
            }
            return newPopulation;
        }

        private JoyGenome MutateGenome(IGenome genome)
        {
            List<string> newProgram = new List<string>(genome.Program);
            int index = Random.Next(newProgram.Count);
            newProgram[index] = JoyUtils.RandomJoyKeywordOrInt();
            return new JoyGenome(newProgram);
        }

        private JoyGenome CrossoverGenome(IGenome g1, IGenome g2)
        {
            List<string> newProgram = new List<string>(g1.Program);
            int removeIndex = Random.Next(g1.Program.Count);
            int addIndex = Random.Next(g2.Program.Count);
            newProgram.RemoveRange(removeIndex, newProgram.Count - removeIndex);
            newProgram.AddRange(g2.Program.GetRange(addIndex, g2.Program.Count - addIndex));
            return new JoyGenome(newProgram);
        }
    }
}