using UnityEngine;
namespace MinoGames.SnakeGame.GameInput
{
    public interface IInputObserver
    {
        void OnInputReceived(Vector3 direction);
    }
}