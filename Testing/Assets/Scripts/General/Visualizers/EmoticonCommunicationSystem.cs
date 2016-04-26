namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class EmoticonCommunicationSystem : MonoBehaviour
    {
        public Vector3 playerPos;
        public Vector3 companionPos;
        public int offsetX;
        public int offsetY;
        private PlayerController playerC;
        private PathCompanionUnit companion;
        private StateChecker sChecker;
        private MasterScript master;
        public GameObject co;
        private int rnd;
       
        public Texture[] emoticons;
        private int selectEmoji = 0;
        private int playerInterval = 3;
        private int compaionInterval = 3;
        public float companionHP;
        public float playerHP;
        public bool stated = false;

        public enum PlayerEmotionState
        {
            Happy,
            Sad,
            Angry,
            Neutral,
            Scared,
            Dead,
            None
        }

        public enum CompanionEmotionState
        {
            Happy,
            Sad,
            Angry,
            Neutral,
            Scared,
            Dead,
            None
        }

        public PlayerEmotionState _currentPlayerState = PlayerEmotionState.Neutral;
        public CompanionEmotionState _currentCompanionState = CompanionEmotionState.Neutral;

        void Start()
        {
            playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            companion = GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>();
            sChecker = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateChecker>();
            master = GameObject.FindGameObjectWithTag("master").GetComponent<MasterScript>();
            co = GameObject.FindGameObjectWithTag("companion");
            stated = true;

        }
        
        void FixedUpate()
        {
            playerHP = playerC.currentHealth;
            companionHP = co.GetComponent<PathCompanionUnit>().currentHealth;

            playerPos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
            companionPos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("companion").transform.position);





        }

        void OnGUI()
        {
           rnd = Mathf.RoundToInt(Random.Range(0, 2));

            if(stated)
            {
                
                if (companionHP < 200 && companion.ReturnToPlayer == false)
                {
                    _currentCompanionState = CompanionEmotionState.Sad;
                    stated = false;

                }

                else if (companion.ReturnToPlayer == true)
                {
                    _currentCompanionState = CompanionEmotionState.Happy;
                    stated = false;

                }

                else if (companionHP >= 200 && companion.ReturnToPlayer == false)
                {
                    _currentCompanionState = CompanionEmotionState.Neutral;
                    stated = false;

                }

                else if (playerHP < 50 && companionHP >= 300 && companion.ReturnToPlayer == false)
                {
                    _currentCompanionState = CompanionEmotionState.Angry;
                    stated = false;


                }
                
            }


            if (GUI.Button(new Rect(Screen.width-350, Screen.height - 50, 50,50 ),emoticons[0]))
            {
                _currentPlayerState = PlayerEmotionState.Neutral;
                if(rnd == 0)
                {
                    _currentCompanionState = CompanionEmotionState.Neutral;
                }
                else if(rnd == 1)
                {
                    _currentCompanionState = CompanionEmotionState.Happy;
                }
                else if (rnd == 2)
                {
                    _currentCompanionState = CompanionEmotionState.Neutral;
                }

            }
            if (GUI.Button(new Rect(Screen.width-300, Screen.height -50, 50, 50), emoticons[1]))
            {
                _currentPlayerState = PlayerEmotionState.Angry;
                if (rnd == 0)
                {
                    _currentCompanionState = CompanionEmotionState.Sad;
                }
                else if (rnd == 1)
                {
                    _currentCompanionState = CompanionEmotionState.Angry;
                }
                else if (rnd == 2)
                {
                    _currentCompanionState = CompanionEmotionState.Angry;
                }

               
            }
            if (GUI.Button(new Rect(Screen.width - 250, Screen.height - 50, 50, 50), emoticons[2]))
            {
                _currentPlayerState = PlayerEmotionState.Happy;
                if (rnd == 0)
                {
                    _currentCompanionState = CompanionEmotionState.Happy;
                }
                else if (rnd == 1)
                {
                    _currentCompanionState = CompanionEmotionState.Angry;
                }
                else if (rnd == 2)
                {
                    _currentCompanionState = CompanionEmotionState.Neutral;
                }
               
            }
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 50, 50, 50), emoticons[3]))
            {
                _currentPlayerState = PlayerEmotionState.Sad;
                if (rnd == 0)
                {
                    _currentCompanionState = CompanionEmotionState.Sad;
                }
                else if (rnd == 1)
                {
                    _currentCompanionState = CompanionEmotionState.Angry;
                }
                else if (rnd == 2)
                {
                    _currentCompanionState = CompanionEmotionState.Neutral;
                }
              
            }

            //GUI.Label(new Rect(playerPos.x, playerPos.y, 100, 50), "Player State: ");
            switch (_currentPlayerState)
            {
                case PlayerEmotionState.Neutral:
                    selectEmoji = 0;
                    GUI.Label(new Rect(playerPos.x + offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(playerInterval,false));
                    break;
                case PlayerEmotionState.Angry:
                    selectEmoji = 1;
                    GUI.Label(new Rect(playerPos.x + offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(playerInterval,false));
                    break;
                case PlayerEmotionState.Happy:
                    selectEmoji = 2;
                    GUI.Label(new Rect(playerPos.x + offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(playerInterval,false));
                    break;
                case PlayerEmotionState.Sad:
                    selectEmoji = 3;
                    GUI.Label(new Rect(playerPos.x+offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(playerInterval,false));

                    break;
                case PlayerEmotionState.None:
                    StopAllCoroutines();

                    break;
            }

            switch (_currentCompanionState)
            {
                case CompanionEmotionState.Neutral:
                    selectEmoji = 0;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(compaionInterval, true));
                    break;
                case CompanionEmotionState.Angry:
                    selectEmoji = 1;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(compaionInterval, true));
                    break;
                case CompanionEmotionState.Happy:
                    selectEmoji = 2;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(compaionInterval, true));
                    break;
                case CompanionEmotionState.Sad:
                    selectEmoji = 3;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    StartCoroutine(emoticonInterval(compaionInterval, true));
                    break;
                case CompanionEmotionState.None:
                    StopAllCoroutines();

                    break;
            }

        }

        IEnumerator emoticonInterval(float time, bool pORc)
        {
            if(pORc == true)
            {
                yield return new WaitForSeconds(time);
                if (_currentCompanionState != CompanionEmotionState.None)
                {
                    _currentCompanionState = CompanionEmotionState.None;
                    stated = true;

                }
            }
            if(pORc == false)
            {
                yield return new WaitForSeconds(time);
                if (_currentPlayerState != PlayerEmotionState.None)
                {
                    _currentPlayerState = PlayerEmotionState.None;

                }
            }

        }



    }

}
