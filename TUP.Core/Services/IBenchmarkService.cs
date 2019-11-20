using System;
using Examine;
using System.Collections.Generic;
using TUP.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace TUP.Core.Services
{
    public interface IBenchmarkService
    {
        List<TestResult> GetAllTestResults(int iterations = 1, bool loop = false);

        double GetTime(Action action);

        #region all posts

        IEnumerable<IPublishedContent> GetAllLinq(bool loop = false);

        IEnumerable<IPublishedContent> GetAllXPathGreedy(bool loop = false);

        IEnumerable<IPublishedContent> GetAllXPathEfficient(bool loop = false);

        IEnumerable<IPublishedContent> GetAllTypedExamine(bool loop = false);

        IEnumerable<ISearchResult> GetAllPureExamine(bool loop = false);

        #endregion

        #region latest ten posts

        IEnumerable<IPublishedContent> GetLatestLinq(bool loop = false);

        IEnumerable<IPublishedContent> GetLatestXPathGreedy(bool loop = false);

        IEnumerable<IPublishedContent> GetLatestXPathEfficient(bool loop = false);

        IEnumerable<IPublishedContent> GetLatestTypedExamine(bool loop = false);

        IEnumerable<ISearchResult> GetLatestPureExamine(bool loop = false);

        #endregion

        #region searches

        IEnumerable<IPublishedContent> GetSearchLinq(bool loop = false);

        IEnumerable<IPublishedContent> GetSearchXPathGreedy(bool loop = false);

        IEnumerable<IPublishedContent> GetSearchXPathEfficient(bool loop = false);

        IEnumerable<IPublishedContent> GetSearchTypedExamine(bool loop = false);

        IEnumerable<ISearchResult> GetSearchPureExamine(bool loop = false);

        #endregion
    }
}
