using System.Collections.Generic;
using System.Linq;

namespace TUP.Core.Models
{
    public class TestResult
    {
        public QueryMethod Method;
        public QueryType Type;
        public List<double> Times;
        public bool Loop;
        public int Iterations => Times.Count();
        public double TotalTimes => Times.Sum();
        public double AverageTime => TotalTimes > 0 ? TotalTimes / Iterations : 0;

        public TestResult(QueryMethod method, QueryType type, List<double> times, bool loop)
        {
            Method = method;
            Type = type;
            Times = times;
            Loop = loop;
        }
    }
}
