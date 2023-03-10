using MinoGames.SnakeGame.Factory;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake
{
    public class SnakeManager: MonoBehaviour
    {
        [SerializeField] private Snake _snakePrefab;
        [SerializeField] private Snake _snakeAIPrefab;

        public Snake CreatePlayer()
        {
            var snakeFactory = new SnakeFactory();
            var player1 = snakeFactory.Build(_snakePrefab);

            return player1;
        }

        public Snake CreateAI()
        {
            var snakeFactory = new SnakeFactory();
            var player2 = snakeFactory.Build(_snakeAIPrefab);

            return player2;
        }
    }
}