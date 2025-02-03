using Unity.Entities;
using Unity.Mathematics;

namespace ThirdCharacter
{
    public struct PlayerInputComponent : IComponentData
    {
        public float2 MoveInput;
    }

    public struct PlayerMovementComponent : IComponentData
    {
        public float MoveSpeed;
    }

    public struct PlayerAnimationComponent : IComponentData
    {
        public bool IsMoving;
        public Entity AnimatorEntity;
        public int HashMove;
    }
}

