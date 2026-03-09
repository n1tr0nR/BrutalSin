using UnityEngine;

namespace Player.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroInput))]
    public class HeroMotor : MonoBehaviour //Simple ass controller, lowk bum but ill make it better in the future. TODO: LOCK TF IN!!!
    {
        private CharacterController controller;
        private HeroInput input;
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float gravity = -9.81f;

        [Header("Mouse Look")]
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private Transform cameraPivot;
        
        private float verticalVelocity;
        private float cameraPitch;
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<HeroInput>();
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
        }

        private void HandleMovement()
        {
            var moveInput = input.KeyboardInput;
            var move = transform.right * moveInput.x + transform.forward * moveInput.y;

            if (controller.isGrounded && verticalVelocity < 0) verticalVelocity = -2f;

            verticalVelocity += gravity * Time.deltaTime;
            var velocity = move * moveSpeed;
            velocity.y = verticalVelocity;

            controller.Move(velocity * Time.deltaTime);
        }

        private void HandleLook()
        {
            var mouse = input.MouseInput * mouseSensitivity;

            cameraPitch -= mouse.y;
            cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);
            cameraPivot.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);

            transform.Rotate(Vector3.up * mouse.x);
        }
    }
}