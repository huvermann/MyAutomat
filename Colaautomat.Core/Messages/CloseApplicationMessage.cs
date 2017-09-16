using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colaautomat.Core.Messages
{
    /// <summary>
    /// This message is fired when the Application is going down.
    /// </summary>
    public class CloseApplicationMessage : PubSubEvent
    {
    }
}
