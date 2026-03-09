using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Title
{
    public class UiFloater : MonoBehaviour
    {
        [SerializeField] private float multiplier = 0.02f;

        private Vector2 targetPosition;
        
        private void Update()
        {
            var mouse = Mouse.current.position.ReadValue();
            var center = new Vector2(Screen.width, Screen.height) * 0.5f;

            targetPosition = (mouse - center) * multiplier;

            transform.localPosition = Vector2.Lerp(transform.localPosition, targetPosition, 5 * Time.deltaTime);
        }
    }
}