using RacingGame.Scripts.Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace RacingGame.Scripts.Aspects
{
    public readonly partial struct WheelAspect : IAspect 
    {
        public readonly TransformAspect _transformAspect;
        public readonly RefRO<Wheel> Wheel;
        public readonly RefRW<Suspension> Suspension;
    }
}
