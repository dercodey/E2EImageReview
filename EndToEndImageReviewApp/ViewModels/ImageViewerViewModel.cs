﻿using System;
using System.Windows.Input;

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

using EndToEndImageReviewApp.Events;


namespace EndToEndImageReviewApp.ViewModels
{
    /// <summary>
    /// view model to represent the image viewer
    /// </summary>
    public class ImageViewerViewModel : BindableBase
    {
        IEventAggregator _eventAggregator;

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
        /// the load image command can be bound to a button (but needs parameters)
        /// </summary>
        public ICommand LoadImageCommand { get; set; }

        /// <summary>
        /// performs the load image, given a set of command arguments
        /// </summary>
        /// <param name="args"></param>
        void LoadImage(LoadImageCommandArgs args)
        {
            ImageInfoText = 
                string.Format($"Image loaded:\nguid = {args.ImageId}\nwhen = {args.AcquisitionDateTime}");
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
