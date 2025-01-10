using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("조작키 설정")]
        [SerializeField] private KeyCode forwardKey = KeyCode.W;
        [SerializeField] private KeyCode backwardKey = KeyCode.S;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;

        public Vector3 InputVector => _inputVector;
        public bool IsJumping { get => _isJumping; set => _isJumping = value; }

        public Vector3 _inputVector;
        public bool _isJumping;
        private float _xInput;
        private float _yInput;
        private float _zInput;


        private void Update()
        {
            HandleInput();
        }

        public void HandleInput()
        {
            _xInput = 0;
            _yInput = 0;
            _zInput = 0;

            if (Input.GetKey(forwardKey))
            {
                _zInput = 1;
            }

            if (Input.GetKey(backwardKey))
            {
                _zInput = -1;
            }

            if (Input.GetKey(leftKey))
            {
                _xInput = -1;
            }

            if (Input.GetKey(rightKey))
            {
                _xInput = 1;
            }

            _inputVector = new Vector3(_xInput, _yInput, _zInput);
            _isJumping = Input.GetKey(jumpKey);
        }
    }
}