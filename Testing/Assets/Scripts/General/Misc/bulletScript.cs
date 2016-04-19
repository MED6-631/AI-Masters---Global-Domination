namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class bulletScript : MonoBehaviour
    {

        public float lifeTime = 1;
        public float bulletSpeed;

        void FixedUpdate()
        {

            transform.Translate(Vector3.up * bulletSpeed);
            if (lifeTime <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }

            lifeTime -= Time.deltaTime;

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "unit" || other.gameObject.tag == "obstacle" || other.gameObject.tag == "structure")
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

}
