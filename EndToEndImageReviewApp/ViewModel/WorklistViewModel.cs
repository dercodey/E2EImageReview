using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndToEndImageReviewApp.ViewModel
{
    public class WorklistViewModel
    {
        WorklistModel model = new WorklistModel();

        public WorklistViewModel()
        {
            var task = model.PopulateWorklistForStaff(Guid.Empty);
            task.ContinueWith(PopulateWorklistItemsAsync);
        }

        void PopulateWorklistItemsAsync(Task<List<WorklistItem>> task)
        {
            Items = new ObservableCollection<WorklistItem>(task.Result);
        }

        public ObservableCollection<WorklistItem> Items { get; set; }
    }
}
