using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.Library.Api
{
    public class UserApi : IUserApi
    {
        private readonly IAPIHelper _apiHelper;

        public UserApi(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ApplicationUserModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ApplicationUserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //public async Task<Dictionary<string, string>> GetAllRoles()
        //{
        //    using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllRoles"))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}

        //public async Task AddRoleToUser(string userId, string roleName)
        //{
        //    var data = new { userId = userId, roleName = roleName };

        //    using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/AddRoleToUser", data))
        //    {
        //        if (response.IsSuccessStatusCode == false)
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}

        //public async Task RemoveRoleFromUser(string userId, string roleName)
        //{
        //    var data = new { userId = userId, roleName = roleName };

        //    using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/RemoveRoleFromUser", data))
        //    {
        //        if (response.IsSuccessStatusCode == false)
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}
    }
}
