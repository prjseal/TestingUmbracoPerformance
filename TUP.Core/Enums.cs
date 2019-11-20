namespace TUP.Core
{
    public enum QueryMethod
    {
        Linq = 0,
        XPathGreedy = 1,
        XPathEfficient = 2,
        ExaminePure = 3,
        ExamineTyped = 4,
    }

    public enum QueryType
    {
        All = 0,
        Latest = 1,
        Search = 2,
        Page = 3
    }
}
