using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataManager.Models
{
    //Made just when passing userId RoleName to Add or Remove role, so these values are not in Uri of post methods
    public class UserRolePairModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}