using UnityEngine;

namespace MinoGames.SnakeGame.Physics
{
    public interface ICollidable
    {
        public void OnTriggerEnter2D(Collider2D other);
    }
}