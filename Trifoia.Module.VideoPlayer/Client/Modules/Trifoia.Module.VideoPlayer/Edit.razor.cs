using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using MudBlazor;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Shared;
using Oqtane.Services;

using Trifoia.Module.VideoPlayer.Services;


namespace Trifoia.Module.VideoPlayer
{
    public partial class Edit: ModuleBase
    {
		[Inject] public VideoPlayerService VideoPlayerService { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }
		[Inject] public IStringLocalizer<Edit> Localizer { get; set; }		
        [Inject] public ISettingService SettingService { get; set; }

        private MudForm mudform;
        private bool success = false;
        private SettingsViewModel _settingsVM;
        private bool IsLoaded = false;
        private Models.VideoPlayer VideoPlayer { get; set; } = new();
        private int _VideoPlayerId;

		public override SecurityAccessLevel SecurityAccessLevel => SecurityAccessLevel.Edit;

		public override string Actions => "Add,Edit";

		public override string Title => "Manage VideoPlayer";

        public override List<Resource> Resources => new List<Resource>()
        {
            new Resource { ResourceType = ResourceType.Stylesheet,  Url = "https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" },
            new Resource { ResourceType = ResourceType.Stylesheet,  Url = "_content/MudBlazor/MudBlazor.min.css" },
            new Resource { ResourceType = ResourceType.Stylesheet,  Url = ModulePath() + "Module.css" },
            new Resource { ResourceType = ResourceType.Script,     Url = "_content/MudBlazor/MudBlazor.min.js", Location = ResourceLocation.Body, Level = ResourceLevel.Site },
            new Resource { ResourceType = ResourceType.Script,      Url = ModulePath() + "Module.js" },
        };

        protected override async Task OnInitializedAsync()
	    {
		    try
		    {
                var moduleSettings = await SettingService.GetModuleSettingsAsync(ModuleState.ModuleId);
                _settingsVM = new SettingsViewModel(SettingService, moduleSettings);
			    if (PageState.Action == "Edit")
			    {
				    _VideoPlayerId = Int32.Parse(PageState.QueryString["id"]);
                    (VideoPlayer, var code) = await VideoPlayerService.GetVideoPlayerAsync(_VideoPlayerId);
                    if (!IsSuccessStatusCode(code)) {
                        throw new HttpRequestException($"Error loading VideoPlayer. Code: {code}");
                    }
			    }
                IsLoaded = true;
            }
		    catch (Exception ex)
		    {
			    await logger.LogError(ex, "Error Loading VideoPlayer {VideoPlayerId} {Error}", _VideoPlayerId, ex.Message);
			    AddModuleMessage(Localizer["Message.LoadError"], MessageType.Error);
		    }
	    }

		private async Task Save()
		{
            try
            {
				await mudform.Validate();
				
                if (mudform.IsValid)
                {
                    if (PageState.Action == "Add")
                    {
                        VideoPlayer.ModuleId = ModuleState.ModuleId;
                        (VideoPlayer, var code) = await VideoPlayerService.AddVideoPlayerAsync(VideoPlayer);
                        if (code is not HttpStatusCode.OK) {
                            throw new HttpRequestException($"Error Adding {VideoPlayer}. Code: {code}");
                        }    
                        await logger.LogInformation("VideoPlayer Added {VideoPlayer}", VideoPlayer);
                    }
                    else
                    {
                        (var VideoPlayerLatest, var code) = await VideoPlayerService.GetVideoPlayerAsync(_VideoPlayerId);
                        if (code is not HttpStatusCode.OK) {
                            throw new HttpRequestException($"Error loading VideoPlayer. Code: {code}");
                        }
                    
                        // update values from the local version of VideoPlayer
                        VideoPlayerLatest.Name = VideoPlayer.Name;
                        // update Database with the latest version of VideoPlayer
                        (VideoPlayer, code) = await VideoPlayerService.AddVideoPlayerAsync(VideoPlayerLatest);
                        if (code is not HttpStatusCode.OK) {
                            throw new HttpRequestException($"Error Adding {VideoPlayer}. Code: {code}");
                        }         
                        await logger.LogInformation("VideoPlayer Updated {VideoPlayerLatest}", VideoPlayerLatest);
                    }
                    NavigationManager.NavigateTo(NavigateUrl());
                }
                else
                {
                    AddModuleMessage(Localizer["Message.SaveValidation"], MessageType.Warning);
                }
            }
            catch (Exception ex)
            {
                await logger.LogError(ex, "Error Saving VideoPlayer {Error}", ex.Message);
                AddModuleMessage(Localizer["Message.SaveError"], MessageType.Error);
            }
		}
    
        static bool IsSuccessStatusCode(HttpStatusCode statusCode) { 
            return (int)statusCode >= 200 && (int)statusCode <= 299; 
        }
    }
}
