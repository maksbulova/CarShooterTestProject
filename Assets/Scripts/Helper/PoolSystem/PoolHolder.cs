using System;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace PoolSystem
{
    public class PoolHolder<T> where T : class, IPoolableItem
    {
        private DiContainer _container;
        private PoolConfig _config;
        private Transform _parent;

        private IObjectPool<T> _pool;

        public PoolHolder(DiContainer container, PoolConfig config, Transform parent)
        {
            _container = container;
            _config = config;
            _parent = parent;

            switch (config.PoolType)
            {
                case PoolType.Stack:
                    _pool = new ObjectPool<T>(
                        CreateFunc,
                        ActionOnGet,
                        ActionOnRelease,
                        ActionOnDestroy,
                        maxSize: config.PooledAmount);
                    break;
                case PoolType.LinkedList:
                    _pool = new LinkedPool<T>(
                        CreateFunc,
                        ActionOnGet,
                        ActionOnRelease,
                        ActionOnDestroy,
                        maxSize: config.PooledAmount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(config.PoolType), config.PoolType, null);
            }
        }

        private T CreateFunc()
        {
            var prefab = _container.InstantiatePrefabForComponent<T>(_config.Prefab, _parent);
            prefab.CreateByPool();
            return prefab;
        }

        private void ActionOnGet(T item)
        {
            item.GetByPool();
        }

        private void ActionOnRelease(T item)
        {
            item.ReleaseByPool();
        }

        private void ActionOnDestroy(T item)
        {
            item.DestroyByPool();
        }

        public T GetItem() => _pool.Get();

        public void Release(T item) => _pool.Release(item);
    }
}