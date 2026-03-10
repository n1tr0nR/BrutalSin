using System;
using UnityEngine;

namespace UI.Title
{
    public class TitleScreenHandler : MonoBehaviour
    {
        public TitleScreenView currentView;
        public LevelsScreenView currentLevel;

        public GameObject mainView;
        public GameObject levelsView;
        public GameObject optionsView;
        
        private void Update()
        {
            levelsView.transform.localPosition =
                Vector2.Lerp(levelsView.transform.localPosition,
                    currentView.Equals(TitleScreenView.Levels) ? new Vector2(0, 0) : new Vector2(0, -600),
                    10 * Time.deltaTime);
            
            mainView.transform.localPosition =
                Vector2.Lerp(mainView.transform.localPosition,
                    currentView.Equals(TitleScreenView.Main) ? new Vector2(0, 0) : new Vector2(0, 600),
                    10 * Time.deltaTime);
            
            optionsView.transform.localPosition =
                Vector2.Lerp(optionsView.transform.localPosition,
                    currentView.Equals(TitleScreenView.Options) ? new Vector2(0, 0) : new Vector2(0, -600),
                    10 * Time.deltaTime);
        }

        public void SetMainView(int id)
        {
            currentView = id switch
            {
                0 => TitleScreenView.Main,
                1 => TitleScreenView.Levels,
                2 => TitleScreenView.Options,
                _ => currentView
            };
        }
    }

    public enum TitleScreenView
    {
        Main,
        Levels,
        Options
    }
    public enum LevelsScreenView
    {
        Difficulty,
        Levels
    }
}