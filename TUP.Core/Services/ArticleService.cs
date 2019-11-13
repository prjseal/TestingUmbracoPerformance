using Examine;
using Examine.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Examine;
using Umbraco.Web;

namespace TUP.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public ArticleService(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public IEnumerable<ISearchResult> GetLatestArticlesUsingExamine(IPublishedContent currentContentItem)
        {
            if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out var index))
            {
                var searcher = index.GetSearcher();
                var results = searcher.CreateQuery("content").NodeTypeAlias("article")
                    .OrderByDescending(new SortableField("articleDate", SortType.Long)).Execute().Take(10);
                return results;
            }

            return Enumerable.Empty<ISearchResult>();
        }

        public IEnumerable<IPublishedContent> GetLatestArticlesUsingXPath(IPublishedContent currentContentItem)
        {
            var siteId = currentContentItem.Root().Id;

            return _umbracoContextAccessor.UmbracoContext.Content.GetByXPath("root/home[@id="+ siteId +"]/articleList/article")
                .OrderByDescending(x => x.Value<DateTime>("articleDate")).Take(10);
        }

        public IEnumerable<IPublishedContent> GetLatestArticlesUsingDescendants(IPublishedContent currentContentItem)
        {
            return currentContentItem.Root().Descendants().Where(x => x.ContentType.Alias == "article")
                .OrderByDescending(x => x.Value<DateTime>("articleDate")).Take(10);
        }
    }
}
