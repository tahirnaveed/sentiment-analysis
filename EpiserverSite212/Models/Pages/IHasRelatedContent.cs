using EPiServer.Core;

namespace EpiserverSite212.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
