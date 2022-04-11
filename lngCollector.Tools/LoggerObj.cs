using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lngCollector.Tools
{
    public static class LoggerObj
    {
        public static void Write(object o)
        {
            var j = JsonConvert.SerializeObject(o, Formatting.Indented);
            Console.WriteLine(j);
        }
    }
}
