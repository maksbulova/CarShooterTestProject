using Cysharp.Threading.Tasks;
using PoolSystem;
using UnityEngine;
using Zenject;

public class ShotController : MonoBehaviour
{
    [SerializeField] private PoolConfig bulletPrefabPoolConfig;
    [SerializeField] private Projectile bulletPrefab;
    [Space]
    [SerializeField] private ProjectileStats projectileStats;
    [SerializeField] private Transform shotOrigin;
    [SerializeField] private TurretVFXController vfxController;

    [Inject] private PoolSystem.PoolSystem _poolSystem;

    private float _shotCooldown;
    private float _lastShotTime;

    public void Init(float shotCooldown)
    {
        _shotCooldown = shotCooldown;
        _poolSystem.InitPool<Projectile>(bulletPrefabPoolConfig);
    }

    public void TryShot()
    {
        if (Time.time >= _lastShotTime + _shotCooldown)
        {
            Shot();
            _lastShotTime = Time.time;
        }
    }
    
    private void Shot()
    {
        vfxController.PlayShot();
        
        var bullet = _poolSystem.Spawn(bulletPrefab);
        bullet.transform.SetPositionAndRotation(shotOrigin.position, Quaternion.LookRotation(shotOrigin.forward));
        bullet.Launch(projectileStats);
    }
}
