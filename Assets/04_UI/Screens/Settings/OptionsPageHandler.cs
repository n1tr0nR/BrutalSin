using System;
using UnityEngine;

namespace _04_UI.Screens.Settings
{
    public class OptionsPageHandler : MonoBehaviour
    {
        public CanvasGroup[] pages;

        private int current;

        public void SetPage(int pageIndex)
        {
            current = Mathf.Clamp(pageIndex, 0, pages.Length - 1);

            for (var i = 0; i < pages.Length; i++)
            {
                if (i == current)
                {
                    pages[i].alpha = 1f;
                    pages[i].interactable = true;
                    pages[i].blocksRaycasts = true;
                }
                else
                {
                    pages[i].alpha = 0f;
                    pages[i].interactable = false;
                    pages[i].blocksRaycasts = false;
                }
            }
        }
    }
}