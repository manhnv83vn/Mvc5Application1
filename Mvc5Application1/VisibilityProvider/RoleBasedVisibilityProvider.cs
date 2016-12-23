using Mvc5Application1.Framework.Security.Authorization;
using MvcSiteMapProvider;
using System.Collections.Generic;
using System.Linq;

namespace Mvc5Application1.VisibilityProvider
{
    public class RoleBasedVisibilityProvider : SiteMapNodeVisibilityProviderBase
    {
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            if (!node.Attributes.ContainsKey("visibility")) return true;
            var functions = node.Attributes["visibility"] as string;
            if (functions == null) return false;
            var nodeAttributes = functions.Split(',');

            return nodeAttributes.Any(Mvc5Application1Permission.CheckAccess);
        }
    }
}