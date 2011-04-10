namespace RdioSharp.Models
{
    public interface IRdioObject
    {
        string Key { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string Icon { get; set; }
        string BaseIcon { get; set; }
        RdioType RdioType { get; set; }
    }
}
