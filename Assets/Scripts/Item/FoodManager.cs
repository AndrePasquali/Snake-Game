using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MinoGames.SnakeGame.Factory;
using MinoGames.SnakeGame.Gameplay;
using MinoGames.SnakeGame.Services;
using UnityEngine;

namespace MinoGames.SnakeGame.Item
{
    public class FoodManager: MonoBehaviour
    {
        [SerializeField] private float _foodSpawnInterval = 5F;
        [SerializeField] private int _foodsCount;
        [SerializeField] private int _maxFoodCount = 5;
        private List<Vector2Int> _occupiedPositions = new List<Vector2Int>();
        [SerializeField] private BoxCollider2D _grid;
        [SerializeField] private Food _foodPrefab;
        private FoodFactory _foodFactory;
        private ServicePool<Food> _servicePool;

        public static Action<Food> OnFoodEatedAction;

        private void Start()
        {
            GameManager.OnGameStartAction += HandleGameFoods;
            OnFoodEatedAction += OnFoodEated;

            _servicePool = new ServicePool<Food>(_foodPrefab);
            _foodFactory = new FoodFactory(_foodPrefab);
        }
        private async void HandleGameFoods()
        {
            while (true)
            {
                UpdateFoodsCount();
                
                SpawnFood();

                await UniTask.Delay(TimeSpan.FromSeconds(_foodSpawnInterval));
            }
            
        }
        public void SpawnFood()
        {
            if(_foodsCount >= _maxFoodCount) return;
            
            _foodFactory.Build(ref _occupiedPositions, _grid);
        }
        private void UpdateFoodsCount()
        {
            _foodsCount = FindObjectsOfType<Food>().Length;
        }

        private void OnFoodEated(Food food)
        {
            _servicePool.ReturnObject(food);
        }
    }
}