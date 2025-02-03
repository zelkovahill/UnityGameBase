using ThirdCharacter;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ThirdCharacter
{
    [UpdateAfter(typeof(PlayerInputSystem))]
    partial struct PlayerMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, input, movement) in
             SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerInputComponent>, RefRW<PlayerMovementComponent>>())
            {
                var moveSpeed = movement.ValueRO.MoveSpeed;
                var move = new float3(input.ValueRO.MoveInput.x, 0, input.ValueRO.MoveInput.y);

                if (math.length(move) > 0)
                {
                    move = math.normalize(move);

                    quaternion rotation = quaternion.LookRotationSafe(move, math.up());
                    transform.ValueRW.Rotation = rotation;
                }

                transform.ValueRW.Position += move * moveSpeed * Time.deltaTime;
            }
        }

    }
}