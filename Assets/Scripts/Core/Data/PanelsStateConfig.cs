using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Data
{
    public class PanelsStateConfig
    {
        public PanelsStateData PanelsStateData { get; set; }

        public PanelsStateConfig InitializeDefaultValues()
        {
            var panelsStateData = new PanelsStateData()
            {
                IntroductionWindowOpenState = true,
                SettingsOpenState = false,
                MusicValue = Constants.MusicVolumeBaseValue,
                SoundValue = Constants.SoundVolumeBaseValue
            };

            PanelsStateData = panelsStateData;
            return this;
        }
        
        public async UniTask SaveStates()
        {
            var filePath = Path.Combine(Application.persistentDataPath, Constants.PanelsStateFile);
            
            await ClearData(filePath);
            
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            
            await File.WriteAllTextAsync(filePath, json);
        }
        
        public async UniTask ClearData(string filePath)
        {
            await File.WriteAllTextAsync(filePath, String.Empty);
        }

        public void SetSettingsState(bool state)
        {
            PanelsStateData.SettingsOpenState = state;
        }

        public void SetIntroductionWindowState(bool state)
        {
            PanelsStateData.IntroductionWindowOpenState = state;
        }

        public void SetSoundValue(float soundValue)
        {
            PanelsStateData.SoundValue = soundValue;
        }

        public void SetMusicValue(float musicValue)
        {
            PanelsStateData.MusicValue =  musicValue;
        }

        public float GetSoundValue()
        {
            return PanelsStateData.SoundValue;
        }

        public float GetMusicValue()
        {
            return PanelsStateData.MusicValue;
        }

        public bool GetIntroductionPanelState()
        {
            return PanelsStateData.IntroductionWindowOpenState;
        }

        public bool GetSettingsPanelState()
        {
            return PanelsStateData.SettingsOpenState;
        }
    }
}