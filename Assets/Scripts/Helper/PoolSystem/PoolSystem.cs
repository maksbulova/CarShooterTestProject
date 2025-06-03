using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Helper.PoolSystem
{
    public class PoolSystem
    {
        private Dictionary<Type, object> _poolDictionary = new Dictionary<Type, object>();

        [Inject] private DiContainer _container;

        public void InitPool<T>(PoolConfig config) where T : class, IPoolableItem
        {
            PoolHolder<T> pool = new PoolHolder<T>(_container, config);
            _poolDictionary.Add(typeof(T), pool);
        }
        
        public T Spawn<T>() where T : class, IPoolableItem
        {
            var pool = GetPool<T>();
            return pool?.GetItem();
        }

        public void Despawn<T>(T item) where T : class, IPoolableItem
        {
            var pool = GetPool<T>();
            pool.Release(item);
        }

        private PoolHolder<T> GetPool<T>() where T : class, IPoolableItem
        {
            PoolHolder<T> holder = null;
            if (_poolDictionary.TryGetValue(typeof(T), out var poolHolder))
            {
                holder = ((PoolHolder<T>)poolHolder);
            }
            else
            {
                Debug.LogError($"{typeof(T)} has no initialized pool");
            }
            return holder;
        }
    }
}
