using Examine;
using Examine.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TUP.Core.Models;
using Umbraco.Core.Cache;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Examine;
using Umbraco.Web;

namespace TUP.Core.Services
{
    public class BenchmarkService : IBenchmarkService
    {

        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IContentService _contentService;
        private readonly IAppPolicyCache _runtimeCache;

        public BenchmarkService(IUmbracoContextAccessor umbracoContextAccessor, IContentService contentService, AppCaches caches)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _contentService = contentService;
            _runtimeCache = caches.RuntimeCache;
        }

        public List<TestResult> GetAllTestResultsCached(int iterations = 1, bool loop = false)
        {
            List<TestResult> testResults = new List<TestResult>();

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.All, 
                GetSetOfTimes(() => GetCachedResult($"GetAllLinq_{loop}", () => GetAllLinq(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.All,
                GetSetOfTimes(() => GetCachedResult($"GetAllChildrenOfId_{loop}", () => GetAllChildrenOfId(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.All,
                GetSetOfTimes(() => GetCachedResult($"GetAllChildrenOfType_{loop}", () => GetAllChildrenOfType(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.All,
                GetSetOfTimes(() => GetCachedResult($"GetAllXPathGreedy_{loop}", () => GetAllXPathGreedy(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.All,
                GetSetOfTimes(() => GetCachedResult($"GetAllXPathEfficient_{loop}", () => GetAllXPathEfficient(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.All,
                GetSetOfTimes(() => GetCachedResult($"GetAllPureExamine_{loop}", () => GetAllPureExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.All,
                GetSetOfTimes(() => GetCachedResult($"GetAllTypedExamine_{loop}", () => GetAllTypedExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestLinq_{loop}", () => GetLatestLinq(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestChildrenOfId_{loop}", () => GetLatestChildrenOfId(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestChildrenOfType_{loop}", () => GetLatestChildrenOfType(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestXPathGreedy_{loop}", () => GetLatestXPathGreedy(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestXPathEfficient_{loop}", () => GetLatestXPathEfficient(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPureExamine_{loop}", () => GetLatestPureExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.Latest,
                GetSetOfTimes(() => GetCachedResult($"GetLatestTypedExamine_{loop}", () => GetLatestTypedExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchLinq_{loop}", () => GetSearchLinq(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchChildrenOfId_{loop}", () => GetSearchChildrenOfId(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchChildrenOfType_{loop}", () => GetSearchChildrenOfType(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchXPathGreedy_{loop}", () => GetSearchXPathGreedy(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchXPathEfficient_{loop}", () => GetSearchXPathEfficient(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPureExamine_{loop}", () => GetSearchPureExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.Search,
                GetSetOfTimes(() => GetCachedResult($"GetSearchTypedExamine_{loop}", () => GetSearchTypedExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPageLinq_{loop}", () => GetLatestPageLinq(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPageChildrenOfId_{loop}", () => GetLatestPageChildrenOfId(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPageChildrenOfType_{loop}", () => GetLatestPageChildrenOfType(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPageXPathGreedy_{loop}", () => GetLatestPageXPathGreedy(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPageXPathEfficient_{loop}", () => GetLatestPageXPathEfficient(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPagePureExamine_{loop}", () => GetLatestPagePureExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.LatestPage,
                GetSetOfTimes(() => GetCachedResult($"GetLatestPageTypedExamine_{loop}", () => GetLatestPageTypedExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPageLinq_{loop}", () => GetSearchPageLinq(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPageChildrenOfId_{loop}", () => GetSearchPageChildrenOfId(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPageChildrenOfType_{loop}", () => GetSearchPageChildrenOfType(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPageXPathGreedy_{loop}", () => GetSearchPageXPathGreedy(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPageXPathEfficient_{loop}", () => GetSearchPageXPathEfficient(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPagePureExamine_{loop}", () => GetSearchPagePureExamine(loop)), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.SearchPage,
                GetSetOfTimes(() => GetCachedResult($"GetSearchPageTypedExamine_{loop}", () => GetSearchPageTypedExamine(loop)), iterations), loop));

            return testResults;
        }

        public List<TestResult> GetAllTestResults(int iterations = 1, bool loop = false)
        {
            List<TestResult> testResults = new List<TestResult>();

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.All, 
                GetSetOfTimes(() => GetAllLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.All,
                GetSetOfTimes(() => GetAllChildrenOfId(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.All,
                GetSetOfTimes(() => GetAllChildrenOfType(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.All,
                GetSetOfTimes(() => GetAllXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.All,
                GetSetOfTimes(() => GetAllXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.All,
                GetSetOfTimes(() => GetAllPureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.All,
                GetSetOfTimes(() => GetAllTypedExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.Latest,
                GetSetOfTimes(() => GetLatestLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.Latest,
                GetSetOfTimes(() => GetLatestChildrenOfId(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.Latest,
                GetSetOfTimes(() => GetLatestChildrenOfType(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.Latest,
                GetSetOfTimes(() => GetLatestXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.Latest,
                GetSetOfTimes(() => GetLatestXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.Latest,
                GetSetOfTimes(() => GetLatestPureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.Latest,
                GetSetOfTimes(() => GetLatestTypedExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.Search,
                GetSetOfTimes(() => GetSearchLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.Search,
                GetSetOfTimes(() => GetSearchChildrenOfId(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.Search,
                GetSetOfTimes(() => GetSearchChildrenOfType(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.Search,
                GetSetOfTimes(() => GetSearchXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.Search,
                GetSetOfTimes(() => GetSearchXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.Search,
                GetSetOfTimes(() => GetSearchPureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.Search,
                GetSetOfTimes(() => GetSearchTypedExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPageLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPageChildrenOfId(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPageChildrenOfType(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPageXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPageXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPagePureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.LatestPage,
                GetSetOfTimes(() => GetLatestPageTypedExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.DescendantsOfType, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPageLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfId, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPageChildrenOfId(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ChildrenOfType, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPageChildrenOfType(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPageXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPageXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPagePureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.SearchPage,
                GetSetOfTimes(() => GetSearchPageTypedExamine(loop), iterations), loop));

            return testResults;
        }

        public List<double> GetSetOfTimes(Action action, int iterations)
        {
            var times = new List<double>();
            for (var i = 0; i < iterations; i++) times.Add(GetTime(action));
            return times;
        }

        public double GetTime(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        public IEnumerable<IPublishedContent> GetCachedResult(string cacheKey, Func<IEnumerable<IPublishedContent>> function)
        {
            return _runtimeCache.GetCacheItem<IEnumerable<IPublishedContent>>(cacheKey, () => function.Invoke());
        }

        public IEnumerable<ISearchResult> GetCachedResult(string cacheKey, Func<IEnumerable<ISearchResult>> function)
        {
            return _runtimeCache.GetCacheItem<IEnumerable<ISearchResult>>(cacheKey, () => function.Invoke());
        }

        public IEnumerable<IPublishedContent> GetAllLinq(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot()
                .DescendantsOrSelfOfType("article");

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestLinq(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .DescendantsOrSelfOfType("article")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchLinq(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .DescendantsOrSelfOfType("article")
                .Where(a => a.Name.IndexOf("Popular blogs") > -1)
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestPageLinq(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .DescendantsOrSelfOfType("article")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchPageLinq(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .DescendantsOrSelfOfType("article")
                .Where(a => a.Name.IndexOf("Popular blogs") > -1)
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }


        public IEnumerable<IPublishedContent> GetAllChildrenOfId(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetById(1086).Children;

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestChildrenOfId(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetById(1086).Children
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchChildrenOfId(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetById(1086).Children
                .Where(a => a.Name.IndexOf("Popular blogs") > -1)
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestPageChildrenOfId(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetById(1086).Children
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchPageChildrenOfId(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetById(1086).Children
                .Where(a => a.Name.IndexOf("Popular blogs") > -1)
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetAllChildrenOfType(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .ChildrenOfType("articleList").FirstOrDefault().Children;

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestChildrenOfType(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .ChildrenOfType("articleList").FirstOrDefault().Children
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchChildrenOfType(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .ChildrenOfType("articleList").FirstOrDefault().Children
                .Where(a => a.Name.IndexOf("Popular blogs") > -1)
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestPageChildrenOfType(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .ChildrenOfType("articleList").FirstOrDefault().Children
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchPageChildrenOfType(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .ChildrenOfType("articleList").FirstOrDefault().Children
                .Where(a => a.Name.IndexOf("Popular blogs") > -1)
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetAllXPathGreedy(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetByXPath("//article [@isDoc]");

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestXPathGreedy(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetByXPath("//article [@isDoc]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchXPathGreedy(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("//article [@isDoc and contains(@nodeName, 'Popular blogs')]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestPageXPathGreedy(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content.GetByXPath("//article [@isDoc]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchPageXPathGreedy(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("//article [@isDoc and contains(@nodeName, 'Popular blogs')]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetAllXPathEfficient(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home [@isDoc]/articleList [@isDoc]/article [@isDoc]");

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestXPathEfficient(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home [@isDoc]/articleList [@isDoc]/article [@isDoc]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchXPathEfficient(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home/articleList/article [@isDoc and contains(@nodeName, 'Popular blogs')]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetLatestPageXPathEfficient(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home [@isDoc]/articleList [@isDoc]/article [@isDoc]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<IPublishedContent> GetSearchPageXPathEfficient(bool loop = false)
        {
            var results = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home/articleList/article [@isDoc and contains(@nodeName, 'Popular blogs')]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Skip(40).Take(10);

            if (loop == true)
            {
                foreach (var item in results)
                {

                }
            }

            return results;
        }

        public IEnumerable<ISearchResult> GetAllPureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .Execute(8000);

                if (loop == true)
                {
                    foreach (var item in results)
                    {

                    }
                }

                return results;
            }

            return Enumerable.Empty<ISearchResult>();
        }

        public IEnumerable<ISearchResult> GetLatestPureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                if (loop == true)
                {
                    foreach (var item in results)
                    {

                    }
                }

                return results;
            }

            return Enumerable.Empty<ISearchResult>();
        }

        public IEnumerable<ISearchResult> GetSearchPureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .And().Field("nodeName", "Popular blogs")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                if (loop == true)
                {
                    foreach (var item in results)
                    {

                    }
                }

                return results;
            }

            return Enumerable.Empty<ISearchResult>();
        }

        public IEnumerable<ISearchResult> GetLatestPagePureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Skip(40).Take(10);

                if (loop == true)
                {
                    foreach (var item in results)
                    {

                    }
                }

                return results;
            }

            return Enumerable.Empty<ISearchResult>();
        }

        public IEnumerable<ISearchResult> GetSearchPagePureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .And().Field("nodeName", "Popular blogs")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Skip(40).Take(10);

                if (loop == true)
                {
                    foreach (var item in results)
                    {

                    }
                }

                return results;
            }

            return Enumerable.Empty<ISearchResult>();
        }

        public IEnumerable<IPublishedContent> GetAllTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article").Execute(8000);

                var items = new List<IPublishedContent>();
                foreach (var item in results)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }

                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        public IEnumerable<IPublishedContent> GetLatestTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                var items = new List<IPublishedContent>();
                foreach (var item in results)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }

                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        public IEnumerable<IPublishedContent> GetSearchTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .And().Field("nodeName", "Popular blogs")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                var items = new List<IPublishedContent>();
                foreach (var item in results)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }

                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        public IEnumerable<IPublishedContent> GetLatestPageTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Skip(40).Take(10);

                var items = new List<IPublishedContent>();
                foreach (var item in results)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }

                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }

        public IEnumerable<IPublishedContent> GetSearchPageTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery().NodeTypeAlias("article")
                    .And().Field("nodeName", "Popular blogs")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Skip(40).Take(10);

                var items = new List<IPublishedContent>();
                foreach (var item in results)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }

                return items;
            }

            return Enumerable.Empty<IPublishedContent>();
        }
    }
}
