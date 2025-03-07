using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using Oqtane.Services;

using Trifoia.Module.VideoPlayer.Services;
using Blazored.Video.Support;
using Blazored.Video;
using System.ComponentModel;

namespace Trifoia.Module.VideoPlayer;

public partial class Index : ModuleBase
{
    List<Models.VideoPlayer> _list;
		
    [Inject] public VideoPlayerService VideoPlayerService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IStringLocalizer<Index> Localizer { get; set; }
    [Inject] public ISettingService SettingService { get; set; }
	
    public override List<Resource> Resources => new List<Resource>()
    {
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" },
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = "_content/MudBlazor/MudBlazor.min.css" },
        new Resource { ResourceType = ResourceType.Stylesheet,  Url = ModulePath() + "Module.css" },
        new Resource { ResourceType = ResourceType.Script,      Url = "_content/MudBlazor/MudBlazor.min.js", Location = ResourceLocation.Body, Level = ResourceLevel.Site },
        new Resource { ResourceType = ResourceType.Script,      Url = ModulePath() + "Module.js" },
    };	
    private bool IsLoaded;
    private SettingsViewModel _settingsVM;
    Dictionary<VideoEvents, VideoStateOptions> options = new();
    BlazoredVideo videoPlayer;


    protected override async Task OnInitializedAsync()
    {
        try
        {
            var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
            _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
           
            ((INotifyPropertyChanged)SiteState.Properties).PropertyChanged += PropertyChanged;

            IsLoaded = true;
        }
        catch (Exception ex)
        {
            await logger.LogError(ex, "Error Loading VideoPlayer {Error}", ex.Message);
            AddModuleMessage(Localizer["Message.LoadError"], MessageType.Error);
        }
    }


    async void PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "VideoSeek")
        {
            // Wait until videoPlayer is not null
            while (videoPlayer == null)
            {
                await Task.Delay(100); // Retry every 100 milliseconds
            }

            double seekTime = (double)SiteState.Properties.VideoSeek;
            await videoPlayer.SetCurrentTimeAsync(seekTime);
        }

        StateHasChanged();
    }

    
}

