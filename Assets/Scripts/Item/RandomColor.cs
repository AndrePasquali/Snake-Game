using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MinoGames.SnakeGame.Item
{
    public class RandomColor : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());
        private SpriteRenderer _spriteRenderer;
        private bool _active = true;
        [SerializeField] private float _speed = 1.0f;

        private void Start() => ExecuteRandomColor();
        
        private async void ExecuteRandomColor()
        {
            while (_active)
            {
                SpriteRenderer.color = Random.ColorHSV();

                await UniTask.Delay(TimeSpan.FromSeconds(_speed));
            }
        }

        private void OnDestroy() => _active = false;
    }
}
