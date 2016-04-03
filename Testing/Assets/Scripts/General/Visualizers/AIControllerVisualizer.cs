namespace AI.Master
{
    using UnityEngine;

    [RequireComponent(typeof(AIController))]
    public sealed class AIControllerVisualizer : MonoBehaviour
    {
        private const float width = 150f;
        private const float height = 100f;
        private const float padding = 5f;

        [SerializeField]
        private bool _leftAlign;

        private AIController _aiController;

        private void OnEnable()
        {
            _aiController = this.GetComponent<AIController>();
        }

        private void OnGUI()
        {
            if (!this.enabled || !Application.isPlaying)
            {
                return;
            }

            var mainBase = _aiController.mainBase;
            var text = string.Concat(
                "Resources: ", mainBase.currentResources.ToString(),
                "\nMainBase HP: ", mainBase.currentHealth.ToString("F0"), " / ", mainBase.maxHealth.ToString("F0"),
                "\nTotal Units: ", mainBase.units.Count.ToString(),
                "\nMiners: ", mainBase.minerCount,
                "\nSoldiers: ", mainBase.soldierCount,
                "\nDemolishers: ", mainBase.demolisherCount);

            Rect rect;
            if (_leftAlign)
            {
                rect = new Rect(padding, padding, width, height);
            }
            else
            {
                rect = new Rect(Screen.width - width - (padding * 2f), padding, width, height);
            }

            GUI.color = _aiController.color;
            GUI.Box(rect, text);
        }
    }
}