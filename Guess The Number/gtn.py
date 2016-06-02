import random 

max_turns = 6
no_of_turns = 1
print('Hello, What\'s your name?')
name = input()
print('Welcome ' + name + ', I am thinking of a number between 1 and 20.')
print('Take a guess.')

thought_number = random.randint(1, 20)

while no_of_turns <= max_turns:
	guess = int(input())
	if guess == thought_number:
		print('Good Job ' + name + '. You guess my number in ' + str(no_of_turns) + ' turns.')
		break
	elif guess > thought_number:
		print('You guess is too high.')
	else:
		print('Your guess is too low.')

	no_of_turns += 1

if no_of_turns > max_turns:
	print('Nope, The number I was thinking was ' + str(thought_number))