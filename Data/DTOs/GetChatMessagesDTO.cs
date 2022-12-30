using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class GetChatMessagesDTO
    {
        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }

    public class MessageDTO
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
