using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.Client.Models;

namespace WCF.Client.Contracts
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
