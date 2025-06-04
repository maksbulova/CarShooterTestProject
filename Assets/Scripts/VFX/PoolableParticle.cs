using PoolSystem;
using UnityEngine;
using Zenject;

public class PoolableParticle : MonoBehaviour, IPoolableItem
{
    [SerializeField] private ParticleSystem particleSystem;

    [Inject] private PoolSystem.PoolSystem _poolSystem;
    
    /// <summary>
    /// Called by particle system callback
    /// </summary>
    public void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
        _poolSystem.Despawn(this);
    }
    
    public void CreateByPool()
    {
    }

    public void GetByPool()
    {
        gameObject.SetActive(true);
        particleSystem.Clear();
        particleSystem.Play();
    }

    public void ReleaseByPool()
    {
    }

    public void DestroyByPool()
    {
    }
}
