namespace MinoGames.SnakeGame.UI
{
    public class ScoreModel
    {
        public int _playerScore { get; private set; }
        public int _aiScore { get; private set; }

        public void IncreasePlayerScore() => _playerScore++;

        public void IncreaseAiScore() => _aiScore++;

    }
}