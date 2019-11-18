using System;
using System.Diagnostics;
using System.Web.Mvc;
using TUP.Core.Services;
using Umbraco.Web.Mvc;

namespace TUP.Core.Controllers
{
    public class BenchmarkController : SurfaceController
    {
        private readonly IBenchmarkService _benchmarkService;

        public BenchmarkController(IBenchmarkService benchmarkService)
        {
            _benchmarkService = benchmarkService;
        }

        #region all posts

        public ActionResult AllByBadLinq(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetByBadLinq(loop));

            return Content(result);
        }

        public ActionResult AllChildrenFromNode(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllChildren(loop));

            return Content(result);
        }

        public ActionResult AllDescendantsFromRoot(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllDescendants(loop));

            return Content(result);
        }

        public ActionResult AllByXpathGreedy(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllXPathGreedy(loop));

            return Content(result);
        }

        public ActionResult AllByXpathEfficient(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllXPathEfficient(loop));

            return Content(result);
        }

        public ActionResult AllByTypedExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetTypedExamine(loop));

            return Content(result);
        }

        public ActionResult AllByPureExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetPureExamine(loop));

            return Content(result);
        }

        public ActionResult AllByContentService(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetByContentService(loop));

            return Content(result);
        }

        #endregion

        #region latest ten posts

        public ActionResult GetLatestBadLinq(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.LatestBadLinq(loop));

            return Content(result);
        }

        public ActionResult GetLatestChildrenFromNode(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.LatestChildrenFromNode(loop));

            return Content(result);
        }

        public ActionResult LatestDescendantsFromRoot(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestDescendants(loop));

            return Content(result);
        }

        public ActionResult LatestByXpathGreedy(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestXPathGreedy(loop));

            return Content(result);
        }

        public ActionResult LatestByXpathEfficient(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestXPathEfficient(loop));

            return Content(result);
        }

        public ActionResult LatestByTypedExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.LatestTypedExamine(loop));

            return Content(result);
        }

        public ActionResult LatestByPureExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestPureExamine(loop));

            return Content(result);
        }

        #endregion

        #region searches

        public ActionResult SearchWithLinq(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchLinq(loop));

            return Content(result);
        }

        public ActionResult SearchWitXsltGreedy(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchXsltGreedy(loop));

            return Content(result);
        }

        public ActionResult SearchWitXsltEfficient(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchXsltEfficient(loop));

            return Content(result);
        }

        public ActionResult SearchWithTypedExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchTypedExamine(loop));

            return Content(result);
        }

        public ActionResult SearchWithPureExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchPureExamine(loop));

            return Content(result);
        }

       public ActionResult SearchWithPureExamineGetContent(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchTypedExamine(loop));

            return Content(result);
        }

        
        #endregion


        private string GetTime(Action action)
        {
            double total = 0;
            int iterations = 10;

            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                action();

                total += stopwatch.Elapsed.TotalMilliseconds;
            }

            return (total / iterations).ToString("F4");
        }
    }
}
