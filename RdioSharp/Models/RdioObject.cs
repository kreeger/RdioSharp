using System;

namespace RdioSharp.Models
{
    public class RdioObject
    {
        public string Key { get; private set; }
        public string Url { get; private set; }
        public string Icon { get; private set; }
        public string BaseIcon { get; private set; }
        public RdioType RdioType { get; private set; }

        internal RdioObject(string key, string url, string icon, string baseIcon, RdioType rdioType)
        {
            Key = key;
            Url = url;
            Icon = icon;
            BaseIcon = baseIcon;
            RdioType = rdioType;
        }
    }
}
