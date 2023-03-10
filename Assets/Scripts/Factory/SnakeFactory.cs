using UnityEngine;

namespace MinoGames.SnakeGame.Factory
{
    public class SnakeFactory
    {
        public Snake.Snake Build(Snake.Snake snakePrefab)
        {
            var snake = GameObject.Instantiate(snakePrefab);

            return snake;
        }
    }
}