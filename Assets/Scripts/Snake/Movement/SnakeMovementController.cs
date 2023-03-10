using MinoGames.SnakeGame.GameInput;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake.Movement
{
    public class SnakeMovementController: IMovementController
    {
        private Vector2Int currentDirection = Vector2Int.right;

        public SnakeMovementController()
        {
        }

        public void SetDirection(Vector2Int direction)
        {
            throw new System.NotImplementedException();
        }
    }
}