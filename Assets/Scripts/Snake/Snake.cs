using System.Collections.Generic;
using System.Linq;
using MinoGames.SnakeGame.Gameplay;
using MinoGames.SnakeGame.Physics;
using MinoGames.SnakeGame.Services;
using MinoGames.SnakeGame.Snake.Movement;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake
{
   public sealed class Snake : MonoBehaviour, IMovementController, ICollidable
   { 
       // Private fields
       private LinkedList<Transform> _segments = new LinkedList<Transform>(); // Holds the list of all snake segments
       [SerializeField] private Transform _segmentPrefab; // Prefab used to instantiate new snake segments
       private Vector2 _direction = Vector2.right; // The current direction the snake is moving in
       private int _initialSize = 4; // The initial size of the snake when it is spawned
       private float _baseSpeed = 5f; // The base speed of the snake
       private float _speedMultiplier = 1f; // A multiplier applied to the speed of the snake
       private float _speedIncrementPerSegment = 0.5f; // The amount the speed of the snake increases per segment
       private float _nextUpdate; // The time of the next update
       private int _numSegments; // The current number of snake segments
       private bool _initialized; // Flag indicating whether the snake has been initialized
       private int _playerId; // The ID of the player controlling the snake
       private ServicePool<Transform> _servicePool;

       private void Start() => Initialize();

       private void Initialize()
       {
           SetupState();

           _servicePool = new ServicePool<Transform>(_segmentPrefab);
       }

       #region Settings
       public void SetSize(int size) => _initialSize = size;
       public void SetSpeed(float speed) => _speedMultiplier = speed;
       public void SetPlayerId(int playerId) => _playerId = playerId;

       #endregion

       private void FixedUpdate() => EveryFrame();

       private void EveryFrame()
       {
           if (Time.time < _nextUpdate) return;

           for (var i = _segments.Count - 1; i > 0; i--) _segments.ElementAt(i).position = _segments.ElementAt(i - 1).position;
           
           UpdatePosition();

           _nextUpdate = Time.time + (1f / (GetSpeed() * _speedMultiplier));
       }

       private void UpdatePosition()
       {
           float x = Mathf.Round(transform.position.x) + _direction.x;
           float y = Mathf.Round(transform.position.y) + _direction.y;
           Vector2 newPosition = new Vector2(x, y);

           transform.position = newPosition;
       }
   
       public void AddSegment()
       {
           Transform segment = Instantiate(_segmentPrefab);

           var segmentColor = ServiceLocator.Get<GameManager>().GetColorByPlayer(_playerId);

           segment.GetComponent<SpriteRenderer>().color = segmentColor * 3.5f;

           if (_segments.Last != null) segment.position = _segments.Last.Value.position;
           _segments.AddLast(segment);

           _numSegments++;
       }
   
       public void SetupState()
       {
           _direction = Vector2.right;
          if(_playerId == 0) transform.position = Vector3.zero;
   
           for (int i = 1; i < _segments.Count; i++) {
               Destroy(_segments.ElementAt(i).gameObject);
           }
   
           _segments.Clear();
           _segments.AddLast(transform);
           
           _numSegments = 1;
   
           for (int i = 0; i < _initialSize - 1; i++) AddSegment();

           _initialized = true;
       }
       
       public void SetDirection(Vector2Int direction)
       {
           _direction = direction;
       }
       
       private float GetSpeed()
       {
           return _baseSpeed + (_numSegments * _speedIncrementPerSegment);
       }
       
       public void OnTriggerEnter2D(Collider2D other)
       {
           CollisionManager.OnAnyCollision.Invoke(other.gameObject, _playerId);
       }
   }
}
