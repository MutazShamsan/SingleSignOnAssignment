using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace SSO.WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISignInService" in both code and config file together.
    [ServiceContract]
    public interface ISignInService
    {
        [OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json)]
        [WebGet]
        //Task<bool> DoWork(string username, string password);
        Task<bool> DoWork();
    }
}
