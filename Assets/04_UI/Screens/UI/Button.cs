using _06_Audio;
using Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onClick;
        [SerializeField] private UnityEvent onHoverEnter;
        [SerializeField] private UnityEvent onHoverExit;

        [Header("Visuals")] 
        [SerializeField] private GameObject enableOnHover;

        [SerializeField] private RectTransform buttonTransform;
        [SerializeField] private Vector2 defaultButtonDimensions;
        [SerializeField] private Vector2 hoverButtonDimensions;

        [SerializeField] private AudioClip hoverClip;
        [SerializeField] private AudioClip clickClip;

        private bool hovering;

        public void OnPointerEnter(PointerEventData eventData)
        {
            hovering = true;
            AudioHelper.PlayClipAtPoint(hoverClip, Vector3.zero, 1.0F, 0.1F, MusicManager.Instance.sound);
            enableOnHover.SetActive(true);
            onHoverEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hovering = false;
            enableOnHover.SetActive(false);
            onHoverExit?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
            AudioHelper.PlayClipAtPoint(clickClip, Vector3.zero, 1.0F, 0.1F, MusicManager.Instance.sound);
            hovering = false;
            enableOnHover.SetActive(false);
        }

        private void Update()
        {
            var targetSize = hovering ? hoverButtonDimensions : defaultButtonDimensions;

            buttonTransform.sizeDelta = Vector2.Lerp(
                buttonTransform.sizeDelta,
                targetSize,
                Time.deltaTime * 10
            );
        }
    }
}