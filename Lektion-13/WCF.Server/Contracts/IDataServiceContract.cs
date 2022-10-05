using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF.Server.Models;

namespace WCF.Server.Contracts
{
    [ServiceContract]
    internal interface IDataServiceContract
    {
        [OperationContract]
        string[] GetStrings();

        [OperationContract]
        Message[] GetMessages();

        [OperationContract]
        string GetMessagesAsJson();

        [OperationContract]
        string PostMessage(Message message);

    }
}
