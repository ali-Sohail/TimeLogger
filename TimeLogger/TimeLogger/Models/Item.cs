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
    }
}