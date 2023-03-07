using RacingGame.Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace RacingGame.Scripts.Authoring
{
    public class SpeedAuthoring : MonoBehaviour 
    {
        public float Value;
    }

    public class SpeedBaker : Baker<SpeedAuthoring>
    {
        public override void Bake(SpeedAuthoring authoring)
        {
            AddComponent(new Speed
            {
                Value = authoring.Value
            });
        }
    }
}
