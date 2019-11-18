using TUP.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace TowergatePlatform.Core.Composing
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Umbraco.Core.Composing.Composition composition)
        {
            composition.Register<IArticleService, ArticleService>(Lifetime.Request);
            composition.Register<IBenchmarkService, BenchmarkService>(Lifetime.Request);
        }
    }
}