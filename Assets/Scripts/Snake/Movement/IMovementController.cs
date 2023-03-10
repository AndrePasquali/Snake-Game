using System.Numerics;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake.Movement
{
    public interface IMovementController
    {
        public void SetDirection(Vector2Int direction);
    }
}