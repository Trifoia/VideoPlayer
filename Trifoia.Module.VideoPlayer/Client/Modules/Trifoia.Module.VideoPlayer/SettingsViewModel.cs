using MudBlazor;
using Oqtane.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trifoia.Module.VideoPlayer
{
    internal class SettingsViewModel
    {
        public string Title { get; set; } = "UntitledVideo";
        public string Source { get; set; } = "https://samplelib.com/lib/preview/mp4/sample-5s.mp4";
        public string EnglishCaptions { get; set; } = String.Empty;
        public string SpanishCaptions { get; set; } = String.Empty;

        public SettingsViewModel(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {
            Title = settingService.GetSetting(moduleSettings, nameof(Title), Title);
            Source = settingService.GetSetting(moduleSettings, nameof(Source), Source);
            EnglishCaptions = settingService.GetSetting(moduleSettings, nameof(EnglishCaptions), String.Empty);
            SpanishCaptions = settingService.GetSetting(moduleSettings, nameof(SpanishCaptions), String.Empty);
        }

        public void SetSettings(ISettingService settingService, Dictionary<string, string> moduleSettings)
        {

            settingService.SetSetting(moduleSettings, nameof(Title), Title);
            settingService.SetSetting(moduleSettings, nameof(Source), Source);
            settingService.SetSetting(moduleSettings, nameof(EnglishCaptions), EnglishCaptions);
            settingService.SetSetting(moduleSettings, nameof(SpanishCaptions), SpanishCaptions);
        }
    }
}
