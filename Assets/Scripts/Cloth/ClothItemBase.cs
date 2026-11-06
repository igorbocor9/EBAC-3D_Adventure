using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string compareTag = "Player";

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("Collected");
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
