using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;
using DataService.Service;

namespace DataService.IService
{
    public interface ISettingsService
    {
        Setting Setting { get; set; }
        event EventHandler<SettingsChangedEventArgs> SettingsChanged;
        void RaiseSettingsChanged();
    }
}
