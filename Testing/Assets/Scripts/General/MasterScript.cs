namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class MasterScript : MonoBehaviour
    {
        //public bool waveStarted = false;
        public int pickedUpBoosters;
        public int timer;
        public Font b;
        public float sec;
        public float min;
        public float hour;
        public bool isPlayerDead = false;
        public bool isCompanionDead = false;
        public bool tutorial;
        public int LevelCheck;
        public bool pressed = false;
        //public int currentWave;
        public bool startWave = false;
        public float playerHP;
        public float companionHP;
        private WaveMaster wM;
        private Vector3 pC;
        public GameObject tutlabel;
        public GameObject waveButton;
        public GameObject returnButton;


        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


        void FixedUpdate()
        {


            if(LevelCheck == 1)
            {
                sec += Time.deltaTime;
                if (sec > 59)
                {
                    min += 1;
                    sec = 0;
                }

                if (min > 59)
                {
                    hour += 1;
                    min = 0;
                }
            }



            if(isPlayerDead && LevelCheck != 2)
            {
                Application.LoadLevel("GameOver");
            }

            if (isCompanionDead && LevelCheck != 2)
            {
                Application.LoadLevel("GameOver");
            }

            if(Application.loadedLevelName == "GameOver")
            {
                LevelCheck = 2;
            }

            if(Application.loadedLevelName == "Level01")
            {
                LevelCheck = 1;
            }

            if(Application.loadedLevelName == "Victory")
            {
                LevelCheck = 3;
            }

            if(Application.loadedLevelName == "Menu")
            {
                LevelCheck = 0;
            }

            if(wM.waveLevel >= 5 && LevelCheck != 3)
            {
                Application.LoadLevel("Victory");
            }
            
            if(Input.GetKeyDown(KeyCode.V) && LevelCheck != 3)
            {
                Application.LoadLevel("Victory");
            }



        }

        void OnGUI()
        {
            GUIStyle myFont = new GUIStyle();
            myFont.font = b;
            myFont.normal.textColor = Color.white;
            myFont.fontSize = 24;

            if(LevelCheck == 1)
            {

                wM = GameObject.FindGameObjectWithTag("waveMaster").GetComponent<WaveMaster>();
                GUI.Label(new Rect(Screen.width / 20, Screen.height / 20, Screen.width / 8, Screen.height / 8), "Wave: " + wM.waveLevel, myFont);
                GUI.Label(new Rect(Screen.width / 5, Screen.height / 20, Screen.width / 8, Screen.height / 8), "H : " + hour, myFont);
                GUI.Label(new Rect(Screen.width / 3.75f, Screen.height / 20, Screen.width / 8, Screen.height / 8), "M : " + min, myFont);
                GUI.Label(new Rect(Screen.width / 3, Screen.height / 20, Screen.width / 8, Screen.height / 8), "S : " + Mathf.RoundToInt(sec), myFont);
                GUI.Label(new Rect(Screen.width / 20, Screen.height / 10, Screen.width/8, Screen.height/8), "Player Health: " + Mathf.RoundToInt(playerHP) + " \n \nCompanion Health: " + Mathf.RoundToInt(companionHP), myFont);

                if(Input.GetKey(KeyCode.Mouse2))
                {
                    
                    Rect guiRect = new Rect(Screen.width/2-200, Screen.height/2-50, 100, 100);

                    GUILayout.BeginArea(guiRect, "CompanionAI Commands", myFont);
                    GUILayout.Space(30);

                    if (GUILayout.Button("Aggressive"))
                    {
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                        GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().t = null;
                        GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().RandomWander();
                    }

                    if (GUILayout.Button("Defensive"))
                    {
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                        GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().ReturnToPlayer = true;
                    }

                    GUILayout.EndArea();




                }

                    if (wM.waveLevel == 0)
                    {
                    waveButton.SetActive(true);
                        //myFont.fontSize = 64;
                        //if (GUI.Button(new Rect(Screen.width / 2-150, Screen.height / 2-50, Screen.width / 4, Screen.height / 4), "Start Wave", myFont))
                        //{
                        //startWave = true;

                        //}
                    } else
                {
                    waveButton.SetActive(false);
                }


            }

            if(LevelCheck == 0)
            {
                //if(GUI.Button( new Rect(25,25,100,30), "Tutorial", myFont))
                //{
                //    tutorial = !tutorial;
                //}
                if(tutorial)
                {
                    //GUI.Label(new Rect(Screen.width / 2 -200, Screen.height / 8, Screen.width / 20, Screen.height / 20), "Controls: \nW = Up, S = Down, A = Left, D = Right, Q = Recall Companion\nSpace = shoot, Right Mousebutton = Move Companion to location \nHold Middle Mousebutton to show AI Behavior Menu \nThe Emoticons can be used to communicate \nwith the Companion AI who knows may do something\n", myFont);
                    tutlabel.SetActive(true);
                }
                else if(!tutorial)
                {
                    //myFont.fontSize = 128;
                    //if(GUI.Button(new Rect(Screen.width/2 - 200, Screen.height/2-100, 400, 200), "Survive!", myFont))
                    //{
                    //    Application.LoadLevel("Level01");


                    //}
                    tutlabel.SetActive(false);
                }
            }

            if(LevelCheck == 2)
            {
                //myFont.fontSize = 64;
                //GUI.Label(new Rect(Screen.width / 2.5f, Screen.height / 2, 100, 30), "Game Over", myFont);
                myFont.fontSize = 18;
                GUI.Label(new Rect(25, 25, 100, 30), "Total Time: \n Hours: " + hour + " Minutes: " + min + " Seconds: " + sec, myFont);
                myFont.fontSize = 24;
                if(isPlayerDead)
                {
                    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 100, 30), "Player Died", myFont);
                }
                else if(isCompanionDead)
                {
                    GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100, 100, 30), "Companion Died", myFont);
                }
               
                //if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height / 1.25f, 100, 30), "Return to Main Menu", myFont))
                //{
                //    Application.LoadLevel("Menu");
                //    Destroy(this.gameObject);
                //}
                

            }

            if(LevelCheck == 3)
            {
                //myFont.fontSize = 64;
                //GUI.Label(new Rect(Screen.width / 2.5f, Screen.height / 2, 100, 30), "You Won!", myFont);
                myFont.fontSize = 18;
                GUI.Label(new Rect(25, 25, 100, 30), "Total Time: \n Hours: " + hour + " Minutes: " + min + " Seconds: " + sec, myFont);
                myFont.fontSize = 24;
                //if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height / 1.25f, 100, 30), "Return to Main Menu", myFont))
                //{
                //    Application.LoadLevel("Menu");
                //    Destroy(this.gameObject);
                //}
            }


        }

       public void LoadScene(int scene)
        {
            if(scene == 0)
            {
                Application.LoadLevel("Menu");
            }
            else if (scene == 1)
            {
                Application.LoadLevel("Level01");
            }
            else if (scene == 2)
            {
                Application.LoadLevel("GameOver");
            }
            else if (scene == 3)
            {
                Application.LoadLevel("Victory");
            }
            else if(scene == 4)
            {
                tutorial = !tutorial;
            }
            else if(scene == 5)
            {
                startWave = true;
            }
            else
            {
                print("Scene: " + scene + " does not exist please try again.");
            }
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }

}
