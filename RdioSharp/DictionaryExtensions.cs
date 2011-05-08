using System.Collections.Generic;

namespace RdioSharp
{
	public static class DictionaryExtensions
	{
		public static T GetOptionalKey<T>(this IDictionary<string, object> dict, string key)
		{
			object value;

			var found = dict.TryGetValue(key, out value);

			return found ? (T)value : default(T);
		}
	}
}