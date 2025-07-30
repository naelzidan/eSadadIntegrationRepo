﻿using Esadad.Core.Entities;

using System.Xml;

namespace Esadad.Infrastructure.Interfaces
{
    public interface ICommonService
    {
        EsadadTransactionLog InsertLog(string transactionType, string apiName, string guid,
                                XmlElement requestElement, Object responseObject=null);

        EsadadPaymentLog InsertPaymentLog(string guid, XmlElement requestElement);

    }
}
