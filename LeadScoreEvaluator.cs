using System;
using System.Linq;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DataIntelligenceConnector.Configuration;
using Telerik.Sitefinity.Personalization;

namespace Sitefinity.Sample.LeadScoreCriteria
{
    public class LeadScoreEvaluator : ICriterionEvaluator
    {
        public bool IsMatch(string settings, IPersonalizationTestContext testContext)
        {
            var decCookie = HttpContext.Current.Request.Cookies["sf-data-intell-subject"];
            if (decCookie != null)
            {
                //Retrieve the tracking cookie ID from the request
                var dataIntelligenceSubject = HttpContext.Current.Request.Cookies["sf-data-intell-subject"].Value;
                var address = "https://api.dec.sitefinity.com/analytics/v1/scorings/leads/in";
                using (WebClient client = new WebClient())
                {
                    var currentSite = new Telerik.Sitefinity.Multisite.MultisiteContext().CurrentSite;

                    client.Headers.Add("x-dataintelligence-datacenterkey: " + Config.Get<DigitalExperienceCloudConnectorConfig>().SiteToApiKeyMappings[currentSite.Name].DataCenterApiKey);
                    client.Headers.Add("x-dataintelligence-datasource: " + Config.Get<DigitalExperienceCloudConnectorConfig>().ApplicationName);
                    client.Headers.Add("x-dataintelligence-ids: " + settings.Split(new string[] { "--" }, StringSplitOptions.None)[0]);
                    client.Headers.Add("x-dataintelligence-subject: " + dataIntelligenceSubject);
                    client.Headers.Add("Authorization: appauth 04C79B5D-3146-7AD2-3FCA-C6966A00C2A2");
                    client.Headers.Add("x-dataintelligence-contacts:{\"" + Config.Get<DigitalExperienceCloudConnectorConfig>().ApplicationName + "\":[\"" + dataIntelligenceSubject + "\"]}");
                    try
                    {
                        var leads = client.DownloadString(address);
                        return leads.Contains("\"LevelId\":" + settings.Split(new string[] { "--" }, StringSplitOptions.None)[1] + ",\"PassedOn") ? true : false;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            else {
                return false;
            }
            
        }
 
    }
}
