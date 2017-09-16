using Colaautomat.Core.Models;
using Prism.Events;

namespace Colaautomat.Core.Messages
{
    /// <summary>
    /// This event is fired when the product has been delivered.
    /// </summary>
    public class ProductDeliveredMessage : PubSubEvent<IProduct>
    {
    }
}
