using System;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake.Ability
{
    public abstract class SnakeAbility: MonoBehaviour
    {
        public enum AbilityState
        {
            Stopped,
            Executing,
            Finished
        }

        public AbilityState CurrentAbilityState
        {
            get => _currentAbilityState;
            set
            {
                switch (value)
                {
                    case AbilityState.Executing: OnAbilityStartAction?.Invoke();
                        break;
                    case AbilityState.Finished: OnAbilityFinishedAction?.Invoke();
                        break;
                }
            }
        }

        private AbilityState _currentAbilityState;

        protected Action OnAbilityStartAction;
        protected Action OnAbilityFinishedAction;

        protected bool IsExecuting => CurrentAbilityState == AbilityState.Executing;
        protected bool IsStopped => CurrentAbilityState == AbilityState.Stopped;

        public float AbilityDuration = 5F;

        private void Start()
        {
            OnAbilityStartAction += OnAbilityStart;
            OnAbilityFinishedAction += OnAbilityFinished;
        }

        public abstract void StartAbility();

        public virtual void OnAbilityStart()
        {
            
        }

        public virtual void OnAbilityFinished()
        {
            
        }

        private void OnDestroy()
        {
            OnAbilityStartAction -= OnAbilityStart;
            OnAbilityFinishedAction -= OnAbilityFinished;
        }
    }
}