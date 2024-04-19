import random

def fuel_cost(inp: list[int]) -> float:
    return sum((num // 3 - 2) for num in inp)

def bouncing_balls(inp: list[int]) -> float:
    starting_height = inp[0]
    height_after_first_bounce = inp[1]
    num_bounces = inp[2]
    bounciness_index = height_after_first_bounce / starting_height
    total_dist = 0
    current_height = starting_height
    for _ in range(num_bounces):
        total_dist += current_height
        current_height *= bounciness_index
        total_dist += current_height
    return total_dist


def generate_random_inputs(
        num_examples: int, 
        input_count_min: int,
        input_count_max: int,
        input_values_min: int,
        input_values_max: int,
        func: callable
        ) -> list[tuple[list[int], float]]:
    examples = []
    for _ in range(num_examples):
        input_data = [
            random.randint(input_values_min, input_values_max) for _ in range(random.randint(input_count_min, input_count_max))]
        output = func(input_data)
        examples.append((input_data, output))
    return examples

def print_inputs(sample_inputs: list[tuple[list[int], float]]) -> None:
    for input_data, output in sample_inputs:
        input_data = ', '.join(map(str, input_data))
        print(f"{{ new List<int> {{{input_data}}}, {output}f }},")



num_examples = 50
input_count_min = 3
input_count_max = 3
input_values_min = 1
input_values_max = 100
func = bouncing_balls
random_examples = generate_random_inputs(num_examples, 
                                         input_count_min, 
                                         input_count_max, 
                                         input_values_min, 
                                         input_values_max, 
                                         func
                                         )
print_inputs(random_examples)
