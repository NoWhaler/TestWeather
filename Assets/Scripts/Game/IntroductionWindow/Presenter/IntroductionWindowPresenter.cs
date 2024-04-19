using Game.IntroductionWindow.Model;
using UnityEngine;

namespace Game.IntroductionWindow.Presenter
{
    public class IntroductionWindowPresenter
    {
        private IntroductionModel _introductionModel;

        public IntroductionWindowPresenter(IntroductionModel introductionModel)
        {
            _introductionModel = introductionModel;
        }

        public void OnLinkButtonClick()
        {
            OpenURL(_introductionModel.LinkedinURL);
        }

        public void OnExitButtonClick()
        {
            CloseWindow();
        }

        private void OpenURL(string link)
        {
            Application.OpenURL(link);
        }

        private void CloseWindow()
        {
            _introductionModel.SetOpenState(false);
        }
    }
}
