using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pAPI_WSD_Agenda.ViewModels
{
    public class MessageItem :BaseResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public MessageItem(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public MessageItem()
        {
        }
    }
}
