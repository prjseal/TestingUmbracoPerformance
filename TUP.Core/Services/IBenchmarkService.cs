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

        List<TestResult> GetAllTestResultsCached(int iterations = 1, bool loop = false);

        double GetTime(Action action);

        IEnumerable<IPublishedContent> GetCachedResult(string cacheKey, Func<IEnumerable<IPublishedContent>> function);

        IEnumerable<ISearchResult> GetCachedResult(string cacheKey, Func<IEnumerable<ISearchResult>> function);

        IEnumerable<IPublishedContent> GetAllLinq(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestLinq(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchLinq(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestPageLinq(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchPageLinq(bool loop = false);

        IEnumerable<IPublishedContent> GetAllChildrenOfId(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestChildrenOfId(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchChildrenOfId(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestPageChildrenOfId(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchPageChildrenOfId(bool loop = false);

        IEnumerable<IPublishedContent> GetAllChildrenOfType(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestChildrenOfType(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchChildrenOfType(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestPageChildrenOfType(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchPageChildrenOfType(bool loop = false);

        IEnumerable<IPublishedContent> GetAllXPathGreedy(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestXPathGreedy(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchXPathGreedy(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestPageXPathGreedy(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchPageXPathGreedy(bool loop = false);

        IEnumerable<IPublishedContent> GetAllXPathEfficient(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestXPathEfficient(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchXPathEfficient(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestPageXPathEfficient(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchPageXPathEfficient(bool loop = false);

        IEnumerable<ISearchResult> GetAllPureExamine(bool loop = false);
        IEnumerable<ISearchResult> GetLatestPureExamine(bool loop = false);
        IEnumerable<ISearchResult> GetSearchPureExamine(bool loop = false);
        IEnumerable<ISearchResult> GetLatestPagePureExamine(bool loop = false);
        IEnumerable<ISearchResult> GetSearchPagePureExamine(bool loop = false);

        IEnumerable<IPublishedContent> GetAllTypedExamine(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestTypedExamine(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchTypedExamine(bool loop = false);
        IEnumerable<IPublishedContent> GetLatestPageTypedExamine(bool loop = false);
        IEnumerable<IPublishedContent> GetSearchPageTypedExamine(bool loop = false);
    }
}
