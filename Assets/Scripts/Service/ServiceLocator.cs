using System;
using System.Collections.Generic;
using UnityEngine;

namespace MinoGames.SnakeGame.Services
{
    public sealed class ServiceLocator: MonoBehaviour
    {
        private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public static void Register<T>(T service) where T : class
        {
            _services[typeof(T)] = service;
        }

        public static T Get<T>() where T : class
        {
            object service;
            
            if (_services.TryGetValue(typeof(T), out service))
            {
                return (T)service;
            }
            else
            {
                throw new Exception("Service not found: " + typeof(T).ToString());
            }
        }
    }
}