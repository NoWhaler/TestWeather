using Core.Bootstrapper;
using Game.IntroductionWindow.Model;
using UnityEngine;

namespace Game.IntroductionWindow.Presenter
{
    public class IntroductionWindowPresenter
    {
        private IntroductionModel _introductionModel;
        private Bootstrap _bootstrap;

        public IntroductionWindowPresenter(IntroductionModel introductionModel, Bootstrap bootstrap)
        {
            _introductionModel = introductionModel;
            _bootstrap = bootstrap;
        }

        public void OnLinkButtonClick()
        {
            OpenURL(_introductionModel.LinkedinURL);
        }

        public async void OnExitButtonClick()
        {
            CloseWindow();
            _bootstrap.PanelsStateConfig.SetIntroductionWindowState(false);

            await _bootstrap.PanelsStateConfig.SaveStates();
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
