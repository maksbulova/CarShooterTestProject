using Cysharp.Threading.Tasks;
using Helper.PoolSystem;
using UnityEngine;
using Zenject;

public class ShotController : MonoBehaviour
{
    [SerializeField] private PoolConfig bulletPrefabPoolConfig;
    [SerializeField] private Transform shotOrigin;
    [SerializeField] private Transform bulletHolder;
    
    [Inject] private PoolSystem _poolSystem;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
    }

    public void Init()
    {
        _poolSystem.InitPool<Projectile>(bulletPrefabPoolConfig);
    }
    
    public async UniTaskVoid Shot()
    {
        var bullet = _poolSystem.Spawn<Projectile>();
        
        bullet.transform.SetPositionAndRotation(shotOrigin.position, shotOrigin.rotation);
        bullet.transform.SetParent(bulletHolder);

        await UniTask.WaitForSeconds(3);
        
        _poolSystem.Despawn(bullet);
    }
}
