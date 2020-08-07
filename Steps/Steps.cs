using System;
using System.Net;
using TechTalk.SpecFlow;
using HotelBookingTests.Drivers;
using HotelBookingTests.Models;
using Xunit;

namespace HotelBookingTests.StepDefinitions
{
    [Binding]
    public class Steps
    {
        private readonly Driver _driver;

        public Steps(Driver driver)
        {
            _driver = driver;
        }

        [Given(@"a user wants to make a booking with the following details")]
        public void GivenAUserWantsToMakeABookingWithTheFollowingDetails(Table table)
        {
            var row = table.Rows[0];

            var booking = new Booking
            {
                FirstName = row["firstname"],
                LastName = row["lastname"],
                DepositPaid = row["paid"] == "true",
                TotalPrice = Int32.Parse(row["price"]),
                Dates = new BookingDates
                {
                    CheckIn = row["from"],
                    CheckOut = row["to"]
                },
                AdditionalNeeds = row["needs"]
            };

            _driver.NextBooking = booking;
        }

        [When(@"the booking is submitted by the user")]
        public void WhenTheBookingIsSubmittedByTheUser()
        {
            _driver.PostBooking();
        }

        [Then(@"the booking is successfully stored")]
        public void ThenTheBookingIsSuccessfullyStored()
        {
            Assert.Equal(_driver.Response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"shown to the user as stored")]
        public void ThenShownToTheUserAsStored()
        {
            Assert.True(_driver.LastBookingId.Id >= 1);
        }

        [Given(@"Hotel Booking has existing bookings")]
        public void GivenHotelBookingHasExistingBookings()
        {
            var booking = new Booking
            {
                FirstName = "Rose",
                LastName = "Boylu",
                DepositPaid = true,
                TotalPrice = 10,
                Dates = new BookingDates
                {
                    CheckIn = "2020-07-24",
                    CheckOut = "2020-07-25"
                },
                AdditionalNeeds = "Nothing"
            };

            _driver.NextBooking = booking;

            _driver.PostBooking();
            Assert.Equal(_driver.Response.StatusCode, HttpStatusCode.OK);
        }

        [When(@"a specific booking is requested by the user")]
        public void WhenASpecificBookingIsRequestedByTheUser()
        {
            _driver.GetBooking(_driver.LastBookingId.Id);
        }

        [Then(@"the booking is shown")]
        public void ThenTheBookingIsShown()
        {
            var booking = _driver.LastBooking;
            Assert.Equal(booking.FirstName, "Rose");
            Assert.Equal(booking.LastName, "Boylu");
            Assert.Equal(booking.DepositPaid, true);
            Assert.Equal(booking.TotalPrice, 10);
            Assert.Equal(booking.Dates.CheckIn, "2020-07-24");
            Assert.Equal(booking.Dates.CheckOut, "2020-07-25");
            Assert.Equal(booking.AdditionalNeeds, "Nothing");
        }

        [When(@"a specific booking is updated by the user")]
        public void WhenASpecificBookingIsUpdatedByTheUser()
        {
            var booking = new Booking
            {
                FirstName = "Matus",
                LastName = "Novak",
                DepositPaid = true,
                TotalPrice = 10,
                Dates = new BookingDates
                {
                    CheckIn = "2020-07-24",
                    CheckOut = "2020-07-25"
                },
                AdditionalNeeds = "Nothing"
            };

            _driver.NextBooking = booking;

            _driver.UpdateBooking(_driver.LastBookingId.Id);
            Assert.Equal(_driver.Response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"the booking is shown to be updated")]
        public void ThenTheBookingIsShownToBeUpdated()
        {
            var booking = _driver.LastBooking;
            Assert.Equal(booking.FirstName, "Matus");
            Assert.Equal(booking.LastName, "Novak");
        }

        [When(@"a specific booking is deleted by the user")]
        public void WhenASpecificBookingIsDeletedByTheUser()
        {
            _driver.DeleteBooking(_driver.LastBookingId.Id);
            Assert.Equal(_driver.Response.StatusCode, HttpStatusCode.OK);
        }

        [Then(@"the booking is removed")]
        public void ThenTheBookingIsRemoved()
        {
            _driver.GetBooking(_driver.LastBookingId.Id);
            Assert.Equal(_driver.Response.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
