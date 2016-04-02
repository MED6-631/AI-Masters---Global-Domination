namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class MarkerTest : MonoBehaviour
    {

        public float time;
        private float counter;


        void Start()
        {
            counter = time;
        }

        void FixedUpdate()
        {
            if (gameObject.GetComponent<Renderer>().enabled == true)
            {
                TurnSelfInvisible(true);
            }

        }

        public void TurnSelfInvisible(bool invisible)
        {

            counter -= Time.fixedDeltaTime;
            if (invisible && counter <= 0)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }

            if (invisible == false)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                counter = time;
            }
        }
    }
}

