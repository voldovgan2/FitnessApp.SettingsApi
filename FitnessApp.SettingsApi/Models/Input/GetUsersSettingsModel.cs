using FitnessApp.Abstractions.Models.Base;
using System.Collections.Generic;

namespace FitnessApp.SettingsApi.Models.Input
{
    public class GetUsersSettingsModel : IGetItemsModel
    {
        public IEnumerable<string> UsersIds { get; set; }
        public string Search { get; set; }
    }
}