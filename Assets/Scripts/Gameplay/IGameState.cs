namespace MinoGames.SnakeGame.Gameplay
{
    public interface IGameState
    {
        public void SnakeHitWall();
        public void SnakeEatFood();
        public void EndGame();

        public void StartGame();
    }
}