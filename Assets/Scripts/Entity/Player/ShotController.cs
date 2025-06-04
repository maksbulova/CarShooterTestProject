using Cysharp.Threading.Tasks;
using Helper.PoolSystem;
using UnityEngine;
using Zenject;

public class ShotController : MonoBehaviour
{
    [SerializeField] private PoolConfig bulletPrefabPoolConfig;
    [SerializeField] private ProjectileStats projectileStats;
    [SerializeField] private Transform shotOrigin;
    [SerializeField] private Transform bulletHolder;
    // TODO Scriptable config
    [SerializeField] private float shotCooldown = 0.5f;
    
    [Inject] private PoolSystem _poolSystem;

    private float _lastShotTime;
    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _poolSystem.InitPool<Projectile>(bulletPrefabPoolConfig);
    }

    public void TryShot()
    {
        if (Time.time >= _lastShotTime + shotCooldown)
        {
            Shot();
            _lastShotTime = Time.time;
        }
    }
    
    private void Shot()
    {
        var bullet = _poolSystem.Spawn<Projectile>();
        
        bullet.transform.SetPositionAndRotation(shotOrigin.position, Quaternion.LookRotation(shotOrigin.forward));
        bullet.transform.SetParent(bulletHolder);

        bullet.Launch(projectileStats);
    }
}
