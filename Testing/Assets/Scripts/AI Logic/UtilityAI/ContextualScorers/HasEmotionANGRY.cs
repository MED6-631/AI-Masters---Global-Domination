namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using Apex.AI;
    using System;

    public class HasEmotionANGRY : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            var c = (AIContext)context;
            var gC = GameObject.FindGameObjectWithTag("GameController").GetComponent<EmoticonCommunicationSystem>();

            if(gC._currentCompanionState == EmoticonCommunicationSystem.CompanionEmotionState.Angry)
            {
                return this.score;
            }

            return 0f;

        }
    }

}
