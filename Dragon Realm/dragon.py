import random
import time

def intro():
	print('You are in a land full of dragons. In front of you,')
	print('and will share his treasure with you. The other dragon')
	print('you see two caves. In one cave, the dragon is friendly')
	print('is greedy and hungry, and will eat you on sight.')
	print()

def choose_cave():
	cave = ''
	while cave != '1' and cave != '2':
		print('Which cave will you go into? (1 or 2)')
		cave = input()

	return cave

def check_cave(cave):
	print('You approached the cave')
	time.sleep(2)
	print('Ti\'s dark and spooky')
	time.sleep(2)
	print('A large dragon jumps in front of you and open it\'s jaws')
	time.sleep(2)

	safe_cave = random.randint(1,2)
	if int(cave) == safe_cave:
		print('Gives the gold')
	else:
		print('Goobles you down in single bite!')

play_again = 'yes'
while play_again == 'yes' or play_again == 'y':
	intro()
	cave = choose_cave()
	check_cave(cave)
	print('Do you want to play again?')
	play_again = input()