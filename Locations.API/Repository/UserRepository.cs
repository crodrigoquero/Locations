using AutoMapper;
using Locations.API.Data;
using Locations.API.Models;
using Locations.API.Models.DTOs;
using Locations.API.Repository.iRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Locations.API.Helpers;

namespace Locations.API.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateUser(Trail trail)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Trail trail)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(int userId)
        {
            //throw new NotImplementedException();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "telnet");



            User user = new User();

            try
            {
                var stringTask = client.GetStringAsync("https://bpdts-test-app.herokuapp.com/user/" + userId);

                var msg = await stringTask;
                dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(msg);

                user.Id = x.id;
                user.first_name = x.first_name;
                user.last_name = x.last_name;
                user.ip_address = x.ip_address;
                user.email = x.email;
                user.ip_address = x.ip_address;
                user.city = x.city;
                user.latitude = x.latitude;
                user.longitude = x.longitude;
            }
            catch
            {
                return null;
            }


            return user;


        }

        public async Task<ICollection<User>> GetUsers()
        {
            //throw new NotImplementedException();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "telnet");

            List<User> usersList = new List<User>();
            int numberOfIndividualsFormAPI;

            try
            {
                var stringTask = client.GetStringAsync("https://bpdts-test-app.herokuapp.com/users");

                var msg = await stringTask;
                dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(msg);

                numberOfIndividualsFormAPI = x.Count;
 
                for (int index = 0; index <= numberOfIndividualsFormAPI -1; index++){

                    var user = new User();

                    user.Id = x[index].id;
                    user.first_name = x[index].first_name;
                    user.last_name = x[index].last_name;
                    user.ip_address = x[index].ip_address;
                    user.email = x[index].email;
                    user.ip_address = x[index].ip_address;
                    user.city = x[index].city;

                    //if (user.city == null)
                    //{
                    //    UserDto userDto = await GetCityFromIpAddress(user.ip_address); // geo location by IP; by using another API

                    //    if (userDto != null) 
                    //    {
                    //        user.city = userDto.city;
                    //        user.Country = userDto.Country;
                    //        user.ZipCode = userDto.ZipCode;
                    //        user.Area = userDto.Area;
                    //    }

                    //}

                    user.latitude = x[index].latitude;
                    user.longitude = x[index].longitude;


                    // Once we knew the user precise location (by IP) we can calculate distance to London
                    //if (user.Country == "GB")
                    //{
                    //  // Calculate/update distance here
                    //}

                    // first two params are LONDON lat and long
                      user.DistanceFromLondon = Math.Round( DistanceAlgorithm.distance(51.506307, -0.132727, user.latitude, user.longitude, 'M'),0);

                    usersList.Add(user);
                };

            }
            catch 
            {
                return null;
            }

            return usersList;
        }

        public async Task<ICollection<User>> GetUsersFromLondon()
        {
            //throw new NotImplementedException();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "telnet");

            List<User> usersList = new List<User>();
            int numberOfIndividualsFormAPI;
              
            try
            {
                var stringTask = client.GetStringAsync("https://bpdts-test-app.herokuapp.com/users");

                var msg = await stringTask;
                dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(msg);

                numberOfIndividualsFormAPI = x.Count;

                for (int index = 0; index <= numberOfIndividualsFormAPI - 1; index++)
                {
                       
                    var user = new User();
                     
                    user.Id = x[index].id;
                    user.first_name = x[index].first_name;
                    user.last_name = x[index].last_name;
                    user.ip_address = x[index].ip_address;
                    user.email = x[index].email;
                    user.ip_address = x[index].ip_address;
                    user.city = x[index].city;
                    user.latitude = x[index].latitude;
                    user.longitude = x[index].longitude;


                    user.DistanceFromLondon = Math.Round(DistanceAlgorithm.distance(51.506307, -0.132727, user.latitude, user.longitude, 'M'), 0);

                    usersList.Add(user);
                };

            }
            catch (Exception e)
            {
                var excep = e.Message;
                return null;
            }

            // filter user unti 50 miles
            var londonDiameterAverage = 80; // in miles
            List<User> usersListFiltered = usersList.Where(c => c.DistanceFromLondon <= (50+londonDiameterAverage)).ToList();

            return usersListFiltered;
        }


        public async Task<UserDto> GetCityFromIpAddress(string ipAddress)
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "carlos");

            try
            {
                var stringTask = client.GetStringAsync("http://api.ipstack.com/" + ipAddress + "?access_key=999d9e81048f1e2e1497cf272ffbd7f9");

                var msg = await stringTask;
                UserDto user = new UserDto();

                dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(msg);
                user.city = x.city;
                user.Country = x.country_code;
                user.ZipCode = x.zip;
                user.Area = x.region_name;
                return user;
            }
            catch
            {
                return null;
            }
        }


        public ICollection<User> GetUsersFromCity(string city)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(Trail trail)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(string name)
        {
            throw new NotImplementedException();
        }

        public bool UserExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}
