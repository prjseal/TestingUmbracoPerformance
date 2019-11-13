using Examine;
using System;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Examine.Providers;

namespace TUP.Core.Composing
{
    public class IndexerComponent : IComponent
    {
        private readonly IExamineManager examineManager;
        private readonly IUmbracoContextFactory umbracoContextFactory;
        private readonly ILogger logger;

        public IndexerComponent(IExamineManager examineManager,
            IUmbracoContextFactory umbracoContextFactory,
            ILogger logger)
        {
            this.examineManager = examineManager ?? throw new ArgumentNullException(nameof(examineManager));
            this.umbracoContextFactory = umbracoContextFactory ?? throw new ArgumentNullException(nameof(umbracoContextFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize()
        {
            if (examineManager.TryGetIndex("ExternalIndex", out IIndex externalIndex))
            {
                externalIndex.FieldDefinitionCollection.AddOrUpdate(
                    new FieldDefinition("articleDate", FieldDefinitionTypes.Long));

                ((BaseIndexProvider)externalIndex).TransformingIndexValues +=
                    IndexerComponent_TransformingIndexValues;
            }
        }

        private void IndexerComponent_TransformingIndexValues(object sender, IndexingItemEventArgs e)
        {
            if (int.TryParse(e.ValueSet.Id, out var nodeId))
                switch (e.ValueSet.ItemType)
                {
                    case "article":
                        using (var umbracoContext = umbracoContextFactory.EnsureUmbracoContext())
                        {
                            var contentNode = umbracoContext.UmbracoContext.Content.GetById(nodeId);
                            if (contentNode != null)
                            {
                                var articleDate = contentNode.Value<DateTime>("articleDate");
                                e.ValueSet.Set("articleDate", articleDate.Date.Ticks);
                            }
                        }
                        break;
                }
        }

        public void Terminate() { }
    }

    public class RegisterIndexerComponentComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<IndexerComponent>();
        }
    }
}