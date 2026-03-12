using UnityEngine;

namespace Player.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(HeroInput))]
    public class HeroMotor : MonoBehaviour //Simple ass controller, lowk bum but ill make it better in the future. TODO: LOCK TF IN!!!
    {
        private CharacterController controller;
        private HeroInput input;
        
        private Vector3 horizontalVelocity;
        private Vector3 horizontalVelocityRef;

        private float cameraRollVelocity;

        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float groundSmoothTime = 0.08f;
        [SerializeField] private float airSmoothTime = 0.25f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpForce = 5f;

        [Header("Camera Tilt")]
        [SerializeField] private float maxRoll = 6f;
        [SerializeField] private float rollSmoothTime = 0.15f;

        [Header("Mouse Look")]
        [SerializeField] private float mouseSensitivity = 0.25f;
        [SerializeField] private Transform cameraPivot;
        
        private float verticalVelocity;
        private float cameraPitch;
        private float cameraFallPitch;
        private float cameraRoll;
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<HeroInput>();
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            var fallTilt = verticalVelocity * 0.5f;
            cameraFallPitch = Mathf.Lerp(cameraFallPitch, fallTilt, 5f * Time.deltaTime);
            cameraFallPitch = Mathf.Clamp(cameraFallPitch, -5f, 5f);
        }

        private void HandleMovement()
        {
            var moveInput = input.KeyboardInput;
            var move = transform.right * moveInput.x + transform.forward * moveInput.y;
            move = Vector3.ClampMagnitude(move, 1f);
            var targetVelocity = move * moveSpeed;
            var smoothTime = controller.isGrounded ? groundSmoothTime : airSmoothTime;

            horizontalVelocity = Vector3.SmoothDamp(
                horizontalVelocity,
                targetVelocity,
                ref horizontalVelocityRef,
                smoothTime
            );

            if (controller.isGrounded)
            {
                if (verticalVelocity < 0)
                    verticalVelocity = -2f;

                if (input.JumpPressed)
                    verticalVelocity = jumpForce;
            }

            verticalVelocity += gravity * Time.deltaTime;

            var velocity = horizontalVelocity;
            velocity.y = verticalVelocity;

            controller.Move(velocity * Time.deltaTime);
        }

        private void HandleLook()
        {
            var mouse = input.MouseInput * mouseSensitivity;

            cameraPitch -= mouse.y;
            cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

            var targetRoll = Mathf.Clamp(-mouse.x * maxRoll, -maxRoll, maxRoll);

            cameraRoll = Mathf.SmoothDamp(
                cameraRoll,
                targetRoll,
                ref cameraRollVelocity,
                rollSmoothTime
            );

            transform.Rotate(Vector3.up * mouse.x);
            
            cameraPivot.localRotation = Quaternion.Euler(
                cameraPitch + cameraFallPitch,
                0f,
                cameraRoll
            );
        }
    }
}