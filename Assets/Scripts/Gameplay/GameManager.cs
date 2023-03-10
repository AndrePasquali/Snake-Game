using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MinoGames.SnakeGame.Services;
using MinoGames.SnakeGame.Snake;
using MinoGames.SnakeGame.Snake.Ability;
using MinoGames.SnakeGame.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MinoGames.SnakeGame.Gameplay
{ 
	public sealed class GameManager : MonoBehaviour 
	{ 
		[Header("GAME PARAMETERS")] [Range(1, 5)] [Tooltip("Default size of player snake ")] [SerializeField] 
		private int _initialSnakeSize;

		[Tooltip("Number of extra players managed by AI ")] [Range(1, 5)] [SerializeField]
		private int _numberOfPlayers;

		[SerializeField] private float _speedMultiplier = 1;

		[Range(0.5F, 100)] [Tooltip("The Board Size")] [SerializeField]
		private float _boardSize;

		[SerializeField] private Transform _board;
		[SerializeField] private Transform _grid;

		[Tooltip("Player colors by player order")] [SerializeField]
		private Color[] _playerColors;

		public SnakeManager SnakeManager => _snakeManager ?? (_snakeManager = GetComponent<SnakeManager>());
		private SnakeManager _snakeManager;

		// Define public properties that can be accessed by other scripts
		public static Action OnGameStartAction;
		public static Action<Snake.Snake> OnPlayerStartAction;
		public static Action<int> OnPlayerEatFoodAction;
		public static Action<int> OnPlayerHitWallAction;
		public static Action<int> OnPlayerGetWeaponAction;

		// Define private fields that are used in the GameManager class
		private Dictionary<int, Snake.Snake> _players = new Dictionary<int, Snake.Snake>();

		private void Start()
		{
			// Register the GameManager instance with the ServiceLocator
			ServiceLocator.Register(this);

			// Assign event handlers to events
			OnPlayerEatFoodAction += OnPlayerEat;
			OnPlayerHitWallAction += OnPlayerHitWall;
			OnGameStartAction += OnGameStart;
			OnPlayerGetWeaponAction += OnPlayerWeapon;
			
			// Set up the game
			SetupGame();
		}

		// get a list of players
		public List<Snake.Snake> GetPlayers()
		{ 
			var players = _players.Values.ToList();

			return players;
		}

		private void SetBoardSize()
		{ 
			// Set the size of the board and grid
			_board.localScale = new Vector3(_boardSize, _boardSize, _boardSize);
			_grid.localScale = new Vector3(_boardSize, _boardSize, _boardSize);
		}

		private void SetInitialSnakeSize()
		{
			// Set the initial size of the player's snake
			_players.First().Value.SetSize(_initialSnakeSize);
		}

		private void SetSpeed()
		{
			// Set the speed of the player's snake
			_players.First().Value.SetSpeed(_speedMultiplier);
		}

		private void CreatePlayers()
		{
			var player1 = SnakeManager.CreatePlayer();

			// Assign the player to player ID 0
			AssignPlayer(player1);

			// Create the extra players that are managed by AI
			for (int i = 1; i < _numberOfPlayers; i++)
			{ 
				// Create an extra player
				var extraPlayer = SnakeManager.CreateAI();

				extraPlayer.SetPlayerId(i);

				// Set the player's position to a random point inside the grid
				extraPlayer.transform.position = GetRandomPointInBoxCollider();

				// Set the player's color based on the player order
				extraPlayer.GetComponent<SpriteRenderer>().color = _playerColors[i];

				// Assign the AI player to the corresponding player ID
				AssignAI(i, extraPlayer);

			}
		}

		private void AssignPlayer(Snake.Snake player)
		{ 
			_players.Add(0, player);
		}

		private void AssignAI(int playerId, Snake.Snake Ai)
		{
			_players.Add(playerId, Ai);
		}

		#region Events

		private void OnPlayerEat(int playerId)
		{ 
			var player = _players.Single(e => e.Key == playerId).Value;

			// Make the player's snake grow
			player.AddSegment();
		}

		private void OnPlayerHitWall(int playerId)
		{
			var player = _players.Single(e => e.Key == playerId).Value;

			// Reset the player's state
			player.SetupState();

			if (playerId != 0) player.transform.position = GetRandomPointInBoxCollider();
		}

		private void OnPlayerWeapon(int playerId)
		{
			var player = _players.Single(e => e.Key == playerId).Value;

			var weapon = player.GetComponent<SnakeWeaponAbility>();

			weapon.StartAbility();
		}

		private void OnGameStart() => ShowScore();

		private async void ShowScore()
		{ 
			await UniTask.Delay(1000);

			// Loop through each player and show their score
			for (var i = 0; i < _players.Count; i++) ScoreController.OnPlayerScore.Invoke(i);
		}


		#endregion

		private Vector2 GetRandomPointInBoxCollider()
		{
			// Get the grid's box collider
			var gridBox = _grid.GetComponent<BoxCollider2D>();

			// Generate a random X coordinate within the grid
			var randomX = Random.Range(gridBox.bounds.min.x, gridBox.bounds.max.x);

				// Generate a random Y coordinate within the grid
				var randomY = Random.Range(gridBox.bounds.min.y, gridBox.bounds.max.y);

				// Return a vector representing the random point within the grid
				return new Vector2(randomX, randomY);
		}

		public Color GetColorByPlayer(int playerId) => _playerColors[playerId];


		private void SetupGame()
		{
			// Set the size of the game board
			SetBoardSize();

			// Create the players
			CreatePlayers();

			// Set the initial size of the player's snake
			SetInitialSnakeSize();

			// Set the speed of the player's snake
			SetSpeed();

			// Invoke the OnPlayerStartAction with the player object for player ID 0
			OnPlayerStartAction.Invoke(_players.First().Value);

			OnGameStartAction.Invoke();
		}
	} 
}