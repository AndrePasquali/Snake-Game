using System;
using System.Collections.Generic;
using MinoGames.SnakeGame.Gameplay;
using MinoGames.SnakeGame.Item;
using MinoGames.SnakeGame.UI;
using UnityEngine;

namespace MinoGames.SnakeGame.Physics
{
    public sealed class CollisionManager: MonoBehaviour
    {
        private List<ICollidable> _collidables;

        public static Action<GameObject, int> OnAnyCollision;
        private void Start() => OnAnyCollision += HandleCollisions;

        private void HandleCollisions(GameObject gameObjectCollided, int playerId)
        {
            switch (gameObjectCollided.tag)
            {
                case "Food":
                    GameManager.OnPlayerEatFoodAction.Invoke(playerId);
                    ScoreController.OnPlayerScore.Invoke(playerId);
                    FoodManager.OnFoodEatedAction.Invoke(gameObjectCollided.GetComponent<Food>());
                    break;
                case "Wall":
                    GameManager.OnPlayerHitWallAction.Invoke(playerId);
                    break;
                case "Player": break;
                case "Weapon":
                {
                    GameManager.OnPlayerGetWeaponAction.Invoke(playerId);
                    Destroy(gameObjectCollided.gameObject);
                    break;
                }
            }
        }
    }
}