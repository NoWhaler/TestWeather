using Game.IntroductionWindow.View;
using UnityEngine;

namespace Game.IntroductionWindow.Model
{
    public class IntroductionModel
    {
        private IntroductionWindowView _introductionWindowView;

        public string LinkedinURL { get; set; } = "https://www.linkedin.com/in/nikita-zlochevskyi-578803249/";
        
        public bool CurrentOpenState { get; set; }

        public IntroductionModel(IntroductionWindowView introductionWindowView)
        {
            _introductionWindowView = introductionWindowView;
        }

        public void SetOpenState(bool state)
        {
            CurrentOpenState = state;
            _introductionWindowView.DisplayOrCloseWindow(CurrentOpenState);
        }
    }
}
