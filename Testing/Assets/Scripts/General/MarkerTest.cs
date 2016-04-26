namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class MarkerTest : MonoBehaviour
    {

        public AnimationClip clip;
        public Animation anim;
        public bool pressed = false;
        public bool Trigger
        {
            get { return pressed; }
            set { pressed = value; }
        }

        public GameObject[] childrenGO;





        void Start()
        {
            StartCoroutine("CoStart");
           
            anim = GetComponent<Animation>();

        }


        IEnumerator CoStart()
        {
            while (true)
                yield return StartCoroutine("CoUpdate");
        }

        IEnumerator CoUpdate()
        {
            if(pressed)
            {
                for (int i = 0; i < childrenGO.Length; i++)
                {
                    childrenGO[i].SetActive(true);
                }
                anim.CrossFade(clip.name);
                yield return new WaitForSeconds(anim.GetClip(clip.name).length);
                for (int i = 0; i < childrenGO.Length; i++)
                {
                    childrenGO[i].SetActive(false);
                }
                pressed = false;

                
            }

            
            yield return null;
        }

    }
}

