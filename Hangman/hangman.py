import random

HANGMAN = [
'''
     +---+
     |   |
         |
         |
         |
         |
==============
''','''

    +---+
    |   |
    O   |
        |
        |
        |
==============
''','''

    +---+
    |   |
    O   |
   /    |
        |
        |
==============
''','''

    +---+
    |   |
    O   |
   /|   |
        |
        |
==============
''','''

    +---+
    |   |
    O   |
   /|\  |
        |
        |
==============
''','''

    +---+
    |   |
    O   |
   /|\  |
   /    |
        |
==============
''','''

    +---+
    |   |
    O   |
   /|\  |
   / \  |
        |
==============
''']


words = 'ant baboon badger bat bear beaver camel cat clam cobra cougar coyote crow deer dog donkey duck eagle ferret fox frog goat goose hawk lion python rabbit ram rat raven rhino salmon seal shark sheep skunk sloth snake spider stork swan tiger toad trout turkey turtle weasel whale wolf wombat zebra'.split()

def get_random_word():
	index = random.randint(0, len(words) - 1)
	return words[index]

def current_state(missed_words, gussed_words, secret_word):
	print(HANGMAN[len(missed_words)])
	print()

	print('Missed Letters: ', end='')
	for letter in missed_words:
		print(letter, end=' ')

	print('')

	blanks = '_' * len(secret_word)
	for i in range(len(secret_word)):
		if secret_word[i] in gussed_words:
			blanks = blanks[:i] + secret_word[i] + blanks[i+1:]

	print('Gussed Word: ', end='')
	for letter in blanks:
		print(letter, end=' ')

	print('')

def go_guess(already_gussed):
	while True:
		print('Guess a Letter: ')
		guess = input()
		guess = guess.lower()
		if len(guess) != 1:
			print('Please enter single letter')
		elif guess in already_gussed:
			print('You have already choose this letter. Choose again.')
		elif guess not in 'abcdefghijklmnopqrstuvwxyz':
			print('Please choose a LETTER')
		else:
			return guess

def play_again():
	print('Do you want to play again?')
	return input().lower().startswith('y')


print('H A N G M A N')
missed_words = ''
correct_words = ''
secret_word = get_random_word()
game_over = False

while True:
	current_state(missed_words, correct_words, secret_word)
	
	guess = go_guess(missed_words + correct_words)
	if guess in secret_word:
		correct_words = correct_words + guess

		found_all = True
		for letter in secret_word:
			if letter not in correct_words:
				found_all = False
		if found_all:
			print('You have guessed the word!')
			game_over = True

	else:
		missed_words = missed_words + guess
		if len(missed_words) == len(HANGMAN) - 1:
			current_state(missed_words, correct_words, secret_word)
			print('You have ran out of guesses!')
			print('The word was ' + secret_word)
			game_over = True

	if game_over:
		if play_again():
			missed_words = ''
			correct_words = ''
			secret_word = get_random_word()
			game_over = False
		else:
			break
