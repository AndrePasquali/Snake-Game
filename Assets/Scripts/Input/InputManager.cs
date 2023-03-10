using System;
using System.Collections.Generic;
using System.Linq;
using MinoGames.SnakeGame.Gameplay;
using MinoGames.SnakeGame.Services;
using MinoGames.SnakeGame.Snake.Movement;
using UnityEngine;

namespace MinoGames.SnakeGame.GameInput
{
    public class InputManager : MonoBehaviour
    {
        private Dictionary<Direction, ICommand> _inputCommands = new Dictionary<Direction, ICommand>();
        private Snake.Snake _player;

        public Direction CommandDirection
        {
            get => _commandDirection;

            set
            {
                _inputCommands[value].Execute();
                _commandDirection = value;
            }

        }

        private Direction _commandDirection;

        private void Start()
        {
            GameManager.OnPlayerStartAction += OnGameStart;
        }

        private void Initialize()
        {
            _inputCommands.Add(Direction.Up, new MoveCommand(_player, Vector2Int.up));
            _inputCommands.Add(Direction.Down, new MoveCommand(_player, Vector2Int.down));
            _inputCommands.Add(Direction.Left, new MoveCommand(_player, Vector2Int.left));
            _inputCommands.Add(Direction.Right, new MoveCommand(_player, Vector2Int.right));
        }

        public void OnGameStart(Snake.Snake player)
        {
            _player = player;
            
            Initialize();
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
                CommandDirection = Direction.Up;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                CommandDirection = Direction.Down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                CommandDirection = Direction.Left;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                CommandDirection = Direction.Right;
        }
    }
}