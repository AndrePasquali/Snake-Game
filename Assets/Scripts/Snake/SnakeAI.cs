using System.Collections.Generic;
using MinoGames.SnakeGame.Gameplay;
using MinoGames.SnakeGame.Services;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake 
{
    public sealed class SnakeAI : MonoBehaviour
    {
        private List<GameObject> _foodList;
        private Snake _snake;
        private float _nextDecisionTime;
        private float _decisionDelay = 0.5F;

        private void Start()
        {
            _foodList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Food"));
            _snake = GetComponent<Snake>();
            _decisionDelay = Random.Range(0.1F, _decisionDelay);
        }


        private void Update() => EveryFrame();

        private void EveryFrame()
        {
            // If it's not time to make a decision yet, return
            if (Time.time < _nextDecisionTime) return;

            // Finding the closest food to the snake
            Transform closestFood = FindClosestFood();
            if (closestFood != null)
            {
                // If there is another snake nearby, avoid it
                Transform closestSnake = FindClosestSnake();
                if (closestSnake != null)
                {
                    AvoidSnake(closestSnake); 
                }
            
                // Update the snake's direction to move towards the closest food
                UpdateDirection(closestFood.position);
            }
        
            // Update the list of food objects
            _foodList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Food"));
        
            // Set the time for the next decision, with a random delay between half and double the decision delay
            _nextDecisionTime = Time.time + Random.Range(_decisionDelay / 2, _decisionDelay * 2);
        }

        // Method to find the closest food object to the snake
        private Transform FindClosestFood()
        {
            Transform closestFood = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject food in _foodList)
            {
                if (food != null)
                {
                    float distance = Vector2.Distance(transform.position, food.transform.position);
                    if (distance < closestDistance)
                    {
                        closestFood = food.transform;
                        closestDistance = distance;
                    }
                }
            }

            return closestFood ?? new RectTransform();
        }

        // Method to find the closest snake to the current snake
        private Transform FindClosestSnake()
        {
            Transform closestSnake = null;
            float closestDistance = Mathf.Infinity;

            var players = ServiceLocator.Get<GameManager>().GetPlayers();

            foreach (var otherSnake in players)
            {
                if (otherSnake.gameObject == gameObject) continue;

                float distance = Vector2.Distance(transform.position, otherSnake.transform.position);
                if (distance < closestDistance)
                {
                    closestSnake = otherSnake.transform;
                    closestDistance = distance;
                }
            }

            return closestSnake;
        }

        // Method to avoid another snake if it is nearby
        private void AvoidSnake(Transform otherSnake)
        {
            // Calculate the direction to the other snake and an avoid direction perpendicular to it
            Vector2 directionToOtherSnake = (otherSnake.position - transform.position).normalized;
            Vector2 avoidDirection = new Vector2(-directionToOtherSnake.y, directionToOtherSnake.x);
            Vector2Int avoidDirectionInt = Vector2Int.RoundToInt(avoidDirection); 
            
            // Move in the avoid direction
            Move(avoidDirectionInt);

            // Check if the AI snake will hit a wall in the avoid direction and adjust its direction accordingly
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(avoidDirectionInt.x, avoidDirectionInt.y, 0), 0.5f);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Wall"))
                {
                    Vector2 newDirection = new Vector2(-avoidDirection.y, avoidDirection.x);
                    Move(Vector2Int.RoundToInt(newDirection));
                    break;
                }
            }
            
        }

        private void UpdateDirection(Vector2 targetPosition)
        {
            // Calculate the direction to the target position and round it to the nearest integer values
            Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;
            Vector2Int directionToTargetInt = Vector2Int.RoundToInt(directionToTarget);
            
            Move(directionToTargetInt);
          
        }
        public void Move(Vector2Int direction)
        {
            // Ensure the AI snake moves only in the x or y direction but not both at the same time
            if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) == 0 ||
                Mathf.Abs(direction.x) == 0 && Mathf.Abs(direction.y) > 0)
            {
                _snake.SetDirection(direction);

            }
        }
    }
}
