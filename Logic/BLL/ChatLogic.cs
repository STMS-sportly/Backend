using Data.DataAccess;
using Data.DTOs;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BLL
{
    public class ChatLogic
    {
        private readonly IChatRepository chatRepo;

        public ChatLogic(StmsContext context)
        {
            chatRepo = new UnitOfWork(context).Chat;
        }

        public async Task<bool> SendMessage(string email, int teamId, string message)
        {
            return await chatRepo.SendMessage(email, teamId, message);
        }

        public async Task<GetChatMessagesDTO> GetMessages(string email, int teamId)
        {
            return await chatRepo.GetMessages(email, teamId);
        }
    }
}
