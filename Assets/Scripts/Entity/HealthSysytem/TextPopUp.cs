using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PoolSystem;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private PoolableText popUpPrefab;
    [SerializeField] private PoolConfig popUpPrefabConfig;
    [SerializeField] private Transform popUpHolder;
    [SerializeField] private float jumpDuration = 1;
    [SerializeField] private float jumpDistribution = 1;
    [SerializeField] private float jumpPower = 1;
    
    [Inject] private PoolSystem.PoolSystem _poolSystem;

    private void Start()
    {
        _poolSystem.InitPool<PoolableText>(popUpPrefabConfig);
    }

    public async UniTaskVoid PlayText(string text, Vector3 spawnPosition, Color color)
    {
        var popUp = _poolSystem.Spawn(popUpPrefab);
        popUp.transform.position = spawnPosition;
        popUp.transform.SetParent(popUpHolder);
        popUp.Text.text = text;
        popUp.Text.color = color;
        popUp.transform.localEulerAngles = Vector3.zero;
        popUp.transform.localScale = Vector3.one;
        popUp.transform.DOLocalJump(Random.insideUnitSphere * jumpDistribution,
            jumpPower, 1, jumpDuration);
        popUp.Text.DOFade(0, jumpDuration).From(1).SetEase(Ease.InCubic);

        await UniTask.WaitForSeconds(jumpDuration);
        
        _poolSystem.Despawn(popUp);
    }
}
