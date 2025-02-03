using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


namespace ThirdCharacter
{
    public class PlayerAutoring : MonoBehaviour
    {
        public float2 MoveInput;
        public float MoveSpeed;
        public bool IsMoving;
        public Animator Animator;
    }

    public class PlayerAutoringBaker : Baker<PlayerAutoring>
    {
        public override void Bake(PlayerAutoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new PlayerInputComponent
            {
                MoveInput = authoring.MoveInput
            });

            AddComponent(entity, new PlayerMovementComponent
            {
                MoveSpeed = authoring.MoveSpeed
            });


            AddComponent(entity, new PlayerAnimationComponent
            {
                IsMoving = authoring.IsMoving,
                AnimatorEntity = GetEntity(authoring.Animator.gameObject, TransformUsageFlags.Dynamic),
                HashMove = Animator.StringToHash("IsMoving")
            });
        }
    }
}
