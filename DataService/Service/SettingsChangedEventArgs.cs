using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entity;

namespace DataService.Service
{
    public class SettingsChangedEventArgs : EventArgs
    {
        public SettingsChangedEventArgs(Setting setting)
        {
            Setting = setting;
        }

        public Setting Setting { get; }
    }
}
