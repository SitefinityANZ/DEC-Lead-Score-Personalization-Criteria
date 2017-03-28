using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Newtonsoft.Json.Linq;
using Telerik.DigitalExperienceCloud.Client;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DataIntelligenceConnector.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Services;

namespace Sitefinity.Sample.LeadScoreCriteria.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LeadScoring" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class LeadScoring : ILeadScoring
    {
       
        public string GetLeadScoringTypes()
        {

            try
            {
                var token =  new AccessToken(
                    SecurityManager.DecryptData(Config.Get<DigitalExperienceCloudConnectorConfig>().UserName), 
                    SecurityManager.DecryptData(Config.Get<DigitalExperienceCloudConnectorConfig>().Password));

                var authHeader = token.GetAuthorizationHeader();
                var address = "https://api.dec.sitefinity.com/analytics/v1/scorings/leads?format=json";
                using (WebClient client = new WebClient())
                {
                    var currentSite = new Telerik.Sitefinity.Multisite.MultisiteContext().CurrentSite;

                    client.Encoding = Encoding.UTF8;
                    client.Headers.Add("Authorization: " + authHeader);
                    client.Headers.Add("x-dataintelligence-datacenterkey: " + Config.Get<DigitalExperienceCloudConnectorConfig>().SiteToApiKeyMappings[currentSite.Name].DataCenterApiKey);//SecurityManager.DecryptData

                    return client.DownloadString(address);
                }

            }
            catch (Exception exception)
            {
                return "";
            }
        }
    }
}
