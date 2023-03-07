using Unity.Entities;
using Unity.Physics;

namespace RacingGame.Scripts.Components
{
    public struct Suspension : IComponentData
    {
        public float Damping;
        public float Spring;
        public float Lenght;
        public float LastLenght;
        public Entity ConnectedEntity;
    }
}
