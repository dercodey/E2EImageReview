using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;

using Prism.Events;
using Prism.Mvvm;
using Prism.Commands;

using EndToEndImageReviewApp.Events;

namespace EndToEndImageReviewApp.ViewModels
{
    /// <summary>
    /// ViewModel representing a worklist for the current logged on staff
    /// Responds to StaffLoginEvents to trigger re-population
    /// Can also be triggered to refresh via RefreshWorklistCommand
    /// </summary>
    public class WorklistViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        Guid _currentStaffId;
        WorklistModel model = new WorklistModel();

        /// <summary>
        /// construct the view model
        /// create the command objects and subscribe to events
        /// </summary>
        /// <param name="eventAggregator">the event aggregator to use (from the UnityContainer)</param>
        public WorklistViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // create the delegate for the refresh command
            RefreshWorklistCommand = 
                new DelegateCommand(() =>
                    model.PopulateWorklistForStaff(_currentStaffId)
                        .ContinueWith(PopulateItemsFromResult));

            // subscribe to the staff login event
            _eventAggregator
                .GetEvent<StaffLoggedInEvent>()
                .Subscribe(args =>
                {
                    _currentStaffId = args.StaffId;
                    model.PopulateWorklistForStaff(_currentStaffId)
                        .ContinueWith(PopulateItemsFromResult);
                });            
        }

        /// <summary>
        /// command object for performing worklist refresh
        /// </summary>
        public ICommand RefreshWorklistCommand { get; private set; }

        /// <summary>
        /// populates the worklist items collection with the result from the model
        /// </summary>
        /// <param name="task">task that populates the items</param>
        void PopulateItemsFromResult(Task<List<WorklistItem>> task)
        {
            _itemsSource = new ObservableCollection<WorklistItem>(task.Result);
            WorklistItems = CollectionViewSource.GetDefaultView(_itemsSource);
        }

        /// <summary>
        /// collection of worklist items to be displayed
        /// </summary>
        public ICollectionView WorklistItems
        {
            get => _items;
            set
            {
                if (_items != null)
                    _items.CurrentChanged -= OnSelectedWorklistItem;

                SetProperty(ref _items, value);
                _items.CurrentChanged += OnSelectedWorklistItem;
            }
        }
        ICollectionView _items;
        ObservableCollection<WorklistItem> _itemsSource;

        /// <summary>
        /// responds to local current item changed event for worklist item collection
        /// sends a WorklistItemSelectedEvent when this happens
        /// </summary>
        /// <param name="sender">n/a</param>
        /// <param name="e">n/a</param>
        private void OnSelectedWorklistItem(object sender, EventArgs e)
        {
            var worklistItem = (WorklistItem)WorklistItems.CurrentItem;
            if (worklistItem == null)
                return;

            _eventAggregator
                .GetEvent<WorklistItemSelectedEvent>()
                .Publish(
                    new WorklistItemSelectedEventArgs()
                    {
                        ImageId = worklistItem.ImageId,
                        AcquisitionDateTime = worklistItem.AcquisitionDateTime,
                    });
        }

    }
}
