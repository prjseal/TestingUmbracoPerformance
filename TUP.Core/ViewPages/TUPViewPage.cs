using TUP.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using Current = Umbraco.Web.Composing.Current;

namespace TUP.Core.ViewPages
{
    public abstract class TUPViewPage<T> : UmbracoViewPage<T>
    {
        public readonly IArticleService ArticleService;
        public readonly IBenchmarkService BenchmarkService;

        public TUPViewPage() : this(
                Current.Factory.GetInstance<IArticleService>(),
                Current.Factory.GetInstance<IBenchmarkService>(),
                Current.Factory.GetInstance<ServiceContext>(),
                Current.Factory.GetInstance<AppCaches>()
                )
        { }

        public TUPViewPage(IArticleService articleService, IBenchmarkService benchmarkService, 
            ServiceContext services, AppCaches appCaches)
        {
            ArticleService = articleService;
            BenchmarkService = benchmarkService;
            Services = services;
            AppCaches = appCaches;
        }
    }

    public abstract class TUPViewPage : UmbracoViewPage
    {
        public readonly IArticleService ArticleService;
        public readonly IBenchmarkService BenchmarkService;

        public TUPViewPage() : this(
                Current.Factory.GetInstance<IArticleService>(),
                Current.Factory.GetInstance<IBenchmarkService>(),
                Current.Factory.GetInstance<ServiceContext>(),
                Current.Factory.GetInstance<AppCaches>()
                )
        { }

        public TUPViewPage(IArticleService articleService, IBenchmarkService benchmarkService,
            ServiceContext services, AppCaches appCaches)
        {
            ArticleService = articleService;
            BenchmarkService = benchmarkService;
            Services = services;
            AppCaches = appCaches;
        }
    }
}