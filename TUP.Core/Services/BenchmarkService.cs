using Examine;
using Examine.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TUP.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Services;
using Umbraco.Examine;
using Umbraco.Web;

namespace TUP.Core.Services
{
    public class BenchmarkService : IBenchmarkService
    {
        #region all posts

        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IContentService _contentService;

        public BenchmarkService(IUmbracoContextAccessor umbracoContextAccessor, IContentService contentService)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _contentService = contentService;
        }

        public List<TestResult> GetAllTestResults(int iterations = 1, bool loop = false)
        {
            List<TestResult> testResults = new List<TestResult>();

            testResults.Add(new TestResult(QueryMethod.Linq, QueryType.All, 
                GetSetOfTimes(() => GetAllLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.All,
                GetSetOfTimes(() => GetAllXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.All,
                GetSetOfTimes(() => GetAllXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.All,
                GetSetOfTimes(() => GetAllPureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.All,
                GetSetOfTimes(() => GetAllTypedExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.Linq, QueryType.Latest,
                GetSetOfTimes(() => GetLatestLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.Latest,
                GetSetOfTimes(() => GetLatestXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.Latest,
                GetSetOfTimes(() => GetLatestXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.Latest,
                GetSetOfTimes(() => GetLatestPureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.Latest,
                GetSetOfTimes(() => GetLatestTypedExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.Linq, QueryType.Search,
                GetSetOfTimes(() => GetSearchLinq(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathGreedy, QueryType.Search,
                GetSetOfTimes(() => GetSearchXPathGreedy(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.XPathEfficient, QueryType.Search,
                GetSetOfTimes(() => GetSearchXPathEfficient(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExaminePure, QueryType.Search,
                GetSetOfTimes(() => GetSearchPureExamine(loop), iterations), loop));

            testResults.Add(new TestResult(QueryMethod.ExamineTyped, QueryType.Search,
                GetSetOfTimes(() => GetSearchTypedExamine(loop), iterations), loop));



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

        

        #endregion

        #region latest ten posts

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

        #endregion

        #region searches

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

        #endregion
    }
}
