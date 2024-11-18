using System.Text.Encodings.Web;
using FitnessApp.Common.Tests.Fixtures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FitnessApp.SettingsApi.IntegrationTests;

public class MockAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) :
    MockAuthenticationHandlerBase(options, logger, encoder);