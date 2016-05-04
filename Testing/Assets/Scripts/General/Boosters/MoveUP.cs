namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class MoveUP : MonoBehaviour
    {
        public float speedBoost;
        public float time;
        public float fastSpeedBoost;
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
                other.GetComponent<PlayerController>().speed = speedBoost;
                other.GetComponent<PlayerController>().fastSpeed = fastSpeedBoost;
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isMoveBoost = true;
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
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().speed = speedBoost;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fastSpeed = fastSpeedBoost;
                GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>().isMoveBoost = true;
                StartCoroutine(boostTime(time));
            }

        }

        IEnumerator boostTime(float t)
        {
            yield return new WaitForSeconds(t);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().speed = 5;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fastSpeed = 10;


            Destroy(this.gameObject);

        }
    }

}
