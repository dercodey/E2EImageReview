using System;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using EndToEndImageReviewApp.Events;


namespace EndToEndImageReviewApp.ViewModels
{
    public class ImageViewerViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;

        public ImageViewerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            LoadImageCommand = 
                new DelegateCommand<LoadImageCommandArgs>(LoadImage);

            _eventAggregator
                .GetEvent<WorklistItemSelectedEvent>()
                .Subscribe(eventArgs =>
                    LoadImageCommand.Execute(
                        new LoadImageCommandArgs()
                        {
                            ImageId = eventArgs.ImageId,
                            AcquisitionDateTime = eventArgs.AcquisitionDateTime,
                        }));
        }

        public string ImageInfoText
        {
            get => _imageInfoText;
            set => SetProperty(ref _imageInfoText, value);
        }
        string _imageInfoText;

        public ICommand LoadImageCommand { get; set; }

        void LoadImage(LoadImageCommandArgs args)
        {
            ImageInfoText = 
                string.Format($"Image loaded:\nguid = {args.ImageId}\nwhen = {args.AcquisitionDateTime}");
        }
    }

    public class LoadImageCommandArgs
    {
        public Guid ImageId { get; set; }

        public DateTime AcquisitionDateTime { get; set; }
    }
}
