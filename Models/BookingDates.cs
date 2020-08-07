using System;
using Newtonsoft.Json;

namespace HotelBookingTests.Models
{
    public class BookingDates
    {
        [JsonProperty("checkin")]
        public String CheckIn;

        [JsonProperty("checkout")]
        public String CheckOut;
    }
}
