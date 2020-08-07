using System;
using TechTalk.SpecFlow;
using HotelBookingTests.Drivers;
using BoDi;

namespace HotelBookingTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            this._container = container;
        }

        [BeforeScenario]
        public void LoginUser()
        {
            var _driver = new Driver("http://localhost:8080", "admin", "password123");
            _container.RegisterInstanceAs<Driver>(_driver);
        }
    }
}
