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

        public void GetByBadLinq(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .Descendants()
                .Where(a => a.ContentType.Alias == "article");

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetAllChildren(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content
                .GetById(false, 1095).Children;

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetAllDescendants(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot()
                .DescendantsOrSelfOfType("article");

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetAllXPathGreedy(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetByXPath("//article [@isDoc]");

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetAllXPathEfficient(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home [@isDoc]/articleList [@isDoc]/article [@isDoc]");

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var test = searcher.CreateQuery().NodeTypeAlias("article").Execute();

                var items = new List<IPublishedContent>();
                foreach (var item in test)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }
            }
        }

        public void GetPureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var test = searcher.CreateQuery().NodeTypeAlias("article")
                    .Execute();

                if (loop == true)
                {
                    foreach (var item in test)
                    {

                    }
                }
            }
        }

        public void GetByContentService(bool loop = false)
        {
            var test = _contentService.GetPagedOfType(1082, 0, 10000, out var totalRecords, null);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        #endregion

        #region latest ten posts

        public void LatestBadLinq(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .Descendants().Where(a => a.ContentType.Alias == "article")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void LatestChildrenFromNode(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetById(false, 1095)
                .Children.OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetLatestDescendants(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .Descendants("article")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetLatestXPathGreedy(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetByXPath("//article [@isDoc]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetLatestXPathEfficient(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home [@isDoc]/articleList [@isDoc]/article [@isDoc]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void LatestTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var test = searcher.CreateQuery().NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                var items = new List<IPublishedContent>();
                foreach (var item in test)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }
            }
        }



        public void GetLatestPureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var test = searcher.CreateQuery().NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                if (loop == true)
                {
                    foreach (var item in test)
                    {

                    }
                }
            }
        }

        #endregion

        #region searches

        public void GetSearchLinq(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content.GetAtRoot().FirstOrDefault()
                .DescendantsOrSelf()
                .Where(a => a.ContentType.Alias == "article" && a.Name.Contains("Popular Blogs"))
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetSearchXsltGreedy(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("//article [@isDoc  and contains(@nodeName, 'Popular Blogs')]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetSearchXsltEfficient(bool loop = false)
        {
            var test = _umbracoContextAccessor.UmbracoContext.Content
                .GetByXPath("root/home/articleList/article [@isDoc  and contains(@nodeName, 'Popular Blogs')]")
                .OrderByDescending(a => a.Value<DateTime>("articleDate")).Take(10);

            if (loop == true)
            {
                foreach (var item in test)
                {

                }
            }
        }

        public void GetSearchTypedExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var test = searcher.CreateQuery().NodeTypeAlias("article")
                    .And().Field("nodeName", "Popular Blogs")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                var items = new List<IPublishedContent>();
                foreach (var item in test)
                {
                    items.Add(_umbracoContextAccessor.UmbracoContext.Content.GetById(false, int.Parse(item.Id)));
                }
            }
        }

        public void GetSearchPureExamine(bool loop = false)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var test = searcher.CreateQuery().NodeTypeAlias("article")
                    .And().Field("nodeName", "Popular Blogs")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long))
                    .Execute().Take(10);

                if (loop == true)
                {
                    foreach (var item in test)
                    {

                    }
                }
            }
        }
        
        #endregion
    }
}
