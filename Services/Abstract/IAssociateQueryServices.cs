using EmailSendingFunctionApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSendingFunctionApp.Services.Abstract
{
    public interface IAssociateQueryServices
    {
        List<AssociateDetailModel> GetAssociateEmailList();
        List<AssociateDetailModel> GetUpcomingAssociateBirthdays();
        List<AssociateDetailModel> GetBirthdayPersonList();

        List<MessageModel> GetBirthdayWishesByAssociateId(int associateId);
        string GetValue(string key);
    }
}
