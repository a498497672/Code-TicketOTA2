using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Ticket.Infrastructure.Print.Request
{
    public class PrintSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            Dictionary<string, PrintSection> names = new Dictionary<string, PrintSection>();

            string _key = string.Empty;
            string _partner = string.Empty;
            string _apiKey = string.Empty;
            string _machineCode = string.Empty;
            string _machineKey = string.Empty;

            foreach (XmlNode childNode in section.ChildNodes)
            {
                if (childNode.Attributes["key"] != null)
                {
                    _key = childNode.Attributes["key"].Value;

                    if (childNode.Attributes["partner"] != null)
                    {
                        _partner = childNode.Attributes["partner"].Value;
                    }
                    if (childNode.Attributes["apiKey"] != null)
                    {
                        _apiKey = childNode.Attributes["apiKey"].Value;
                    }
                    if (childNode.Attributes["machineCode"] != null)
                    {
                        _machineCode = childNode.Attributes["machineCode"].Value;
                    }
                    if (childNode.Attributes["machineKey"] != null)
                    {
                        _machineKey = childNode.Attributes["machineKey"].Value;
                    }
                    names.Add(_key, new PrintSection
                    {
                        Key = _key,
                        Partner = _partner,
                        ApiKey = _apiKey,
                        MachineCode = _machineCode,
                        MachineKey = _machineKey
                    });
                }
            }
            return names;
        }
    }
}
