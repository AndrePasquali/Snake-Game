using UnityEngine;

namespace MinoGames.SnakeGame.Snake.Movement
{
    public class MoveCommand: ICommand
    {
        private Snake _snake;
        private Vector2Int _direction;

        public MoveCommand(Snake snake, Vector2Int direction)
        {
            _snake = snake;
            _direction = direction;
        }
        
        public void Execute()
        {
            _snake.SetDirection(_direction);
        }
    }
}