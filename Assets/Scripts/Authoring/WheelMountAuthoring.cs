using RacingGame.Scripts.Aspects;
using RacingGame.Scripts.Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Authoring;
using Unity.Transforms;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

namespace RacingGame.Scripts.Authoring
{
    public class WheelMountAuthoring : MonoBehaviour
    {
        public float WheelRadius;
        public float Spring;
        public float Damping;
        public float Lenght;
        public PhysicsBodyAuthoring ConnectedBody;
    }

    public class WheelMountBaker : Baker<WheelMountAuthoring>
    {
        public override void Bake(WheelMountAuthoring authoring)
        {
            AddComponent(new Wheel
            {
                Radius = authoring.WheelRadius
            });
            AddComponent(new Suspension
            {
                Damping = authoring.Damping,
                Spring = authoring.Spring,
                Lenght = authoring.Lenght,
                ConnectedEntity = GetEntity(authoring.ConnectedBody)
            });
        }
    }
}
