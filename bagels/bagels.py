import random


def get_secret_number(num_digits):
	numbers = list(range(10))
	random.shuffle(numbers)

	secret_number = ''
	for i in range(0, num_digits):
		secret_number += str(numbers[i])

	return secret_number

def play_again():
	print('Do you want to play again? yes or no')
	return input().lower().startswith('y')

def analyze(guess, secretNum):
	clue = []
	for i in range(len(guess)):
		if guess[i] == secretNum[i]:
			clue.append('Fermi')
		elif guess[i] in secretNum:
			clue.append('Pico')
	if len(clue) == 0:
		return 'Bagels'
	clue.sort()
	return ' '.join(clue)
	
def is_num(num):
	if num == '':
		return False

	for i in num:
		if i not in '0 1 2 3 4 5 6 7 8 9'.split(' '):
			return False

	return True

while True:
	num_digits = 3
	num_guess = 1
	max_guess = 10
	game_end = True

	print('Welcome to Bagels')
	print('I am thinking of a 3 digit number. Try to guess what it is')
	print('Here are some clues:')
	print('When I say:      That means:')
	print('Pico:            One digit is correct but in wrong position')
	print('Fermi:           One digit is correct and in right position')
	print('Bagels:          No digit is correct')
	print('I have thought of a number. You have 10 guesses to get it')
	
	secret_number = get_secret_number(num_digits)

	while num_guess <= max_guess:
		print('Guess #' + str(num_guess))
		guessed_number = input()

		while len(guessed_number) != num_digits or not is_num(guessed_number):
			print('Guess #' + str(num_guess))
			guessed_number = input()

		if guessed_number == secret_number:
			print('You have guess it correctly in ' + str(num_guess) + ' guesses')
			break

		print(analyze(guessed_number, secret_number))
		num_guess += 1

		if num_guess > max_guess:
			print('You Lose! Number was: ' + secret_number)
			break

	if not play_again():
		break
