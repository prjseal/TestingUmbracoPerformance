using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUP.Core.Services
{
    public interface IBenchmarkService
    {
        #region all posts

        void GetByBadLinq(bool loop = false);

        void GetAllChildren(bool loop = false);

        void GetAllDescendants(bool loop = false);

        void GetAllXPathGreedy(bool loop = false);

        void GetAllXPathEfficient(bool loop = false);

        void GetTypedExamine(bool loop = false);

        void GetPureExamine(bool loop = false);

        void GetByContentService(bool loop = false);

        #endregion

        #region latest ten posts

        void LatestBadLinq(bool loop = false);

        void LatestChildrenFromNode(bool loop = false);

        void GetLatestDescendants(bool loop = false);

        void GetLatestXPathGreedy(bool loop = false);

        void GetLatestXPathEfficient(bool loop = false);

        void LatestTypedExamine(bool loop = false);

        void GetLatestPureExamine(bool loop = false);

        #endregion

        #region searches

        void GetSearchLinq(bool loop = false);

        void GetSearchXsltGreedy(bool loop = false);

        void GetSearchXsltEfficient(bool loop = false);

        void GetSearchTypedExamine(bool loop = false);

        void GetSearchPureExamine(bool loop = false);

        #endregion
    }
}
