﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace DesktopAppUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName = "amer@amerbecic.com";
        private string _password = "Pwd12345.";

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public void LogIn()
        {
            Console.WriteLine();
            //try
            //{
            //    ErrorMessage = "";
            //    var result = await _apiHelper.Authenticate(UserName, Password);

            //    //Capture more info about user
            //    await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

            //    _events.PublishOnUIThread(new LogOnEvent());
            //}
            //catch (Exception ex)
            //{
            //    ErrorMessage = ex.Message;
            //}
        }
    }
}