import random


def draw_board(board):
	print('   |   |')
	print(' ' + board[7] + ' | ' + board[8] + ' | ' + board[9])
	print('--------')
	print('   |   |')
	print(' ' + board[4] + ' | ' + board[5] + ' | ' + board[6])
	print('--------')
	print('   |   |')
	print(' ' + board[1] + ' | ' + board[2] + ' | ' + board[3])
	print('   |   |')

def input_player_letter():
	letter = ''
	while not (letter == 'X' or letter == 'O'):
		print('Do you want to be X or O?')
		letter = input().upper()

	if letter == 'X':
		return ['X', 'O']
	else:
		return ['O', 'X']


def first_turn():
	if random.randint(0, 1) == 0:
		return 'computer'
	else:
		return 'player'

def play_again():
	print('Do you want to play again? yes or no')
	return input().lower().startswith('y')

def make_move(board, move, position):
	board[position] = move

def is_winner(bo, le):
	return ((bo[7] == le and bo[8] == le and bo[9] == le) or # across the top
		(bo[4] == le and bo[5] == le and bo[6] == le) or # across the middle
		(bo[1] == le and bo[2] == le and bo[3] == le) or # across the bottom
		(bo[7] == le and bo[4] == le and bo[1] == le) or # down the left side
		(bo[8] == le and bo[5] == le and bo[2] == le) or # down the middle
		(bo[9] == le and bo[6] == le and bo[3] == le) or # down the right side
		(bo[7] == le and bo[5] == le and bo[3] == le) or # diagonal
		(bo[9] == le and bo[5] == le and bo[1] == le)) # diagonal

def copy_board(board):
	dupe_board = []
	for i in board:
		dupe_board.append(i)
	return dupe_board

def is_space_free(board, move):
	return board[move] == ' '


def get_player_move(board):
	move = ' '
	while move not in '1 2 3 4 5 6 7 8 9'.split(' ') or not is_space_free(board, int(move)):
		print('What is your next move? (1-9)')
		move = input()

	return int(move)

def choose_random_move(board, moves_list):
	possible_moves = []
	for i in moves_list:
		if is_space_free(board, i):
			possible_moves.append(i)

	if len(possible_moves) != 0:
		return random.choice(possible_moves)
	else:
		return None

def get_computer_move(board, computer_letter):
	if computer_letter == 'X':
		player_letter = 'O'
	else:
		player_letter = 'X'

	for i in range(1, 10):
		copy = copy_board(board)
		if is_space_free(copy, i):
			make_move(copy, computer_letter, i)
			if is_winner(copy, computer_letter):
				return i


	for i in range(1, 10):
		copy = copy_board(board)
		if is_space_free(copy, i):
			make_move(copy, player_letter, i)
			if is_winner(copy, player_letter):
				return i


	move = choose_random_move(board, [1, 3, 7, 9])
	if move != None:
		return move

	if is_space_free(5):
		return 5

	return choose_random_move(board, [2, 4, 6, 8])

def is_board_full(board):
	for i in range(1, 9):
		if is_space_free(board, i):
			return False

	return True


print('Welcome to Tic Tac Toe')

while True:
	the_board = [' '] * 10
	player_letter, computer_letter = input_player_letter()

	turn = first_turn()
	print('The ' + turn + ' will go first!')
	game_is_playing = True

	while game_is_playing:
		if turn == 'player':
			draw_board(the_board)
			move = get_player_move(the_board)
			make_move(the_board, player_letter, move)

			if is_winner(the_board, player_letter):
				draw_board(the_board)
				print('Hooray! You won')
				game_is_playing = False
			else:
				if is_board_full(the_board):
					draw_board(the_board)
					print('The game is a tie!')
					break
				else:
					turn = 'computer'

		else:
			move = get_computer_move(the_board, computer_letter)
			make_move(the_board, computer_letter, move)

			if is_winner(the_board, computer_letter):
				draw_board(the_board)
				print('The computer has beaten you! You Lose..')
				game_is_playing = False
			else:
				if is_board_full(the_board):
					draw_board(the_board)
					print('The game is a tie!')
					break
				else:
					turn = 'player'

	if not play_again():
		break



