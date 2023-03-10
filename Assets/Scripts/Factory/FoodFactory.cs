using System.Collections.Generic;
using MinoGames.SnakeGame.Item;
using MinoGames.SnakeGame.Services;
using UnityEngine;

namespace MinoGames.SnakeGame.Factory
{
    public class FoodFactory
    {
        private ServicePool<Food> _servicePool;
        public FoodFactory(Food foodPrefab) => _servicePool = new ServicePool<Food>(foodPrefab);
        
        public void Build(ref List<Vector2Int> occupiedPositions, BoxCollider2D grid)
        {
            var availablePositions = new List<Vector2Int>();
            
            for (var x = (int)grid.bounds.min.x; x < (int)grid.bounds.max.x; x++)
            {
                for (var y = (int)grid.bounds.min.y; y < (int)grid.bounds.max.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    if (!occupiedPositions.Contains(position))
                    {
                        availablePositions.Add(position);
                    }
                }
            }
            
            if (availablePositions.Count > 0)
            {
                var randomIndex = Random.Range(0, availablePositions.Count);
                var position = availablePositions[randomIndex];

                var food = _servicePool.GetObject().gameObject;

                if (food != null)
                {
                    food.transform.position = new Vector3(position.x, position.y, 0);
                
                    occupiedPositions.Add(position);
                }
            }
        }
    }
}