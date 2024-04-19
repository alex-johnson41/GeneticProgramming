namespace GeneticProgramming
{
    public class Simulator
    {
        public List<IGenome> Population {get; private set;} //All genomes in current generation
        private int PopulationSize; //Number of genomes in each generation 
        private int MaxProgramLength; //Maximum length of a genome
        private IFitness Fitness; //Fitness function used to score genomes
        private IMutator Mutator; //Mutator used to create new genomes
        private int GenerationCount = 0; //Number of generations run
        private int ThreadID; //ID of the thread running the simulation

        public Simulator(int populationSize, int maxProgramLength, IFitness fitness, IMutator mutator, int threadID)
        {
            PopulationSize = populationSize;
            MaxProgramLength = maxProgramLength;
            Population = new List<IGenome> {};
            Fitness = fitness;
            Mutator = mutator;
            ThreadID = threadID;
        }

        public IGenome Run(int? stopGeneration){
            Logger.Log("Beginning simulation");
            Population = Mutator.CreateInitialPopulation(PopulationSize, MaxProgramLength);
            Logger.Log("Initial population created");
            while (true){
                RunGeneration();
                if (Population[0].Score == Fitness.PassingScore){
                    Logger.Log("Solution found in generation: " + GenerationCount);
                    Logger.Log("Solution: " + Population[0].ProgramToString());
                    return Population[0];
                }
                if (GenerationCount == stopGeneration)
                {
                    return Population[0];
                }
                if (GenerationCount % 10 == 0){
                    Logger.Log("Thread: " + ThreadID + " Generation: " + GenerationCount + " Best score: " + Population[0].Score + " Best program: " + Population[0].ProgramToString());
                }
            }
        }

        private void RunGeneration(){
            ScorePopulation();
            SortPopulation();
            GenerationCount++;
            Population = Mutator.MutatePopulation(Population, PopulationSize, MaxProgramLength);
        }

        private void ScorePopulation()
        {
            List<float> scores = Fitness.CalculatePopulationScores(Population, MaxProgramLength);
            for (int i = 0; i < Population.Count; i++)
            {
                Population[i].Score = scores[i];
            }
        }

        private void SortPopulation()
        {
            Population.RemoveAll(genome => genome.Score < 0); // Remove all genes that didn't run
            Population.Sort((a, b) =>
            {
                // Sort by score, then by length of program in ties
                int scoreComparison = a.Score.CompareTo(b.Score);
                if (scoreComparison != 0)
                    return scoreComparison;
                else
                    return a.ProgramToString().Length.CompareTo(b.ProgramToString().Length);
            });
        }
    }
}   