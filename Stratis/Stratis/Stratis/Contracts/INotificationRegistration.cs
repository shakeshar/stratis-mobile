using System;
using System.Collections.Generic;
using System.Text;

namespace Stratis.Contracts
{
    public interface INotificationRegistration
    {
        string Device { get; }
        string TokenId { get; }
        IEnumerable<string> Tags { get; set; }
    }
}
