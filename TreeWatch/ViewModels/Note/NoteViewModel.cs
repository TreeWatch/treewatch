using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Services.Geolocation;
using System.Threading;
using System.Diagnostics;

namespace TreeWatch
{
	public class NoteViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// The _scheduler.
		/// </summary>
		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

		/// <summary>
		/// The picture chooser.
		/// </summary>
		private IMediaPicker _mediaPicker;

		/// <summary>
		/// The image source.
		/// </summary>
		private ImageSource _imageSource;

		/// <summary>
		/// The take picture command.
		/// </summary>
		private Command _takePictureCommand;

		/// <summary>
		/// The select picture command.
		/// </summary>
		private Command _selectPictureCommand;
		private string _status;

		/// <summary>
		/// The geolocator
		/// </summary>
		private IGeolocator _geolocator;
		/// <summary>
		/// The position status
		/// </summary>
		private string _positionStatus = string.Empty;
		/// <summary>
		/// The position latitude
		/// </summary>
		private string _positionLatitude = string.Empty;
		/// <summary>
		/// The position longitude
		/// </summary>
		private string _positionLongitude = string.Empty;
		/// <summary>
		/// The position longitude
		/// </summary>
		private string _accuracy = string.Empty;
		/// <summary>
		/// The cancel source
		/// </summary>
		private CancellationTokenSource _cancelSource;
		/// <summary>
		/// The get position command
		/// </summary>
		private Command _getPositionCommand;

		/// <summary>
		/// Gets or sets the position latitude.
		/// </summary>
		/// <value>The position latitude.</value>
		public string PositionLatitude
		{
			get
			{
				return _positionLatitude;
			}
			set
			{
				if (value != _positionLatitude)
				{
					_positionLatitude = value;
					OnPropertyChanged ("PositionLatitude");
				}
			}
		}

		/// <summary>
		/// Gets or sets the position longitude.
		/// </summary>
		/// <value>The position longitude.</value>
		public string PositionLongitude
		{
			get
			{
				return _positionLongitude;
			}
			set
			{
				if (value != _positionLongitude)
				{
					_positionLongitude = value;
					OnPropertyChanged ("PositionLongitude");
				}
			}
		}

		public string PositionStatus
		{
			get
			{
				return _positionStatus;
			}
			set
			{
				if (value != _positionStatus)
				{
					_positionStatus = value;
					OnPropertyChanged ("PositionStatus");
				}
			}
		}

		/// <summary>
		/// Gets or sets the position latitude.
		/// </summary>
		/// <value>The position latitude.</value>
		public string Accuracy
		{
			get
			{
				return _accuracy;
			}
			set
			{
				if (value != _accuracy)
				{
					_accuracy = value;
					OnPropertyChanged ("Accuracy");
				}
			}
		}

		/// <summary>
		/// Gets the get position command.
		/// </summary>
		/// <value>The get position command.</value>
		public Command GetPositionCommand 
		{
			get
			{ 
				Debug.WriteLine ("GetPositionCommand");
				return _getPositionCommand ??
					(_getPositionCommand = new Command(async () => await GetPosition(), () => Geolocator != null)); 
			}
		}

		private IGeolocator Geolocator
		{
			get
			{
				if (_geolocator == null)
				{
					_geolocator = DependencyService.Get<IGeolocator>();

					_geolocator.DesiredAccuracy = 1.0;
					_geolocator.PositionError += OnListeningError;
					_geolocator.PositionChanged += OnPositionChanged;
				}
				return _geolocator;
			}
		}

		bool IsBusy;

		private async Task GetPosition()
		{
			Debug.WriteLine ("Available: {0}, enabled: {1}", this.Geolocator.IsGeolocationAvailable, this.Geolocator.IsGeolocationEnabled);
			_cancelSource = new CancellationTokenSource();

			PositionStatus = "...";
			PositionLatitude = "...";
			PositionLongitude = "...";
			Accuracy = "...";
			IsBusy = true;
			await Geolocator.GetPositionAsync(10000, _cancelSource.Token, true)
				.ContinueWith(t =>
					{
						IsBusy = false;
						if (t.IsFaulted)
						{
							PositionStatus = ((GeolocationException) t.Exception.InnerException).Error.ToString();
						}
						else if (t.IsCanceled)
						{
							PositionStatus = "Canceled";
						}
						else
						{
							
							Test(t.Result);
						}
					}, _scheduler);
		}

