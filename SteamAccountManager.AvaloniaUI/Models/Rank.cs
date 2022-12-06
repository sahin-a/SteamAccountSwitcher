using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.AvaloniaUI.Models
{
    public class Rank
    {
        public int Level { get; set; }
        public string Color => GetLevelColor();

        private string GetLevelColor()
        {
            var level = (Level % 100);

            switch (level)
            {
                default:
                case < 10:
                    return "#7c7d7f";
                case < 20:
                    return "#c02942";
                case < 30:
                    return "#d95b43";
                case < 40:
                    return "#ffd023";
                case < 50:
                    return "#467a3c";
                case < 60:
                    return "#4e8ddb";
                case < 70:
                    return "#7552c7";
                case < 80:
                    return "#c252c9";
                case < 90:
                    return "#61223d";
                case < 100:
                    return "#997c52";
            }
        }
    }
}
