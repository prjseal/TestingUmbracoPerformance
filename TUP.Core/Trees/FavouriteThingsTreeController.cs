using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Xml;
using Umbraco.Core;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace TUP.Core.Trees
{
    [Tree("content", "fasterContentTree", TreeTitle = "Faster Content", TreeGroup = "fasterContentGroup", SortOrder = 50)]
    public class FasterContentTreeController : TreeController
    {

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            // check if we're rendering the root node's children
            var nodes = new TreeNodeCollection();
            if (id == Constants.System.Root.ToInvariantString())
            {
                var rootNodes = UmbracoContext.Content.GetAtRoot();

                // create our node collection

                foreach (var item in rootNodes)
                {
                    bool hasChildren = item.Children != null;
                    var node = CreateTreeNode(item.Id.ToString(), "-1", queryStrings, item.Name, "icon-presentation", hasChildren);
                    node.AdditionalData.Add("actionEdit", "/views/content/edit.html");
                    nodes.Add(node);

                }
            }
            else if (id == 1093.ToString())
            {
                var parent = UmbracoContext.Content.GetAtRoot().FirstOrDefault(x => x.Id.ToString() == id);
                var children = parent.Children;
                foreach (var child in children)
                {
                    var nodeChild = CreateTreeNode(child.Id.ToString(), id, queryStrings, child.Name, "icon-presentation", false);
                    nodes.Add(nodeChild);
                }
            }
            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu item actions or custom ActionMenuItems
                menu.Items.Add(new CreateChildEntity(Services.TextService));
                // add refresh menu item (note no dialog)
                menu.Items.Add(new RefreshNode(Services.TextService, true));
                return menu;
            }
            // add a delete action to each individual item
            menu.Items.Add<ActionDelete>(Services.TextService, true, opensDialog: true);

            return menu;
        }

        protected override TreeNode CreateRootNode(FormDataCollection queryStrings)
        {
            var root = base.CreateRootNode(queryStrings);

            // set the icon
            root.Icon = "icon-hearts";
            // could be set to false for a custom tree with a single node.
            root.HasChildren = true;
            //url for menu
            root.MenuUrl = null;

            return root;
        }
    }
}
