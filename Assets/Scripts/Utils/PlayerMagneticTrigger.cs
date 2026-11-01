using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase coin = other.GetComponent<ItemCollectableBase>();
        if (coin != null)
        {
            coin.gameObject.AddComponent<Magnetic>();
        }
    }
}
