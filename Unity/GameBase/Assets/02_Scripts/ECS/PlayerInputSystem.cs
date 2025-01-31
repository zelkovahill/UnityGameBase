using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct PlayerInputSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (input, transform) in SystemAPI.Query<RefRW<PlayerInputComponent>, RefRW<LocalTransform>>())
        {

            float horizontal = Input.GetKey(input.ValueRW.RightKey) ? 1 : Input.GetKey(input.ValueRW.LeftKey) ? -1 : 0;
            float vertical = Input.GetKey(input.ValueRW.UpKey) ? 1 : Input.GetKey(input.ValueRW.DownKey) ? -1 : 0;

            input.ValueRW.MoveInput = new float2(horizontal, vertical);

            input.ValueRW.CurrentSpeed = 5.0f;

            float3 moveDirection = new float3(input.ValueRW.MoveInput.x, 0, input.ValueRW.MoveInput.y) * input.ValueRW.CurrentSpeed * SystemAPI.Time.DeltaTime;
            transform.ValueRW.Position += moveDirection;
        }
    }
}