namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class AIToggle : MonoBehaviour
    {

        public bool isToggled;

        public GameObject FSMStart;
        public GameObject UStart;

        void LateUpdate()
        {

            if (isToggled)
            {
                FSMStart.SetActive(false);
                UStart.SetActive(true);

            }
            else if (!isToggled)
            {
                FSMStart.SetActive(true);
                UStart.SetActive(false);
            }

        }

        public void setToggle(bool t)
        {
            isToggled = t;
        }
    }

}
