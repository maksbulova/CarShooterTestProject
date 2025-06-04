using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Helper.PoolSystem
{
    public class PoolSystem
    {
        private Dictionary<GameObject, object> _poolByPrefab = new Dictionary<GameObject, object>();
        private Dictionary<GameObject, object> _spawnedByPooledItem = new Dictionary<GameObject, object>();

        [Inject] private DiContainer _container;

        public void InitPool<T>(PoolConfig config) where T : Component, IPoolableItem
        {
            PoolHolder<T> pool = new PoolHolder<T>(_container, config);
            _poolByPrefab.Add(config.Prefab, pool);
        }
        
        public T Spawn<T>(T prefab) where T : Component, IPoolableItem
        {
            var pool = GetPool<T>(prefab, _poolByPrefab);
            var item = pool.GetItem();
            _spawnedByPooledItem.Add(item.gameObject, pool);
            return item;
        }

        public void Despawn<T>(T prefab) where T : Component, IPoolableItem
        {
            var pool = GetPool<T>(prefab, _spawnedByPooledItem);
            _spawnedByPooledItem.Remove(prefab.gameObject);
            pool.Release(prefab);
        }

        private PoolHolder<T> GetPool<T>(T prefab, Dictionary<GameObject, object> dictionary) where T : Component, IPoolableItem
        {
            PoolHolder<T> holder = null;
            if (dictionary.TryGetValue(prefab.gameObject, out var poolHolder))
                holder = ((PoolHolder<T>)poolHolder);
            else
                Debug.LogError($"{prefab.name} has no initialized pool");
            return holder;
        }
    }
}
