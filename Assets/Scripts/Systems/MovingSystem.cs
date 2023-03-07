using RacingGame.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RacingGame.Scripts.Systems
{
    [BurstCompile]
    public partial struct MovingSystem : ISystem
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

            new MoveJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MoveJob : IJobEntity 
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(TransformAspect transformAspect, Speed speed, Input input) 
        {
            transformAspect.RotateWorld(quaternion.RotateY(DeltaTime * input.X * speed.Value));

            transformAspect.TranslateWorld(transformAspect.Forward * DeltaTime * input.Y * speed.Value);
        }
    }
}
