using Core;
using Extensions;
using Game.SettingsPanel.Presenter;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.SettingsPanel.View
{
    public class SettingsView : MonoBehaviour
    {
        private bool _isOpen;
        
        private SettingsPresenter _settingsPresenter;

        [SerializeField] private Button _closeButton;

        [SerializeField] private Button _introductionWindowButton;

        [SerializeField] private Button _soundButton;

        [SerializeField] private Button _musicButton;

        [SerializeField] private Image _soundImage;

        [SerializeField] private Image _musicImage;

        [SerializeField] private Slider _soundSlider;

        [SerializeField] private Slider _musicSlider;
        
        [SerializeField] private Sprite[] _soundSprites;

        [SerializeField] private Sprite[] _musicSprites;

        [SerializeField] private AudioMixer _soundMixer;

        [SerializeField] private AudioMixer _musicMixer;

        private const float ANIMATION_DURATION = 0.4f;
        
        private void Start()
        {
            _closeButton.onClick.AddListener(() => 
                _settingsPresenter.OnCloseButtonClick());
            
            _introductionWindowButton.onClick.AddListener(() =>
                _settingsPresenter.OnIntroductionWindowButtonClick());
            
            _soundButton.onClick.AddListener(() => 
                _settingsPresenter.OnSoundButtonClick(IsSoundToggled()));
            
            _musicButton.onClick.AddListener(() => 
                _settingsPresenter.OnMusicButtonClick(IsMusicToggled()));
        }

        private bool IsSoundToggled()
        {
            return _soundImage.sprite == _soundSprites[0];
        }

        private bool IsMusicToggled()
        {
            return _musicImage.sprite == _musicSprites[0];
        }

        public void OnChangeSoundValue(float value)
        {
            _soundMixer.SetFloat(Constants.SoundAudioMixer, Mathf.Log10(value) * 20);
            
            _soundImage.sprite = _soundSlider.value == 0 ? _soundSprites[1] : _soundSprites[0];
            
            _settingsPresenter.OnSoundValueChange(_soundSlider.value);
        }

        public void OnChangeMusicValue(float value)
        {
            _musicMixer.SetFloat(Constants.MusicAudioMixer, Mathf.Log10(value) * 20);

            _musicImage.sprite = _musicSlider.value == 0 ? _musicSprites[1] : _musicSprites[0];
            
            _settingsPresenter.OnMusicValueChange(_musicSlider.value);
        }

        public void Init(SettingsPresenter settingsPresenter, float soundValue, float musicValue)
        {
            _settingsPresenter = settingsPresenter;

            _soundSlider.value = soundValue;
            _musicSlider.value = musicValue;
            _settingsPresenter.SetValuesFromJson();
        }

        public void DisplayOrCloseWindow(bool state)
        {
            _isOpen = state;
            
            if (_isOpen)
            {
                gameObject.ScaleTo(1f, ANIMATION_DURATION);
            }
            else
            {
                gameObject.ScaleTo(0f, ANIMATION_DURATION);
            }
        }

        public void SwitchOnOffSound()
        {
            _soundImage.sprite = _soundImage.sprite == _soundSprites[0] ? _soundSprites[1] : _soundSprites[0];

            if (_soundImage.sprite == _soundSprites[0])
            {
                _soundImage.sprite = _soundSprites[1];
            }
            else
            {
                _soundImage.sprite = _soundSprites[0];
                _soundSlider.value = 0;
            }
        }

        public void SwitchOnOffMusic()
        {
            _musicImage.sprite = _musicImage.sprite == _musicSprites[0] ? _musicSprites[1] : _musicSprites[0];
            
            if (_musicImage.sprite == _musicSprites[0])
            {
                _musicImage.sprite = _musicSprites[1];
            }
            else
            {
                _musicImage.sprite = _musicSprites[0];
                _musicSlider.value = 0;
            }
        }
    }
}
