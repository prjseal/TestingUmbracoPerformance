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

        public ActionResult AllLinq(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllLinq(loop));

            return Content(result);
        }

        public ActionResult AllXPathGreedy(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllXPathGreedy(loop));

            return Content(result);
        }

        public ActionResult AllXPathEfficient(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllXPathEfficient(loop));

            return Content(result);
        }

        public ActionResult AllTypedExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllTypedExamine(loop));

            return Content(result);
        }

        public ActionResult AllPureExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetAllPureExamine(loop));

            return Content(result);
        }

        #endregion

        #region latest ten posts

        public ActionResult LatestLinq(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestLinq(loop));

            return Content(result);
        }

        public ActionResult LatestXPathGreedy(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestXPathGreedy(loop));

            return Content(result);
        }

        public ActionResult LatestXPathEfficient(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestXPathEfficient(loop));

            return Content(result);
        }

        public ActionResult LatestTypedExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestTypedExamine(loop));

            return Content(result);
        }

        public ActionResult LatestPureExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetLatestPureExamine(loop));

            return Content(result);
        }

        #endregion

        #region searches

        public ActionResult SearchLinq(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchLinq(loop));

            return Content(result);
        }

        public ActionResult SearchXPathGreedy(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchXPathGreedy(loop));

            return Content(result);
        }

        public ActionResult SearchXPathEfficient(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchXPathEfficient(loop));

            return Content(result);
        }

        public ActionResult SearchTypedExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchTypedExamine(loop));

            return Content(result);
        }

        public ActionResult SearchPureExamine(bool loop = false)
        {
            var result = GetTime(() => _benchmarkService.GetSearchPureExamine(loop));

            return Content(result);
        }

        #endregion

        private string GetTime(Action action)
        {
            double total = 0;
            int iterations = 100;

            for (int i = 0; i < iterations; i++)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                action();

                total += stopwatch.Elapsed.TotalMilliseconds;
            }

            return (total / iterations).ToString("F4");
        }

        //public ActionResult AllChildrenFromNode(bool loop = false)
        //{
        //    var result = GetTime(() => _benchmarkService.GetAllChildren(loop));

        //    return Content(result);
        //}

        //public ActionResult AllByContentService(bool loop = false)
        //{
        //    var result = GetTime(() => _benchmarkService.GetByContentService(loop));

        //    return Content(result);
        //}


        //public ActionResult GetLatestChildrenFromNode(bool loop = false)
        //{
        //    var result = GetTime(() => _benchmarkService.LatestChildrenFromNode(loop));

        //    return Content(result);
        //}
    }
}
