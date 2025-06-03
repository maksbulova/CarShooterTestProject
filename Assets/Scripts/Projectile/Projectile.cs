using System.Collections;
using System.Collections.Generic;
using BitToolSet;
using Helper.PoolSystem;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolableItem
{
    [SerializeField] private InteractionTrigger interactionTrigger;
    [SerializeField] private Rigidbody rigidbody;

    private void OnProjectileHit(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public void OnPoolCreate()
    {
    }

    public void OnPoolGet()
    {
        ResetVelocity();
        interactionTrigger.SetInteractable(true);
        interactionTrigger.OnTriggerEnterE += OnProjectileHit;
        gameObject.SetActive(true);
    }

    public void OnPoolRelease()
    {
        ResetVelocity();
        interactionTrigger.SetInteractable(false);
        interactionTrigger.OnTriggerEnterE -= OnProjectileHit;
        gameObject.SetActive(false);
    }

    public void OnPoolDestroy()
    {
    }

    private void ResetVelocity()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
