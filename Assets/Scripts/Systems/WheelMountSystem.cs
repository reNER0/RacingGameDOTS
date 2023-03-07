using RacingGame.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using UnityEditor.Search;
using Unity.Mathematics;

namespace RacingGame.Scripts.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [BurstCompile]
    public partial struct WheelMountSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            // TODO : Move this to job
            foreach (var wheelMount in SystemAPI.Query<WheelAspect>())
            {
                var entity = wheelMount.Suspension.ValueRO.ConnectedEntity;

                var physicsVelocity = SystemAPI.GetComponent<PhysicsVelocity>(entity);
                var physicsMass = SystemAPI.GetComponent<PhysicsMass>(entity);

                var raycastInput = new RaycastInput()
                {
                    Start = wheelMount._transformAspect.WorldPosition,
                    End = wheelMount._transformAspect.WorldPosition + wheelMount._transformAspect.Down * wheelMount.Suspension.ValueRO.Lenght,
                    Filter = CollisionFilter.Default,
                };

                if (physicsWorldSingleton.CastRay(raycastInput, out var hit))
                {
                    var lenght = math.distance(wheelMount._transformAspect.WorldPosition, hit.Position);
                    var springForce = (1 - lenght / wheelMount.Suspension.ValueRO.Lenght) * wheelMount.Suspension.ValueRO.Spring;
                    var dampingForce = (wheelMount.Suspension.ValueRO.LastLenght - lenght) / deltaTime * wheelMount.Suspension.ValueRO.Damping;
                    var force = (springForce + dampingForce) * wheelMount._transformAspect.Up * deltaTime;

                    // TODO : replace to ApplyImpulse(AtPoint) (can`t fix yet because of Unity bug 07.03.2023)
                    physicsVelocity.ApplyLinearImpulse(physicsMass, force);

                    wheelMount.Suspension.ValueRW.LastLenght = lenght;
                }
                SystemAPI.SetComponent(entity, physicsVelocity);
            }
        }
    }
}
