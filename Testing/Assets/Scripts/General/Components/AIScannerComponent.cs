namespace AI.Master
{
    using UnityEngine;
    using System.Collections;
    using System;

    [RequireComponent(typeof(UnitBase))]

    public sealed class AIScannerComponent : AIComponentBase<UnitBase>
    {
        protected override void ExecuteAI()
        {
            if(_entity != null && !_entity.isDead)
            {
                _entity.AddOrUpdateObservations(Physics.OverlapSphere(_entity.transform.position, _entity.viewRadius, Layers.all));

                var obs = _entity.obs;
                var count = obs.Count;

                for (int i = count -1; i >= 0; i--)
                {
                    var observations = obs[i];
                    if(observations == null || !observations.gameObject.activeSelf)
                    {
                        obs.RemoveAt(i);
                        continue;
                    }

                    var unit = observations.GetComponent<UnitBase>();
                    if(unit != null && unit.isDead)
                    {
                        obs.RemoveAt(i);
                        continue;
                    }

                    //var resource = observations.GetComponent<ResourceComponent>();
                    //if(resource != null && resource.currentResources <= 0)
                    //{
                    //    obs.RemoveAt(i);
                    //    continue;
                    //}

                    var mainBase = observations.GetComponent<MainBaseStructure>();
                    if(mainBase != null && mainBase.isDead)
                    {
                        obs.RemoveAt(i);
                    }
                }

                obs.Sort(new GameObjectDistanceSortComparer(this.transform.position));

            }
        }
    }

}
