using System.Collections.Generic;
using System.IO;
using Core.Bootstrapper;
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
using Cysharp.Threading.Tasks;
using Game.FooterPanel.View;
using Game.WeatherCardsPanel.View;
using TMPro;
using Zenject;
using Application = UnityEngine.Device.Application;

namespace Core
{
    public class Initialization: MonoBehaviour
    {
        [SerializeField] private IntroductionWindowView _introductionWindowView;

        [SerializeField] private HeaderView _headerView;
        
        [SerializeField] private SettingsView _settingsView;

        [SerializeField] private HorizontalSlideView _horizontalSlideView;

        [SerializeField] private FooterView _footerView;

        [SerializeField] private List<WeatherCardView> _weatherCardViews;

        [Inject] private Bootstrap _bootstrap;

        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            InitViews();
        }

        private async void Start()
        {
            await LoadWeatherCards();
        }

        private async UniTask LoadWeatherCards()
        {
            for (int i = 0; i < _bootstrap.WeatherConfig.WeatherCards.Count; i++)
            {
                if (i < _weatherCardViews.Count)
                {
                    _weatherCardViews[i].WeatherCard = _bootstrap.WeatherConfig.WeatherCards[i];
                    
                    await _weatherCardViews[i].SetView();
                }
            }

            await _bootstrap.WeatherConfig.SaveDataToFile();
        }
        
        private void InitViews()
        {
            HorizontalSlideModel horizontalSlideModel = new HorizontalSlideModel(_horizontalSlideView);
            HorizontalSlidePresenter horizontalSlidePresenter = new HorizontalSlidePresenter(horizontalSlideModel);
            
            IntroductionModel introductionModel = new IntroductionModel(_introductionWindowView);
            IntroductionWindowPresenter introductionWindowPresenter = new IntroductionWindowPresenter(introductionModel, _bootstrap);
            
            SettingsModel settingsModel = new SettingsModel(_settingsView);
            SettingsPresenter settingsPresenter = new SettingsPresenter(settingsModel, introductionModel, _bootstrap);
            
            HeaderModel headerModel = new HeaderModel(_headerView);
            HeaderPresenter headerPresenter = new HeaderPresenter(headerModel, settingsModel, _bootstrap);
            
            _headerView.Init(headerPresenter);
            _horizontalSlideView.Init(horizontalSlidePresenter);
            _introductionWindowView.Init(introductionWindowPresenter);
            _settingsView.Init(settingsPresenter, _bootstrap.PanelsStateConfig.GetSoundValue(), _bootstrap.PanelsStateConfig.GetMusicValue());
        }
    }
}