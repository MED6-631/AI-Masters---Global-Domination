namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class DMGUP : MonoBehaviour
    {

        public int DMGBoost;
        public float time;
        private Renderer[] children;

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag.Contains("Player"))
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                children = this.gameObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].enabled = false;
                }
                other.GetComponent<PlayerController>().Damage = DMGBoost;
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isDMGBoost = true;
                StartCoroutine(boostTime(time));
            }
            else if(other.gameObject.tag.Contains("companion"))
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                children = this.gameObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].enabled = false;
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Damage = DMGBoost;
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isDMGBoost = true;
                StartCoroutine(boostTime(time));
            }

        }

        IEnumerator boostTime(float t)
        {
            yield return new WaitForSeconds(t);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Damage = 40;
            Destroy(this.gameObject);
        }

    }
}

