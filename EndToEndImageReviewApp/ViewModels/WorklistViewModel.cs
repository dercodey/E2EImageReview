using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;

using Prism.Events;
using Prism.Mvvm;

using EndToEndImageReviewApp.Events;
using System;
using System.ComponentModel;

namespace EndToEndImageReviewApp.ViewModels
{
    public class WorklistViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        WorklistModel model = new WorklistModel();

        public WorklistViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator
                .GetEvent<StaffLoggedInEvent>()
                .Subscribe(args => model.PopulateWorklistForStaff(args.StaffId)
                                        .ContinueWith(PopulateItemsFromResult));            
        }

        void PopulateItemsFromResult(Task<List<WorklistItem>> task)
        {
            _itemsSource = new ObservableCollection<WorklistItem>(task.Result);
            Items = CollectionViewSource.GetDefaultView(_itemsSource);
        }

        private void OnSelectedWorklistItem(object sender, EventArgs e)
        {
            var worklistItem = (WorklistItem)Items.CurrentItem;

            _eventAggregator
                .GetEvent<WorklistItemSelectedEvent>()
                .Publish(
                    new WorklistItemSelectedEventArgs()
                    {
                        ImageId = worklistItem.ImageId
                    });
        }

        public ICollectionView Items
        {
            get => _items;
            set
            {
                if (_items != null)
                {
                    _items.CurrentChanged -= OnSelectedWorklistItem;
                }

                SetProperty(ref _items, value);
                _items.CurrentChanged += OnSelectedWorklistItem;
            }
        }
        ICollectionView _items;
        ObservableCollection<WorklistItem> _itemsSource;
    }
}
