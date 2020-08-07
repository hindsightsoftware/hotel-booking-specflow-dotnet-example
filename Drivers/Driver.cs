using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using HotelBookingTests.Models;

namespace HotelBookingTests.Drivers
{
    public class Driver
    {
        private readonly String _baseUrl;
        private readonly String _token;
        private HttpContent _content;
        private HttpResponseMessage _response;
        private Booking _nextBooking;
        private BookingId _lastBookingId;
        private Booking _lastBooking;

        public Driver(String baseUrl, String username, String password)
        {
            _baseUrl = baseUrl;
            _token = this.AccessToken(username, password);
            _content = null;
            _response = null;
            _nextBooking = null;
            _lastBookingId = null;
            _lastBooking = null;
        }

        public HttpResponseMessage Response
        {
            get
            {
                return _response;
            }
        }

        public Booking NextBooking
        {
            get
            {
                return _nextBooking;
            }
            set
            {
                _nextBooking = value;
            }
        }

        public BookingId LastBookingId
        {
            get
            {
                return _lastBookingId;
            }
        }

        public Booking LastBooking
        {
            get
            {
                return _lastBooking;
            }
        }

        public void PostBooking()
        {
            _response = Post("/api/booking", JsonSerialize(_nextBooking), true);
            if (_response.StatusCode == HttpStatusCode.OK)
            {
                _lastBookingId = JsonDeserialize<BookingId>(_response);
            }
        }

        public void GetBooking(int id)
        {
            _response = Get($"/api/booking/{id}", true);
            if (_response.StatusCode == HttpStatusCode.OK)
            {
                _lastBooking = JsonDeserialize<Booking>(_response);
            }
        }

        public void UpdateBooking(int id)
        {
            _response = Put($"/api/booking/{id}", JsonSerialize(_nextBooking), true);
            if (_response.StatusCode == HttpStatusCode.OK)
            {
                _lastBooking = JsonDeserialize<Booking>(_response);
            }
        }

        public void DeleteBooking(int id)
        {
            _response = Delete($"/api/booking/{id}", true);
        }

        private String AccessToken(String username, String password)
        {
            var body = new Login { Username = username, Password = password };
            var response = Post("/login", JsonSerialize(body), false);
            return JsonDeserialize<Token>(response).AccessToken;
        }

        private HttpResponseMessage Post(String uri, HttpContent content, bool auth)
        {
            return Request(uri, HttpMethod.Post, content, auth);
        }

        private HttpResponseMessage Put(String uri, HttpContent content, bool auth)
        {
            return Request(uri, HttpMethod.Put, content, auth);
        }

        private HttpResponseMessage Get(String uri, bool auth)
        {
            return Request(uri, HttpMethod.Get, null, auth);
        }

        private HttpResponseMessage Delete(String uri, bool auth)
        {
            return Request(uri, HttpMethod.Delete, null, auth);
        }

        private HttpResponseMessage Request(String uri, HttpMethod method, HttpContent content, bool auth)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage(method, $"{_baseUrl}{uri}");
            request.Content = content;
            request.Headers.Add("Accept", "application/json");
            if (auth)
            {
                request.Headers.Add("Authorization", $"Bearer {_token}");
            }

            return client.SendAsync(request).Result;
        }

        private HttpContent JsonSerialize<T>(T model)
        {
            var jsonString = JsonConvert.SerializeObject(model);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        private T JsonDeserialize<T>(HttpResponseMessage response)
        {
            var jsonString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
