using RacingGame.Scripts.Components;
using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.InputSystem;

namespace RacingGame.Scripts.Systems
{
    [BurstCompile]
    public partial class InputSystem : SystemBase, UserControls.IPlayerActionMapActions
    {
        private UserControls _userControls;
        private UnityEngine.Vector2 _input;

        [BurstCompile]
        protected override void OnCreate()
        {
            _userControls = new UserControls();
            _userControls.Enable();
            _userControls.PlayerActionMap.SetCallbacks(this);
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            foreach(RefRW<Input> input in SystemAPI.Query<RefRW<Input>>()) 
            {
                input.ValueRW.X = _input.x;
                input.ValueRW.Y = _input.y;
            }
        }

        [BurstCompile]
        protected override void OnDestroy()
        {
            _userControls.Disable();
        }

        [BurstCompile]
        public void OnMoveAction(InputAction.CallbackContext context)
        {
            _input = context.ReadValue<UnityEngine.Vector2>();
        }
    }
}
