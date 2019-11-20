namespace TUP.Core
{
    public enum QueryMethod
    {
        DescendantsOfType = 0,
        ChildrenOfId = 2,
        ChildrenOfType = 3,
        XPathGreedy = 4,
        XPathEfficient = 5,
        ExaminePure = 6,
        ExamineTyped = 7,
    }

    public enum QueryType
    {
        All = 0,
        Latest = 1,
        Search = 2,
        LatestPage = 3,
        SearchPage = 4,
    }
}
