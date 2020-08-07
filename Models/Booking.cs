using System;
using Newtonsoft.Json;

namespace HotelBookingTests.Models
{
    public class Booking
    {
        [JsonProperty("firstname")]
        public String FirstName;

        [JsonProperty("lastname")]
        public String LastName;

        [JsonProperty("totalprice")]
        public int TotalPrice;

        [JsonProperty("depositpaid")]
        public bool DepositPaid;

        [JsonProperty("bookingdates")]
        public BookingDates Dates;

        [JsonProperty("additionalneeds")]
        public String AdditionalNeeds;
    }
}
