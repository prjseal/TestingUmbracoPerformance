using Examine;
using Examine.Search;
using System;
using System.Collections.Generic;
using System.Linq;
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
