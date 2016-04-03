namespace AI.Master
{
    using UnityEngine;

    public class AIController : MonoBehaviour
    {

        [SerializeField]
        private GameObject _baseGO;

        [SerializeField]
        private Color _color;

        private MainBaseStructure _mainBase;

        public MainBaseStructure mainBase 
        {
            get {return _mainBase; }    
             
        }

        public Color color
        {
            get { return _color; }
        }

        private void Awake()
        {
            var renderers = _baseGO.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                if(renderers[i].GetComponent<ParticleSystem>() == null)
                {
                    renderers[i].material.color = _color;
                }
            }

            _mainBase = _baseGO.GetComponent<MainBaseStructure>();
            _mainBase.controller = this;
        }

    }
}

