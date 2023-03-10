using System;
using UnityEngine;

namespace MinoGames.SnakeGame.Gameplay
{
    public sealed class CameraFollow: MonoBehaviour
    {
        [SerializeField] private float _smoothing = 5f; 
        [SerializeField] private Vector3 _offset;

        private void LateUpdate() => EveryFrame();
        private void EveryFrame()
        {
            try
            {
                var player = GameObject.FindGameObjectWithTag("Player");

                var desiredPosition = player.transform.position + _offset;
                desiredPosition.z = -10;
            
                var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothing * Time.deltaTime);
                transform.position = smoothedPosition;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}