using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ThirdCharacter
{
    partial struct PlayerInputSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (input, entity) in SystemAPI.Query<RefRW<PlayerInputComponent>>().WithEntityAccess())
            {
                input.ValueRW.MoveInput = new float2(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical")
                );
            }
        }
    }
}