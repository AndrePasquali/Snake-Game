using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MinoGames.SnakeGame.Snake.Ability
{
    public sealed class SnakeWeaponAbility: SnakeAbility
    {
        public float FireRate = 2F;

        public GameObject BulletPrefab;
        
        public float BulletSpawnDistance = 1f;

        public async override void StartAbility()
        {
            if(IsExecuting) return;
            
            CurrentAbilityState = AbilityState.Executing;
            
            var timeCounter = AbilityDuration;
            while (timeCounter > 0)
            {
                timeCounter -= Time.deltaTime;
                
                Vector3 spawnPosition = transform.position + transform.up * BulletSpawnDistance;

                Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);

                await UniTask.Delay(TimeSpan.FromSeconds(FireRate));
            }

            CurrentAbilityState = AbilityState.Finished;
        }
        
    }
}