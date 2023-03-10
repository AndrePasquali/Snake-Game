using UnityEngine;

namespace MinoGames.SnakeGame.Snake
{
    public interface ISnakeCollisionListener
    {
        public void OnCollisionEnter(Collision2D collision);
        public void OnTriggerEnter(Collider2D other);
    }

}