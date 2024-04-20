using Game.SettingsPanel.Presenter;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.SettingsPanel.View
{
    public class SettingsView : MonoBehaviour
    {
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
            if (_soundImage.sprite == _soundSprites[0])
            {
                return true;
            }
            
            return false;
        }

        private bool IsMusicToggled()
        {
            if (_musicImage.sprite == _musicSprites[0])
            {
                return true;
            }

            return false;
        }

        public void OnChangeSoundValue(float value)
        {
            _soundMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
            
            _soundImage.sprite = _soundSlider.value == 0 ? _soundSprites[1] : _soundSprites[0];
            
            _settingsPresenter.OnSoundValueChange(_soundSlider.value);
        }

        public void OnChangeMusicValue(float value)
        {
            _musicMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);

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
            gameObject.SetActive(state);
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
