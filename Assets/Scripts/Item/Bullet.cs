using UnityEngine;

namespace MinoGames.SnakeGame.Item
{
    public class Bullet : MonoBehaviour
    {
        public float Speed = 10f;

        public float LifeTime = 2f;

        void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            rigidbody.AddForce(transform.up * Speed, ForceMode2D.Impulse);

            Destroy(gameObject, LifeTime);
        }
    }
}