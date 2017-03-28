using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Sitefinity.Sample.LeadScoreCriteria.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILeadScoring" in both code and config file together.
    [ServiceContract]
    public interface ILeadScoring
    {
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetLeadScoringTypes();
    }
}
