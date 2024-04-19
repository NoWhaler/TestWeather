using Game.HeaderPanel.Model;
using Game.HeaderPanel.Presenter;
using Game.HeaderPanel.View;
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

        private void Awake()
        {
            // InitIntroductionWindow();
            InitHeaderWindow();
            InitSettingsWindow();
        }

        private void InitIntroductionWindow()
        {
            IntroductionModel introductionModel = new IntroductionModel(_introductionWindowView);
            IntroductionWindowPresenter introductionWindowPresenter = new IntroductionWindowPresenter(introductionModel);
            
            _introductionWindowView.Init(introductionWindowPresenter);
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

        private void InitSettingsWindow()
        { 
        }
    }
}