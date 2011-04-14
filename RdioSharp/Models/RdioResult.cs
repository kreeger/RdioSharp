using System.Runtime.Serialization;

namespace RdioSharp.Models
{
    [DataContract]
    public class RdioResult<T> where T : class
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public T Result { get; set; }

        public RdioResult() { }

        public RdioResult(string status, T result)
        {
            Status = status;
            Result = result;
        }
    }
}
