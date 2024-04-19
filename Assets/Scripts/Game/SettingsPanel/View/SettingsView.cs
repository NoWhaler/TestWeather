using Game.SettingsPanel.Presenter;
using UnityEngine;
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

        [SerializeField] private Sprite[] _soundSprites;

        [SerializeField] private Sprite[] _musicSprites;

        private void OnEnable()
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

        public void Init(SettingsPresenter settingsPresenter)
        {
            _settingsPresenter = settingsPresenter;
        }

        public void DisplayOrCloseWindow(bool state)
        {
            gameObject.SetActive(state);
        }

        public void SwitchOnOffSound()
        {
            _soundImage.sprite = _soundImage.sprite == _soundSprites[0] ? _soundSprites[1] : _soundSprites[0];
        }

        public void SwitchOnOffMusic()
        {
            _musicImage.sprite = _musicImage.sprite == _musicSprites[0] ? _musicSprites[1] : _musicSprites[0];
        }
    }
}
