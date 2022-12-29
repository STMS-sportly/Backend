using Data.DTOs;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IChatRepository
    {
        Task<GetChatMessagesDTO> GetMessages(string email, int teamId);
        Task<bool> SendMessage(string email, int teamId, string message);
    }
}
