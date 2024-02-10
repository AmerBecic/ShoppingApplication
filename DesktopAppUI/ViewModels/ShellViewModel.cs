using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DesktopAppUI.EventModels;

namespace DesktopAppUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private SimpleContainer _container;
        public ShellViewModel(IEventAggregator events, LoginViewModel loginVM, SalesViewModel salesVM, SimpleContainer container)
        {
            _salesVM = salesVM;
            _events = events;
            _container = container;

            _events.Subscribe(this);

            //ActivateItem(IoC.Get<LoginViewModel>());
            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            //NotifyOfPropertyChange(() => IsUserLoggedIn);
        }
    }
}
