using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SearchResult
    {
        public double ElapsedMilliseconds { get; set; }
        public List<string> IgnoredTerms { get; set; } = new List<string>();
        public List<Documents> Documents { get; set; } = new List<Documents>();

    }
}
