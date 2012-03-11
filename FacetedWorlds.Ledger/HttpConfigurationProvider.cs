using UpdateControls.Correspondence.BinaryHTTPClient;

namespace FacetedWorlds.Ledger
{
    public class HttpConfigurationProvider : IHTTPConfigurationProvider
    {
        public HTTPConfiguration Configuration
        {
            get
            {
                string endpoint = "http://localhost:8080/correspondence_server_web/bin";
                string channelName = "FacetedWorlds.Ledger";
                string apiKey = "0672C04A6459422FA87BD8F8843C1206";
                return new HTTPConfiguration(endpoint, channelName, apiKey);
            }
        }
    }
}
