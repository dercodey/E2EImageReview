using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Text;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using ImagingTypes;

using EndToEndImageReviewApp.Events;

namespace EndToEndImageReviewApp.ViewModels
{
    /// <summary>
    /// view model to represent the image viewer
    /// </summary>
    public class ImageViewerViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;
        TaskScheduler _uiScheduler;
        ImageViewerModel _model = new ImageViewerModel();

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

        /// <summary>
        /// 
        /// </summary>
        public string ImagePixelText
        {
            get => _imagePixelText;
            set => SetProperty(ref _imagePixelText, value);
        }
        string _imagePixelText;

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
            _model.GetImageInfoAsync(args.ImageId, args.AcquisitionDateTime)
                .ContinueWith(task =>
                {
                    ImageInfoText = task.Result;
                }, _uiScheduler);

            _model.ReviewImageAsync(args.ImageId)
                .ContinueWith(task =>
                {
                    ImagePixelText = task.Result.GetPixelString();
                }, _uiScheduler);
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
