using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerInputComponent : IComponentData
{
    public float2 MoveInput;
    public float CurrentSpeed;

    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
}


public class PlayerInputAuthoring : MonoBehaviour
{
    [Header("입력 설정")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    class Baker : Baker<PlayerInputAuthoring>
    {
        public override void Bake(PlayerInputAuthoring src)
        {
            PlayerInputComponent playerInputComponent = new PlayerInputComponent
            {
                UpKey = src.upKey,
                DownKey = src.downKey,
                LeftKey = src.leftKey,
                RightKey = src.rightKey,
            };

            AddComponent(GetEntity(TransformUsageFlags.Dynamic), playerInputComponent);
        }
    }
}
