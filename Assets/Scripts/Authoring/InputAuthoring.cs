using RacingGame.Scripts.Components;
using Unity.Entities;
using UnityEngine;
using Input = RacingGame.Scripts.Components.Input;

namespace RacingGame.Scripts.Authoring
{
    public class InputAuthoring : MonoBehaviour
    {
    }


    public class InputBaker : Baker<InputAuthoring>
    {
        public override void Bake(InputAuthoring authoring)
        {
            AddComponent(new Input());
        }
    }
}
