﻿using UnityEngine;

namespace Common
{
    public class SingletonScene<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}