		private void Test(XLabs.Platform.Services.Geolocation.Position pos)
		{
			PositionStatus = pos.Timestamp.ToString("G");
			PositionLongitude = pos.Longitude.ToString();
			PositionLatitude = pos.Latitude.ToString();
			Accuracy = pos.Accuracy.ToString();
			Debug.WriteLine ("Status: {0}, LA: {1}, LO: {2}", pos.Timestamp.ToString ("G"), pos.Latitude.ToString (), pos.Longitude.ToString ());
		}

		/// <summary>
		/// Handles the <see cref="E:ListeningError" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PositionErrorEventArgs"/> instance containing the event data.</param>
		private void OnListeningError(object sender, PositionErrorEventArgs e)
		{

		}

		/// <summary>
		/// Handles the <see cref="E:PositionChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PositionEventArgs"/> instance containing the event data.</param>
		private void OnPositionChanged(object sender, PositionEventArgs e)
		{
			Debug.WriteLine ("Status: {0}, LA: {1}, LO: {2}", e.Position.Timestamp.ToString ("G"), e.Position.Latitude.ToString (), e.Position.Longitude.ToString ());
		}

		public NoteViewModel ()
		{
			Setup();
		}

		/// <summary>
		/// Gets or sets the image source.
		/// </summary>
		/// <value>The image source.</value>
		public ImageSource ImageSource
		{
			get
			{
				return _imageSource;
			}
			set
			{
				if (value != _imageSource)
					_imageSource = value;
				OnPropertyChanged ("ImageSource");
			}
		}

		/// <summary>
		/// Gets the take picture command.
		/// </summary>
		/// <value>The take picture command.</value>
		public Command TakePictureCommand 
		{
			get
			{
				return _takePictureCommand ?? (_takePictureCommand = new Command(
					async () => await TakePicture(),
					() => true)); 
			}
		}

		/// <summary>
		/// Gets the select picture command.
		/// </summary>
		/// <value>The select picture command.</value>
		public Command SelectPictureCommand 
		{
			get
			{
				return _selectPictureCommand ?? (_selectPictureCommand = new Command(
					async () => await SelectPicture(),
					() => true)); 
			}
		}

		/// <summary>
		/// Gets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status
		{
			get { return _status; }
			private set 
			{ 
				if(value != _status)
					_status = value;
				OnPropertyChanged ("Status");
			}
		}

		/// <summary>
		/// Setups this instance.
		/// </summary>
		private void Setup()
		{
			if (_mediaPicker != null)
			{
				return;
			}

			var device = Resolver.Resolve<IDevice>();

			////RM: hack for working on windows phone? 
			_mediaPicker = DependencyService.Get<IMediaPicker>() ?? device.MediaPicker;
		}

		/// <summary>
		/// Takes the picture.
		/// </summary>
		/// <returns>Take Picture Task.</returns>
		private async Task<MediaFile> TakePicture()
		{
			Setup();

			ImageSource = null;
			var options = new CameraMediaStorageOptions() 
			{ 
				DefaultCamera = CameraDevice.Rear,
				SaveMediaOnCapture = true,
				MaxPixelDimension = 1024,
				PercentQuality = 85
			};

			return await _mediaPicker.TakePhotoAsync(options).ContinueWith(t =>
				{
					if (t.IsFaulted)
					{
						Status = t.Exception.InnerException.ToString();
					}
					else if (t.IsCanceled)
					{
						Status = "Canceled";
					}
					else
					{
						var mediaFile = t.Result;

						ImageSource = ImageSource.FromStream(() => mediaFile.Source);

						return mediaFile;
					}

					return null;
				}, _scheduler);
		}

		/// <summary>
		/// Selects the picture.
		/// </summary>
		/// <returns>Select Picture Task.</returns>
		private async Task SelectPicture()
		{
			Setup();

			ImageSource = null;
			try
			{
				var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
					{
						DefaultCamera = CameraDevice.Front,
						MaxPixelDimension = 400
					});
				ImageSource = ImageSource.FromStream(() => mediaFile.Source);
			}
			catch (System.Exception ex)
			{
				Status = ex.Message;
			}
		}

		private static double ConvertBytesToMegabytes(long bytes)
		{
			return (bytes / 1024f) / 1024f;
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}

