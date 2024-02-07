# GeneticProgramming
## Undergraduate research project by Alex Johnson

Exploring computer generated software using genetic evolution techniques. This project will generate Joy programs using a genetic programming simulator written in C#. 

See the ProfileData folder for data from runs throughout development. Load data into Speedscope (https://www.speedscope.app/)

## General Program Flow
The user creates a fitness function for the desired program that it wants generated. This would include example inputs and expected outputs to be compared and scored against the generated programs. 

A population of randomely generated genomes (joy programs) is created. Each of those genomes are then scored using the fitness function that the user created. From there, a new set of genomes is created from the highest scoring genomes through mutation and crossover. Scoring takes place again and the process repeats until a genome receives the maximum possible score from the fitness function, indicating that it correctly performs the computations that the user desires. 

## Classes (In development, this may not be 100% accurate)

### Genome
- A Joy program made up of a list of symbols that each correspond to a Joy keyword. 
- Attributes:
    - score: Integer score of the program against the fitness function
        - -1 if it hasn't been tested against the fitness function or failed to run

### Simulator
- Class that manages the creation and evolution of genomes
- Attributes:
    - genomes: Array of the current population of genomes

### Fitness
- Class responsible for running a genome and scoring it

### Log
- Class that manages logging data about each generation. May create log files or just print to stdout

## Ideas/Considerations
- Try to create a general fitness class that takes in a text file (JSON?) from the user that specifies the scoring criteria for the desired program. With example inputs/outputs. 
- When running the joy programs, there needs to be infinite loop detection and termination. 