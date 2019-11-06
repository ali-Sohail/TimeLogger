using Realms;
using System;

namespace TimeLogger.Models
{
    public class Item : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string TitleText { get; set; }
        public string InTime { get; set; }

        public string OutTime { get; set; }

        public string Description { get; set; }

        public string Duration
        {
            get
            {
                return CalDuration();
            }
            set
            {
                Duration = CalDuration();
                RaisePropertyChanged();
            }
        }

        private string CalDuration()
        {
            if (TimeSpan.TryParse(InTime, out TimeSpan inSpan) && TimeSpan.TryParse(OutTime, out TimeSpan outSpan))
            {
                return outSpan.Subtract(inSpan).ToString();
            }
            return new TimeSpan(0).ToString();
        }
    }
}