using Game.HeaderPanel.Model;
using Game.HeaderPanel.Presenter;
using Game.HeaderPanel.View;
using Game.HorizontalSlidingArea.Model;
using Game.HorizontalSlidingArea.Presenter;
using Game.HorizontalSlidingArea.View;
using Game.IntroductionWindow.Model;
using Game.IntroductionWindow.Presenter;
using Game.IntroductionWindow.View;
using Game.SettingsPanel.Model;
using Game.SettingsPanel.Presenter;
using Game.SettingsPanel.View;
using UnityEngine;

namespace Core
{
    public class Initialization: MonoBehaviour
    {
        [SerializeField] private IntroductionWindowView _introductionWindowView;

        [SerializeField] private HeaderView _headerView;
        
        [SerializeField] private SettingsView _settingsView;

        [SerializeField] private HorizontalSlideView _horizontalSlideView;

        private void Awake()
        {
            InitHeaderWindow();
            InitHorizontalSlideView();
            
        }
        private void InitHeaderWindow()
        {
            IntroductionModel introductionModel = new IntroductionModel(_introductionWindowView);
            IntroductionWindowPresenter introductionWindowPresenter = new IntroductionWindowPresenter(introductionModel);
            
            _introductionWindowView.Init(introductionWindowPresenter);
            
            SettingsModel settingsModel = new SettingsModel(_settingsView);
            SettingsPresenter settingsPresenter = new SettingsPresenter(settingsModel, introductionModel);
            
            _settingsView.Init(settingsPresenter);
            
            HeaderModel headerModel = new HeaderModel(_headerView);
            HeaderPresenter headerPresenter = new HeaderPresenter(headerModel, settingsModel);
            
            _headerView.Init(headerPresenter);
        }

        private void InitHorizontalSlideView()
        {
            HorizontalSlideModel horizontalSlideModel = new HorizontalSlideModel(_horizontalSlideView);
            HorizontalSlidePresenter horizontalSlidePresenter = new HorizontalSlidePresenter(horizontalSlideModel);

            _horizontalSlideView.Init(horizontalSlidePresenter);
        }
    }
}