namespace GeneticProgramming
{
    public class JoyMutator : IMutator
    {
        private Random Random;
        private float CrossoverRate; // Percentage of population that will be created by crossover
        private float MutationRate; // Proportion of population that will be created by mutation of a single gene
        private float DeletionRate; // Proportion of population that will be created by deletion of a single gene
        private float SurvivalRate; // Proportion of population that will survive to the next generation

        public JoyMutator(float survivalRate, float crossoverRate, float mutationRate, float deletionRate)
        {
            Random = new Random();
            CrossoverRate = crossoverRate;
            MutationRate = mutationRate;
            DeletionRate = deletionRate;
            SurvivalRate = survivalRate;

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
            List<IGenome> newPopulation = population.GetRange(0, (int)(population.Count * SurvivalRate)); 
            while (newPopulation.Count < populationSize)
            {
                IGenome newGenome;
                double rand = Random.NextDouble();
                if (rand < CrossoverRate) 
                    newGenome = CrossoverGenome(population[Random.Next(population.Count)], population[Random.Next(population.Count)]);
                else if (rand < (CrossoverRate + MutationRate)) 
                    newGenome = MutateGenome(population[Random.Next(population.Count)]);
                else 
                    newGenome = DeleteGenome(population[Random.Next(population.Count)]);
                if (newGenome.Program.Count <= maxProgramLength
                    && newGenome.Program.Count > 0
                    && !JoyUtils.SyntaxErrors(newGenome.Program))
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

        private JoyGenome DeleteGenome(IGenome genome)
        {
            List<string> newProgram = new List<string>(genome.Program);
            int index = Random.Next(newProgram.Count);
            newProgram.RemoveAt(index);
            return new JoyGenome(newProgram);
        }
    }
}