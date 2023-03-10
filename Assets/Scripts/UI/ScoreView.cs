using Extensions;
using MinoGames.SnakeGame.Gameplay;
using MinoGames.SnakeGame.Services;
using TMPro;
using UnityEngine;

namespace MinoGames.SnakeGame.UI
{
    public class ScoreView: MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _playerScore;
        
        public void UpdateScore(int playerId, int newScore)
        {
            if((_playerScore.Length - 1) < playerId) return;
            
            _playerScore[playerId].gameObject.Show();
            _playerScore[playerId].text = $"PLAYER {playerId + 1}\n {newScore}";

            var playerColor = ServiceLocator.Get<GameManager>().GetColorByPlayer(playerId);
            _playerScore[playerId].color = playerColor;
        }
    }
}