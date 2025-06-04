using TMPro;
using UnityEngine;
using Zenject;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popUpPrefab;
    [SerializeField] private Transform popUpHolder;
    [SerializeField] private float jumpDuration = 1;
    [SerializeField] private float jumpDistribution = 1;
    [SerializeField] private float jumpPower = 1;
    
    [Inject] private PoolSystem.PoolSystem _poolSystem;

    public void PlayText(string text, Vector3 spawnPosition, Color color)
    {
        // var popUp = poolService.SpawnAndDespawnInTime(popUpPrefab, jumpDuration,
        //     spawnPosition, quaternion.identity, popUpHolder);
        // popUp.text = text;
        // popUp.color = color;
        // popUp.transform.localEulerAngles = Vector3.zero;
        // popUp.transform.localScale = Vector3.one;
        // popUp.transform.DOLocalJump(Random.insideUnitSphere * jumpDistribution,
        //     jumpPower, 1, jumpDuration);
        // popUp.DOFade(0, jumpDuration).From(1).SetEase(Ease.InCubic);
    }
}
