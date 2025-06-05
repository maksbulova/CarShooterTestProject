using System;
using System.Collections;
using System.Collections.Generic;
using PoolSystem;
using TMPro;
using UnityEngine;

public class PoolableText : MonoBehaviour, IPoolableItem
{
    [SerializeField] private TMP_Text text;

    public TMP_Text Text => text;
    
    public void CreateByPool()
    {
    }

    public void GetByPool()
    {
        text.text = String.Empty;
        gameObject.SetActive(true);
    }

    public void ReleaseByPool()
    {
        gameObject.SetActive(false);
    }

    public void DestroyByPool()
    {
    }
}
