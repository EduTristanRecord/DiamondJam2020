using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawn : MonoBehaviour
{
    public Collectible containedCollectible { get; private set; }

    private Coroutine despawnCollectible;

    public event Action OnRemoved;

    public void SetCollectible(Collectible col)
    {
        containedCollectible = col;
    }

    private void OnTriggerEnter()
    {
        // TODO
        
        // The trigger should be on the item itself, and fire an event when it is picked up

        // The CollectibleSpawn script should subscribe to the item's OnCollect when it is spawned
        // The CollectibleSpawn script should unsubscribe from the item's OnCollect when it is collected or destroyed

        // Can probably set that event/trigger on the collectible item itself
    }

    /// <summary>
    /// Debug function to replace the OnTriggerEnter since we dont have a player yet
    /// </summary>
    public void OnMouseDown()
    {
        if (containedCollectible == null)
        {
            Debug.Log("Nothing to pickup");
            return;
        }

        Debug.Log("Picking up!");

        // TODO - Pickup item here

        if (despawnCollectible != null)
        {
            StopCoroutine(despawnCollectible);
            despawnCollectible = null;
        }

        // TEMP DEBUG
        Destroy(containedCollectible.gameObject);
        // TEMP DEBUG

        containedCollectible = null;
        OnRemoved?.Invoke();
    }

    public void DespawnAfter(float delay)
    {
        if (despawnCollectible != null)
        {
            StopCoroutine(despawnCollectible);
            despawnCollectible = null;
        }
        despawnCollectible = StartCoroutine(Despawn(delay));
    }

    private IEnumerator Despawn(float delay)
    {
        float elapsed = 0f;

        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }

        Destroy(containedCollectible.gameObject);
        containedCollectible = null;
        OnRemoved?.Invoke();
    }
}
