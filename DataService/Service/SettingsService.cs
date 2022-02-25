using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.IService;

namespace DataService.Service
{
    public class SettingsService : ISettingsService
    {
        public Setting Setting { get; set; } = new Setting
        {
            AppTitle = "Latihan Blazor",
            UserPictureUrl = "images/profile.png",

        };

        public event EventHandler<SettingsChangedEventArgs> SettingsChanged;
        public void RaiseSettingsChanged()
        {
            SettingsChanged?.Invoke(this, new SettingsChangedEventArgs(Setting));
        }
    }
}
