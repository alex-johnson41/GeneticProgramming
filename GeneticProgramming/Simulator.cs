namespace GeneticProgramming
{
    public class Simulator
    {
        public List<Genome> Population {get; private set;} //All genomes in current generation
        private int PopulationSize; //Number of genomes in each generation 
        private int MaxProgramLength; //Maximum length of a genome
        private IFitness Fitness; //Fitness function used to score genomes
        private IMutator Mutator; //Mutator used to create new genomes
        private int GenerationCount = 0; //Number of generations run

        public Simulator(int populationSize, int maxProgramLength, IFitness fitness, IMutator mutator)
        {
            PopulationSize = populationSize;
            MaxProgramLength = maxProgramLength;
            Population = [];
            Fitness = fitness;
            Mutator = mutator;
        }

        public Genome Run(){
            Logger.Log("Beginning simulation");
            Population = CreateInitialPopulation();
            Logger.Log("Initial population created");
            while (true){
                RunGeneration();
                if (Population[0].Score == Fitness.PassingScore){
                    Logger.Log("Solution found in generation: " + GenerationCount);
                    Logger.Log("Solution: " + Population[0].ProgramToString());
                    return Population[0];
                }
            }
        }

        private void RunGeneration(){
            ScorePopulation();
            SortPopulation();
            GenerationCount++;
            Logger.Log("Generation: " + GenerationCount + "  " + "Best score: " + Population[0].Score + " Best program: " + Population[0].ProgramToString());
            Population = Mutator.MutatePopulation(Population, PopulationSize);
        }

        private List<Genome> CreateInitialPopulation()
        {
            List<Genome> population = new List<Genome>();
            for (int i = 0; i < PopulationSize; i++)
            {
                population.Add(new Genome(MaxProgramLength));
            }
            return population;
        }

        private void ScorePopulation()
        {
            foreach(Genome genome in Population)
            {
                genome.Score = Fitness.CalculateScore(genome);
            };
        }

        private void SortPopulation()
        {
            Population.RemoveAll(genome => genome.Score < 0); // Remove all genes that didn't run
            Population.Sort((a, b) => a.Score.CompareTo(b.Score));
        }
    }
}   