using EmailSendingFunctionApp.Database;
using EmailSendingFunctionApp.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EmailSendingFunctionApp.Models;

namespace EmailSendingFunctionApp.Services
{
    public class AssociateQueryServices : IAssociateQueryServices
    {
        private DbContextOptions<AppDbContext> _dbContextOptions;

        public AssociateQueryServices(DbContextOptions<AppDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
       
        public List<AssociateDetailModel> GetAssociateEmailList()
        {            
            using (var db = new AppDbContext(_dbContextOptions))
            {
                var list = db.Associates.Select(x => 
                new AssociateDetailModel {
                    AssociateName = x.FirstName,
                    AssociateId = x.Id,
                   AssociateEmail= x.Email 
                }).ToList();
                return list;
            }
           
        }

        public List<AssociateDetailModel> GetUpcomingAssociateBirthdays()
        {
            var eventDate = DateTime.UtcNow.AddDays(1);            
            using (var db = new AppDbContext(_dbContextOptions))
            {
                var associates = db.Associates
                    .Where(x =>x.DOB.Month == eventDate.Month && x.DOB.Day == eventDate.Day)
                    .Select(x => new AssociateDetailModel
                    {
                        AssociateId = x.Id,
                        AssociateName = x.FirstName,
                        AssociateEmail = x.Email
                    }).ToList();
                return associates;
            }           
        }

        public List<AssociateDetailModel> GetBirthdayPersonList()
        {            
            using (var db = new AppDbContext(_dbContextOptions))
            {
                var associates = db.Associates
                    .Where(x => x.DOB.Month == DateTimeOffset.UtcNow.Month && x.DOB.Day == DateTimeOffset.UtcNow.Day)
                    .Select(x => new AssociateDetailModel
                    {
                        AssociateId = x.Id,
                        AssociateName = x.FirstName,
                        AssociateEmail = x.Email
                    }).ToList();
                return associates;
            }
        }

        public List<MessageModel> GetBirthdayWishesByAssociateId(int associateId)
        {
            using (var db = new AppDbContext(_dbContextOptions))
            {
                var list = (from input in db.Associate_Birthday_Wishes_Inputs.AsQueryable()
                           join a in db.Associates.AsQueryable()
                           on input.AssociateId equals a.Id
                           where input.BirthdayPersonId == associateId && input.AddedDate.Year == DateTimeOffset.UtcNow.Year
                           select new { 
                               SenderId = input.AssociateId,
                               FirstName = a.FirstName,
                               LastName = a.LastName,
                               Message = input.BirthdayMessage
                           }).ToList();
               return list.Select(x => new MessageModel
                {
                    SenderId = x.SenderId,
                    SenderName = $"{x.FirstName}  {x.LastName}",
                    Message = x.Message
                }).ToList();                              
            }
        }

        public string GetValue(string key)
        {
            using (var db = new AppDbContext(_dbContextOptions))
            {
                var template = db.AppSettings
                    .Where(x => x.Key == key)
                    .Select(x => x.Value).FirstOrDefault();
                return template;
            }
        }
    }
}
