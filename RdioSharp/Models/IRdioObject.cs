namespace RdioSharp.Models
{
    public interface IRdioObject
    {
        string Key { get; }
        string Name { get; }
        string Url { get; }
        string Icon { get; }
        string BaseIcon { get; }
        RdioType RdioType { get; }
    }
}
