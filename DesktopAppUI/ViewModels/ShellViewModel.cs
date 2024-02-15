using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using DesktopAppUI.EventModels;
using DesktopAppUI.Library.Api;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SimpleContainer _container;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;
        public ShellViewModel(IEventAggregator events, LoginViewModel loginVM, SimpleContainer container, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _container = container;
            _user = user;
            _apiHelper = apiHelper;

            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public bool IsUserLoggedIn
        {
            get
            {
                bool output = false;

                if (!string.IsNullOrWhiteSpace(_user.Token))
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserViewModel>(), new CancellationToken());
        }

        public async Task LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsUserLoggedIn);
        }

        public void  ExitApplication()
        {
             TryCloseAsync();
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsUserLoggedIn);
        }
    }
}
