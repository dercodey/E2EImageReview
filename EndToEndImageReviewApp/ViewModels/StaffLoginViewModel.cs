using System;
using System.Windows.Input;

using Prism.Events;
using Prism.Mvvm;

using EndToEndImageReviewApp.Events;
using Prism.Commands;

namespace EndToEndImageReviewApp.ViewModels
{
    /// <summary>
    /// simple viewmodel to allow login events for a given staff
    /// </summary>
    public class StaffLoginViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        Guid _currentStaffId = Guid.NewGuid();

        public StaffLoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // set up the login delegate, to just propagate the staff login ID
            DoStaffLogin = new DelegateCommand(() =>
                _eventAggregator
                    .GetEvent<StaffLoggedInEvent>()
                    .Publish(
                        new StaffLoggedInEventArgs()
                        {
                            StaffId = _currentStaffId,
                            When = DateTime.Now
                        }));
        }

        /// <summary>
        /// command object can be bound to perform login
        /// </summary>
        public ICommand DoStaffLogin { get; set; }
    }
}
