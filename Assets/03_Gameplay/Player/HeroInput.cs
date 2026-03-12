using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Gameplay
{
    [RequireComponent(typeof(PlayerInput))]
    public class HeroInput : MonoBehaviour
    {
        private PlayerInput input;

        private Vector2 keyboardInput;
        private Vector2 mouseInput;
        private bool jumpPressed;

        public Vector2 KeyboardInput => keyboardInput;
        public Vector2 MouseInput => mouseInput;
        public bool JumpPressed => jumpPressed;

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();

            moveAction = input.actions["Move"];
            moveAction.performed += MoveInput;
            moveAction.canceled += MoveInput;

            lookAction = input.actions["Look"];
            lookAction.performed += LookInput;
            lookAction.canceled += LookInput;

            jumpAction = input.actions["Jump"];
            jumpAction.performed += JumpInput;
            jumpAction.canceled += JumpInput;
        }

        private void MoveInput(InputAction.CallbackContext context)
        {
            keyboardInput = context.ReadValue<Vector2>();
        }

        private void LookInput(InputAction.CallbackContext context)
        {
            mouseInput = context.ReadValue<Vector2>();
        }

        private void JumpInput(InputAction.CallbackContext context)
        {
            jumpPressed = context.ReadValueAsButton();
        }

        private void OnDestroy()
        {
            moveAction.performed -= MoveInput;
            moveAction.canceled -= MoveInput;

            lookAction.performed -= LookInput;
            lookAction.canceled -= LookInput;

            jumpAction.performed -= JumpInput;
            jumpAction.canceled -= JumpInput;
        }
    }
}