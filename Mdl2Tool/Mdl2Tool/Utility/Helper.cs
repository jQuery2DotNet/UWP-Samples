using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mdl2Tool.Utility
{
    public class Helper
    {
        public static Dictionary<string, string> GetFieldValues(object obj)
        {
            return obj.GetType().GetProperties().ToDictionary(f => f.Name, f => (string)f.GetValue(null));
        }
    }
}
