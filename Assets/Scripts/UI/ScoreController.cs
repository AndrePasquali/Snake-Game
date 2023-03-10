using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MinoGames.SnakeGame.UI
{
    public class ScoreController: MonoBehaviour
    {
        private ScoreView _scoreView;

        public static Action<int> OnPlayerScore;
        private Dictionary<int, int> _playersScoreBoard = new Dictionary<int, int>();
        
        private void Start()
        {
            _scoreView = GetComponent<ScoreView>();
            OnPlayerScore += IncreasePlayerScore;
        }

        private void IncreasePlayerScore(int playerId)
        {
            RegisterScore(playerId);
            
            var updatedScore = GetScore(playerId);
            
            _scoreView.UpdateScore(playerId, updatedScore);
        }

        private void RegisterScore(int playerId)
        {
            if(_playersScoreBoard.ContainsKey(playerId))
            _playersScoreBoard[playerId] += 1;
            else _playersScoreBoard.Add(playerId, 0);
        }

        private int GetScore(int playerId) => _playersScoreBoard.Single(e => e.Key == playerId).Value;
    }
}