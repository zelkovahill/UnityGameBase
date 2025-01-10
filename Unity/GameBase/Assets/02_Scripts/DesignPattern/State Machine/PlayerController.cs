using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        private PlayerStateMachine _stateMachine;

        [Header("이동 관련 세팅")]
        [Tooltip("이동 속도")][SerializeField] private float moveSpeed = 5f;
        [Tooltip("가속도")][SerializeField] private float acceleration = 10f;
        [Tooltip("점프 높이")][SerializeField] private float jumpHeight = 1.25f;

        [Tooltip("중력")][SerializeField] private float gravity = -15f;
        [Tooltip("점프 쿨타임")][SerializeField] private float jumpTimeout = 0.1f;

        [Tooltip("지면에 닿아있는지 여부")][SerializeField] private bool isGrounded = true;
        [Tooltip("지면 체크에 사용되는 반지름")][SerializeField] private float groundedRadius = 0.5f;
        [Tooltip("지면 체크를 위한 위치 오프셋")][SerializeField] private float groundedOffset = 0.15f;
        [Tooltip("지면 판정을 위한 레이어 마스크")][SerializeField] private LayerMask groundLayers;

        public CharacterController CharacterController => _characterController;
        public bool IsGrounded => isGrounded;
        public PlayerStateMachine PlayerStateMachine => _stateMachine;

        private CharacterController _characterController;
        private float _targetSpeed;
        private float _verticalVelocity;
        private float _jumpColldown;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();

            _stateMachine = new PlayerStateMachine(this);
        }

        private void Start()
        {
            _stateMachine.Initialize(_stateMachine.idleState);
        }

        private void Update()
        {
            _stateMachine.Execute();
        }

        private void LateUpdate()
        {
            CalculateVertical();
            Move();
        }

        private void Move()
        {
            Vector3 inputVector = _playerInput.InputVector;

            if (inputVector == Vector3.zero)
            {
                _targetSpeed = 0;
            }

            float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z).magnitude;
            float tolerance = 0.1f;

            if (currentHorizontalSpeed < _targetSpeed - tolerance || currentHorizontalSpeed > _targetSpeed + tolerance)
            {
                _targetSpeed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, Time.deltaTime * acceleration);
                _targetSpeed = Mathf.Round(_targetSpeed * 1000f) / 1000f;
            }
            else
            {
                _targetSpeed = moveSpeed;
            }

            _characterController.Move((inputVector.normalized * _targetSpeed * Time.deltaTime) + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
        }

        private void CalculateVertical()
        {
            if (isGrounded)
            {
                if (_verticalVelocity < 0f)
                {
                    _verticalVelocity = -2f;
                }

                if (_playerInput.IsJumping && _jumpColldown <= 0)
                {
                    _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (_jumpColldown > 0)
                {
                    _jumpColldown -= Time.deltaTime;
                }
            }
            else
            {
                _jumpColldown = jumpTimeout;
                _playerInput.IsJumping = false;
            }

            _verticalVelocity += gravity * Time.deltaTime;

            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, 0.5f, groundLayers, QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {
            Color transperentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transperentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded)
            {
                Gizmos.color = transperentGreen;
            }
            else
            {
                Gizmos.color = transperentRed;
            }

            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z), 0.5f);
        }

    }
}