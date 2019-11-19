using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examine;
using Umbraco.Core.Models.PublishedContent;

namespace TUP.Core.Services
{
    public interface IBenchmarkService
    {
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
