using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;
        public string compareTag = "Player";
        public Collider collider;
        public ParticleSystem particleSystem;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            if (particleSystem != null)
            {
                //particleSystem.transform.SetParent(null);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if (collider != null)
            {
                collider.enabled = false;
            }
            
            if (graphicItem != null)
            {
                graphicItem.SetActive(false);
            }
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        protected virtual void OnCollect()
        {
            if (particleSystem != null)
            {
                particleSystem.Play();
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }

            ItemManager.Instance.AddByType(itemType);
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
