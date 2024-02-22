namespace GeneticProgramming;

public interface IGenome
{
    List<string> Program { get; }
    float Score { get; set; }
    string ProgramToString();
}