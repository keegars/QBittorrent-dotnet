using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qbittorrent_dotnet.Helpers.Attributes
{
    /// <summary>
    /// Marks a property to be ignored when building a form for POST requests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class FormIgnoreAttribute : Attribute
    {
    }
}
