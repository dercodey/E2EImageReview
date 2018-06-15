using System;
using System.Windows.Input;

using Prism.Events;
using Prism.Mvvm;

using EndToEndImageReviewApp.Events;
using Prism.Commands;

namespace EndToEndImageReviewApp.ViewModels
{
    public class StaffLoginViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;

        public StaffLoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            DoStaffLogin = new DelegateCommand(DoStaffLoginCommand);
        }

        public ICommand DoStaffLogin { get; set; }

        void DoStaffLoginCommand()
        {
            _eventAggregator
                .GetEvent<StaffLoggedInEvent>()
                .Publish(
                    new StaffLoggedInEventArgs()
                    {
                        StaffId = Guid.Empty,
                        When = DateTime.Now
                    });

        }
    }
}
