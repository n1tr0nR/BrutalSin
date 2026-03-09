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

        public Vector2 KeyboardInput => keyboardInput;
        public Vector2 MouseInput => mouseInput;

        private InputAction moveAction;
        private InputAction lookAction;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();

            moveAction = input.actions["Move"];
            moveAction.performed += MoveInput;
            moveAction.canceled += MoveInput;

            lookAction = input.actions["Look"];
            lookAction.performed += LookInput;
            lookAction.canceled += LookInput;
        }

        private void MoveInput(InputAction.CallbackContext context)
        {
            keyboardInput = context.ReadValue<Vector2>();
        }
        private void LookInput(InputAction.CallbackContext context)
        {
            mouseInput = context.ReadValue<Vector2>();
        }

        private void OnDestroy()
        {
            moveAction.performed -= MoveInput;
            moveAction.canceled -= MoveInput;
            
            lookAction.performed -= LookInput;
            lookAction.canceled -= LookInput;
        }
    }
}