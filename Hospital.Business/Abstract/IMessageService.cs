using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesOfChatById(string chatId);
        Task AddMessageAsync(Message message);
        Task<Message> GetLastMessageOfChatAsync(Chat chat);
    }
}
