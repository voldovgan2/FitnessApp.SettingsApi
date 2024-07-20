using System.Threading.Tasks;
using AutoMapper;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Contracts.Output;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.SettingsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

[Authorize]
public class SettingsController(ISettingsService settingsService, IMapper mapper) : Controller
{
    [HttpGet("GetSettings/{userId}")]
    public async Task<SettingsContract> GetSettings([FromRoute] string userId)
    {
        var response = await settingsService.GetSettingsByUserId(userId);
        return mapper.Map<SettingsContract>(response);
    }

    [HttpPost("CreateSettings")]
    public async Task<SettingsContract> CreateSettings([FromBody]CreateSettingsContract contract)
    {
        var model = mapper.Map<CreateSettingsGenericModel>(contract);
        var response = await settingsService.CreateSettings(model);
        return mapper.Map<SettingsContract>(response);
    }

    [HttpPut("UpdateSettings")]
    public async Task<SettingsContract> UpdateSettings([FromBody]UpdateSettingsContract contract)
    {
        var model = mapper.Map<UpdateSettingsGenericModel>(contract);
        var response = await settingsService.UpdateSettings(model);
        return mapper.Map<SettingsContract>(response);
    }

    [HttpDelete("DeleteSettings/{userId}")]
    public async Task<string> DeleteSettings([FromRoute] string userId)
    {
        var response = await settingsService.DeleteSettings(userId);
        return response;
    }
}

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class TestController() : Controller
{
    [HttpGet("GetSettings")]
    public Task<SettingsContract> GetSettings()
    {
        return Task.FromResult(new SettingsContract { CanFollow = Enums.PrivacyType.Followers });
    }

    [HttpDelete("DeleteSettings")]
    public Task<string> DeleteSettings()
    {
        var response = "svTest";
        return Task.FromResult(response);
    }
}