using System;
using System.Windows.Input;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using EndToEndImageReviewApp.Events;
using System.Text;
using EndToEndImageReviewApp.ImageReviewManagerService;

namespace EndToEndImageReviewApp.ViewModels
{
    /// <summary>
    /// view model to represent the image viewer
    /// </summary>
    public class ImageViewerViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        TaskScheduler _uiScheduler;

        /// <summary>
        /// construct the viewer view model, creating commands and subscribing to events
        /// </summary>
        /// <param name="eventAggregator"></param>
        public ImageViewerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // delegate to perform a load image
            LoadImageCommand = 
                new DelegateCommand<LoadImageCommandArgs>(LoadImage);

            // subscribe to the item selected event, to perform a load image
            _eventAggregator
                .GetEvent<WorklistItemSelectedEvent>()
                .Subscribe(eventArgs =>
                    LoadImageCommand.Execute(
                        new LoadImageCommandArgs()
                        {
                            ImageId = eventArgs.ImageId,
                            AcquisitionDateTime = eventArgs.AcquisitionDateTime,
                        }));

            // capture UI scheduler for later continuations on UI thread
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        /// <summary>
        /// text describing image that can be bound for display
        /// </summary>
        public string ImageInfoText
        {
            get => _imageInfoText;
            set => SetProperty(ref _imageInfoText, value);
        }
        string _imageInfoText;

        public string ImagePixelText { get; set; }

        /// <summary>
        /// the load image command can be bound to a button (but needs parameters)
        /// </summary>
        public ICommand LoadImageCommand { get; set; }

        /// <summary>
        /// performs the load image, given a set of command arguments
        /// </summary>
        /// <param name="args"></param>
        void LoadImage(LoadImageCommandArgs args)
        {
            using (var client = new ImageReviewManagerClient())
            {
                client
                    .GetImageInfoAsync(args.ImageId)
                    .ContinueWith(task =>
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Image loaded:");
                        sb.AppendLine($"ImageId = {args.ImageId}");
                        sb.AppendLine($"When = {args.AcquisitionDateTime}");
                        sb.AppendLine($"Patient Name = {task.Result.PatientName}");
                        ImageInfoText = sb.ToString();
                    }, _uiScheduler);

                client
                    .ReviewImageAsync(new ImageReviewRequest()
                    {
                        ImageId = args.ImageId
                    })
                    .ContinueWith(task =>
                    {
                        ImagePixelText = task.Result.DailyImage.Pixels.ToString();
                    }, _uiScheduler);
            }
        }
    }

    /// <summary>
    /// command arguments for loading an image
    /// </summary>
    public class LoadImageCommandArgs
    {
        public Guid ImageId { get; set; }

        public DateTime AcquisitionDateTime { get; set; }
    }
}
