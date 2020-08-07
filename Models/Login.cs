using System;
using Newtonsoft.Json;

namespace HotelBookingTests.Models
{
    public class Login
    {
        [JsonProperty("username")]
        public String Username;

        [JsonProperty("password")]
        public String Password;
    }
}
