using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pAPI_WSD_Agenda.ViewModels
{
    public class GetTokenRequest : BaseResponse
    {
        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("grant_type")]
        public String grant_type { get; set; }

        public GetTokenRequest()
        {

        }

    }
}
