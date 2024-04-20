using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour, ICollectible
{

    public static event Action OnItemCollected;

    public void Collect()
    {
        Debug.Log("Item collected");
        Destroy(gameObject);
        OnItemCollected?.Invoke();
    }
}
