namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class Linker : MonoBehaviour
    {

        public GameObject returnButton;
        public GameObject master;

        void Start()
        {
            master = GameObject.FindGameObjectWithTag("master");

        }

        void FixedUpdate()
        {
            
            if(returnButton.GetComponent<OnHold>().buttonHeld == true)
            {
                AcquireMaster();

            }
        }

        public void AcquireMaster()
        {
            master.GetComponent<MasterScript>().LoadScene(0);
            master.GetComponent<MasterScript>().Destroy();
        }
    }

}
