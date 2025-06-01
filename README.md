# GeneticProgramming
## Undergraduate research project by Alex Johnson

Exploring computer generated software using genetic evolution techniques. This project will generate Joy programs using a genetic programming simulator written in C# and test cases created by the user. 

See the ProfileData folder for data from runs throughout development. Load json files into Speedscope (https://www.speedscope.app/)

## Build and run the project

- I build and run using .NET Core 7.0 on MacOS, although mono works as well
- Download .NET 7.0, navigate to GeneticProgramming/GeneticProgramming in your terminal, and run ```dotnet run```
- To change simulation parameters, open up MainClass.cs in any text editor
    - Create a new testcases dictionary based on the examples provided. (List of ints as input/dictionary key, and a float as expected output/dictionary value)
        - To run with your new test cases, replace the existing test case on line 18, when the fitness object is created
        - If you don't want to manually create many input and output examples, write your function in python in the input_creator.py, and use the paremeters there to generate as many test cases as you'd like
    - Modify the population size, maxProgramLength, and numSimulators by changing the values at the top of the file
        - NOTE: numSimulators has a large impact on system resources, increase with caution. I usually don't go above 10-15 on my M1 Macbook and Ryzen 5 3600 desktop. 
    - Modify the survivalRate, crossoverRate, mutationRate, and deletionRate based on the comments on lines 20-24. (crossoverRate, mutationRate, and deletionRate must add up to 1)


## General Program Flow
The user creates a list of test cases for the desired program that it wants generated. This includes example inputs and their expected output to be compared and scored against the generated programs. 

The simulator creates a population of randomly generated genomes (joy programs). Each of those genomes are then scored using the test cases that the user provided. From there, a new set of genomes is created from the highest scoring genomes through mutation, crossover, and deletion. Scoring takes place again and the process repeats until a genome receives the maximum possible score from the fitness function (0), indicating that it correctly performs the computations that the user desires. 

## Classes

### Genome
- A Joy program made up of a list of symbols that each correspond to a Joy keyword. 
- Attributes:
    - Score: Integer score of the program against the fitness function
        - -1 if it hasn't been tested against the fitness function or failed to run

### Simulator
- Class that manages the creation and evolution of genomes
- Records data from each generation, outputting the current top score and program
- Attributes:
    - Population: List of the current population of genomes
    - PopulationSize: Integer of number of genomes in population
    - MaxProgramLength: Integer representing the maximum number of keywords in a genome
    - GenerationCount: Integer tracking the number of generations performed
    - ThreadID: ID of the thread the simulation is running on
    - Fitness: Fitness class used to score the population
    - Mutator: Mutator class used to mutate the population

### JoyFitness
- Class responsible for running a genome and scoring it
- Attributes:
    - PassingScore: Float representing the goal score for a program, 0 by default
    - TestCases: Dictionary containing all the test cases for the Joy program. Keys are a list of input values, and the values are a float representing the expected output for those inputs. 

### JoyMutator
- Class responsible for mutating a population of genomes and creating the next generation
- The top n% of the population is passed into the next generation, then a loop is entered where a new genome is created by randomly deciding to mutate an existing genome, deleting a gene in a genome, or crossing two genomes together to create a new one. This loop continues until a full population is created. 
- Attributes:
    - SurvivalRate: Float representing the proportion of programs in a population that survive until the next generation and undergo mutation. 
        - Ex// SurvivalRate = 0.3 would result in the top 30% of programs being mutated and going to the next generation
    - MutationRate: Float representing the chance of a new genome being created through mutation
    - CrossoverRate: Float representing the chance of a new genome being created through crossing over two existing genomes. 
    - DeletionRate: Float representing the chance of a new genome being created through deleting a gene from a genome

### JoyUtils
- Class with various helper functions used for performing actions with Joy
- Functions:
    - RunJoy
        - Takes in a joy program as a string, and runs it in the joy interpreter, returns the string returned by the interpreter
    - FormatProgram
        - Takes in a joy program and a dictionary of test cases, and creates a complete joy program for each test case
        - Each program starts with "[] unstack" to reset the joy stack, that's followed by the input value(s) from the test cases, and finally ends the joy program with a "." and a newline at the end. 
    - RandomJoyProgram
        - Creates a random joy program with n keywords or numbers
    - SyntaxErrors
        - Checks a joy program to ensure that there are no unmatched brackets
        - Also checks that the last token isn't a number, as nothing gets returned in that case
    - FormatPopulation
        - Takes in a population of Genomes, and returns a single string containing the formatted programs for each genome in the population with every test case
        - Total number of programs is ```population.Count * testCases.Count```
