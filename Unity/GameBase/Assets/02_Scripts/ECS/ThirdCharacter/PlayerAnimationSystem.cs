using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ThirdCharacter
{
    partial struct PlayerAnimationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityManager = state.EntityManager;

            foreach (var (animation, input, entity) in SystemAPI.Query<RefRW<PlayerAnimationComponent>, RefRO<PlayerInputComponent>>().WithEntityAccess())
            {
                bool isMoving = math.length(input.ValueRO.MoveInput) > 0;
                animation.ValueRW.IsMoving = isMoving;


                if (entityManager.HasComponent<Animator>(animation.ValueRW.AnimatorEntity))
                {
                    var animator = entityManager.GetComponentObject<Animator>(animation.ValueRW.AnimatorEntity);
                    
                    animator.SetBool(animation.ValueRW.HashMove, isMoving);
                }
            }
        }
    }
}