namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class MasterScript : MonoBehaviour
    {
        //public bool waveStarted = false;
        public int pickedUpBoosters;
        private int pickedUpHealth;
        private int pickedUpDMG;
        private int pickedUpMove;
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
        public GameObject objlabel;
        public GameObject waveButton;
        public GameObject returnButton;
        public float dmgBoostTime;
        public float moveBoostTime;
        public bool isDMGBoost;
        public bool isMoveBoost;
        public bool isHealthBoost;


        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


        void FixedUpdate()
        {


            if(LevelCheck == 1 || LevelCheck == 4)
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

            if(Application.loadedLevelName == "Level02")
            {
                LevelCheck = 4;
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

            if(isMoveBoost)
            {
                moveBoostTime = 10;
                pickedUpBoosters++;
                pickedUpMove++;
                isMoveBoost = false;
               
            }

            if(isDMGBoost)
            {
                dmgBoostTime = 10;
                pickedUpBoosters++;
                pickedUpDMG++;
                isDMGBoost = false;
            }

            if(isHealthBoost)
            {
                pickedUpBoosters++;
                pickedUpHealth++;
                isHealthBoost = false;
            }

            if(dmgBoostTime >= 0)
            {
                dmgBoostTime -= Time.deltaTime;
            }

            if(moveBoostTime >= 0)
            {
                moveBoostTime -= Time.deltaTime;
            }

        }

        void OnGUI()
        {
            GUIStyle myFont = new GUIStyle();
            myFont.font = b;
            myFont.normal.textColor = Color.white;
            myFont.fontSize = 24;

            if(LevelCheck == 1 || LevelCheck == 4)
            {

                wM = GameObject.FindGameObjectWithTag("waveMaster").GetComponent<WaveMaster>();
                GUI.Label(new Rect(Screen.width / 20, Screen.height / 20, Screen.width / 8, Screen.height / 8), "Wave: " + wM.waveLevel, myFont);
                GUI.Label(new Rect(Screen.width / 5, Screen.height / 20, Screen.width / 8, Screen.height / 8), "H : " + hour, myFont);
                GUI.Label(new Rect(Screen.width / 3.75f, Screen.height / 20, Screen.width / 8, Screen.height / 8), "M : " + min, myFont);
                GUI.Label(new Rect(Screen.width / 3, Screen.height / 20, Screen.width / 8, Screen.height / 8), "S : " + Mathf.RoundToInt(sec), myFont);
                GUI.Label(new Rect(Screen.width / 20, Screen.height / 10, Screen.width/8, Screen.height/8), "Player Health: " + Mathf.RoundToInt(playerHP) + " \n \nCompanion Health: " + Mathf.RoundToInt(companionHP), myFont);
                if(dmgBoostTime >= 0)
                {
                    GUI.Label(new Rect(Screen.width -400, Screen.height / 8, Screen.width / 8, Screen.height / 8), "DMG Boost Time: " + Mathf.RoundToInt(dmgBoostTime), myFont);
                    
                }
                if(moveBoostTime >= 0)
                {
                    GUI.Label(new Rect(Screen.width -400, Screen.height / 6, Screen.width / 8, Screen.height / 8), "Move Boost Time: " + Mathf.RoundToInt(moveBoostTime), myFont);
                    

                }
                

                if (Input.GetKey(KeyCode.Mouse2))
                {
                    
                    Rect guiRect = new Rect(Screen.width/2-200, Screen.height/2-50, 200, 200);

                    GUILayout.BeginArea(guiRect, "CompanionAI Commands", myFont);
                    GUILayout.Space(30);

                    if (GUILayout.Button("Aggressive"))
                    {
                        if(LevelCheck == 1)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().t = null;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Aggressive = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Defensive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Collector = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Patrol = false;
                        }
                        else if(LevelCheck == 4)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().t = null;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Aggressive = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Defensive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Collector = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Patrol = false;
                        }



                    }

                    if (GUILayout.Button("Defensive"))
                    {
                        if(LevelCheck == 1)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().ReturnToPlayer = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Defensive = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Aggressive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Collector = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Patrol = false;
                        }
                        else if(LevelCheck == 4)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Happy;
                            //GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().ReturnToPlayer = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Defensive = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Aggressive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Collector = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Patrol = false;
                        }

                    }

                    if(GUILayout.Button("Collector"))
                    {
                        if(LevelCheck == 1)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Neutral;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().ReturnToPlayer = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Defensive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Aggressive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Collector = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Patrol = false;
                        }
                        else if (LevelCheck == 4)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Neutral;
                            //GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().ReturnToPlayer = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Defensive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Aggressive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Collector = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Patrol = false;
                        }



                    }

                    if (GUILayout.Button("Patrol"))
                    {
                        if(LevelCheck == 1)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().ReturnToPlayer = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Defensive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Aggressive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Collector = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathCompanionUnit>().Patrol = true;
                        }
                        else if (LevelCheck == 4)
                        {
                            GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>()._currentCompanionState = EmoticonCommunicationSystem.CompanionEmotionState.Angry;
                            //GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().resetPath = true;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().ReturnToPlayer = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Defensive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Aggressive = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Collector = false;
                            GameObject.FindGameObjectWithTag("companion").GetComponent<PathUtilityCompanionUnit>().Patrol = true;
                        }



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
                    objlabel.SetActive(true);
                }
                else if(!tutorial)
                {
                    //myFont.fontSize = 128;
                    //if(GUI.Button(new Rect(Screen.width/2 - 200, Screen.height/2-100, 400, 200), "Survive!", myFont))
                    //{
                    //    Application.LoadLevel("Level01");


                    //}
                    tutlabel.SetActive(false);
                    objlabel.SetActive(false);
                }
            }

            if(LevelCheck == 2)
            {
                //myFont.fontSize = 64;
                //GUI.Label(new Rect(Screen.width / 2.5f, Screen.height / 2, 100, 30), "Game Over", myFont);
                myFont.fontSize = 18;
                GUI.Label(new Rect(25, 25, 100, 200), "Total Time: \n Hours: " + hour + " Minutes: " + min + " Seconds: " + sec + " \n\nAmount of Booster collected : " + pickedUpBoosters + " \nHealth Packs : " + pickedUpHealth + " \nDamage Boosters : " + pickedUpDMG + " \nMoveSpeed Boosters : " + pickedUpMove, myFont);
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
                GUI.Label(new Rect(25, 25, 100, 200), "Total Time: \n Hours: " + hour + " Minutes: " + min + " Seconds: " + sec+" \n\nAmount of Booster collected : "+pickedUpBoosters+" \nHealth Packs : "+pickedUpHealth+" \nDamage Boosters : "+pickedUpDMG+" \nMoveSpeed Boosters : "+pickedUpMove, myFont);
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
            else if(scene == 6)
            {
                Application.LoadLevel("Level02");
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
