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
        public Texture[] emoticons;
        private int emoticonInterval = 0;
        private int selectEmoji = 0;

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
        }
        
        void FixedUpate()
        {
            playerPos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
            companionPos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("companion").transform.position);



        }

        void OnGUI()
        {

            if(GUI.Button(new Rect(Screen.width-350, Screen.height - 50, 50,50 ),emoticons[0]))
            {
                _currentPlayerState = PlayerEmotionState.Neutral;
            }
            if (GUI.Button(new Rect(Screen.width-300, Screen.height -50, 50, 50), emoticons[1]))
            {
                _currentPlayerState = PlayerEmotionState.Angry;
            }
            if (GUI.Button(new Rect(Screen.width - 250, Screen.height - 50, 50, 50), emoticons[2]))
            {
                _currentPlayerState = PlayerEmotionState.Happy;
            }
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 50, 50, 50), emoticons[3]))
            {
                _currentPlayerState = PlayerEmotionState.Sad;
            }

            //GUI.Label(new Rect(playerPos.x, playerPos.y, 100, 50), "Player State: ");
            switch (_currentPlayerState)
            {
                case PlayerEmotionState.Neutral:
                    selectEmoji = 0;
                    GUI.Label(new Rect(playerPos.x + offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    break;
                case PlayerEmotionState.Angry:
                    selectEmoji = 1;
                    GUI.Label(new Rect(playerPos.x + offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    break;
                case PlayerEmotionState.Happy:
                    selectEmoji = 2;
                    GUI.Label(new Rect(playerPos.x + offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);
                    break;
                case PlayerEmotionState.Sad:
                    selectEmoji = 3;
                    GUI.Label(new Rect(playerPos.x+offsetX, playerPos.y + offsetY, 100, 50), emoticons[selectEmoji]);

                    break;
                case PlayerEmotionState.None:


                    break;
            }

            switch (_currentCompanionState)
            {
                case CompanionEmotionState.Neutral:
                    selectEmoji = 0;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    break;
                case CompanionEmotionState.Angry:
                    selectEmoji = 1;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    break;
                case CompanionEmotionState.Happy:
                    selectEmoji = 2;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);
                    break;
                case CompanionEmotionState.Sad:
                    selectEmoji = 3;
                    GUI.Label(new Rect(companionPos.x + offsetX, companionPos.y + offsetY+55, 100, 50), emoticons[selectEmoji]);

                    break;
                case CompanionEmotionState.None:


                    break;
            }

        }



    }

}
