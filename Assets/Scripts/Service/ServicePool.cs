using Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace MinoGames.SnakeGame.Services
{
    public class ServicePool<T> where T : Component
    {
        private T prefab;
        private List<T> objects = new List<T>();
        private Transform parent;

        public ServicePool(T prefab, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public T GetObject()
        {
            // Search for an inactive object in the pool
            for (int i = 0; i < objects.Count; i++)
            {
                if (!objects[i].gameObject.activeSelf)
                {
                    objects[i].gameObject.SetActive(true);
                    return objects[i];
                }
            }

            // No inactive object found, create a new one
            GameObject newGameObject = Object.Instantiate(prefab.gameObject);
            T newObject = newGameObject.GetComponent<T>();

            // Make sure the object is a component of the correct type
            if (newObject == null)
            {
                throw new System.InvalidCastException($"Could not cast {newGameObject.name} to type {typeof(T).Name}");
            }

            newObject.gameObject.SetActive(true);

            // Add the new object to the pool
            objects.Add(newObject);

            // Set the parent of the new object
            if (parent != null)
            {
                newObject.transform.SetParent(parent, false);
            }

            return newObject;
        }

        public void ReturnObject(T obj)
        {
            // Deactivate the object and move it to the pool
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(parent, false);
        }
    }

}