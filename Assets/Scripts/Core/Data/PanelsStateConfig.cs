using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
                MusicValue = 1f,
                SoundValue = 1f
            };

            PanelsStateData = panelsStateData;
            return this;
        }
        
        public async Task SaveStates(string filePath)
        {
            await ClearData(filePath);
            
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            
            await File.WriteAllTextAsync(filePath, json);
        }
        
        public async Task ClearData(string filePath)
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