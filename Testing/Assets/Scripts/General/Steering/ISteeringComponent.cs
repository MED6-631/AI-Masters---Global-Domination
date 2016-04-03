namespace AI.Master
{
    using UnityEngine;

    //Interface for all steeringComponents
    public interface ISteeringComponent
    {

        int priority { get; }


        Vector3? GetSteering(SteeringInput input);
    }
}