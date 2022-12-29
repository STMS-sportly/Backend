using Data.DataAccess;
using Data.DTOs;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly StmsContext chatContext;

        public ChatRepository(StmsContext teamContext)
        {
            this.chatContext = teamContext;
        }
        #region Save Region
        public void Save()
        {
            chatContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await chatContext.SaveChangesAsync();
        }
        #endregion

        #region IChatRepository
        public async Task<bool> SendMessage(string email, int teamId, string message)
        {
            try
            {
                var user = chatContext.Users.Where(e => e.Email == email).First();
                var newMessage = new GroupChat()
                {
                    Message = message,
                    TeamId = teamId,
                    UserId = user.UserId,
                    SendDate = DateTime.Now,
                };
                chatContext.GroupChats.Add(newMessage);
                await SaveAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<GetChatMessagesDTO> GetMessages(string email, int teamId)
        {
            try
            {
                var result = new GetChatMessagesDTO();
                result.Messages = await (from t in chatContext.GroupChats
                          join t2 in chatContext.Users on t.UserId equals t2.UserId
                          orderby t.SendDate
                          select new MessageDTO
                          {
                              MessageId = t.MessageId,
                              Message = t.Message,
                              FirstName = t2.Firstname,
                              LastName = t2.Surname,
                              MessageTime = t.SendDate
                          }).ToListAsync<MessageDTO>();
                return result;
            }
            catch
            {
                throw new Exception("Problem with get Messages");
            }
        }
        #endregion
    }
}
