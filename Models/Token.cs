using System;
using Newtonsoft.Json;

namespace HotelBookingTests.Models
{
    public class Token
    {
        [JsonProperty("token")]
        public String AccessToken;
    }
}
