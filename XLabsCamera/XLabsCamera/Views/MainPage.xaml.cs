using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XLabs.Platform.Services.Media;
using XLabsCamera.Photos;
using System.Runtime.CompilerServices;
using XLabsCamera;
using XLabs.Forms.Controls;
using System.Linq;

namespace XLabsCamera.Views
{
	public partial class MainPage : ContentPage
	{
		private IMediaPicker _mediaPicker;
		private IPhotoService _dataBaseService;
		private ISavePhotoToSecretGallery _savePhotoService;
		//		private GridView GridWithPhoto;

		public MainPage ()
		{
			InitializeComponent ();
			_mediaPicker = DependencyService.Get<IMediaPicker> ();
			_dataBaseService = DependencyService.Get<PhotoService> ();
			_savePhotoService = DependencyService.Get<ISavePhotoToSecretGallery> ();

			var list = _dataBaseService.GetThings ();

			var listSource = new List<ImageSource> ();

			foreach (var item in list) {
				AddImageView (item.Source);
			}

//			GridWithPhoto = new GridView () {
//				BackgroundColor = Color.Black,
//				ItemHeight = Device.OnPlatform (Width, Width, 0),
//				ItemWidth = Device.OnPlatform (Width, Width, 0),
//				RowSpacing = Device.OnPlatform (1, 1, 0),
//				ColumnSpacing = Device.OnPlatform (1, 1, 0),
//				HeightRequest = 200,
//				MinimumHeightRequest = 200,
//				WidthRequest = 200,
//				MinimumWidthRequest = 200
//			};
//
//			GridWithPhoto.ItemTemplate = new DataTemplate (() => {
//				var cell = new ViewCell ();
//				var view = new Image ();
//				view.BackgroundColor = Color.Blue;
//
//				view.SetBinding (Image.SourceProperty, "Source");
//				cell.View = view;
//				return cell;
//			});
//
			//			GridWithPhoto.ItemsSource = list;
//			mainStack.Children.Add (GridWithPhoto);

			takePhoto.Clicked += TakePhotoClicked;
			selectPhoto.Clicked += SelectPhotoClicked;
		}

		private async void SelectPhotoClicked (object sender, EventArgs e)
		{
			
			if (_mediaPicker.IsPhotosSupported) {
				await _mediaPicker.SelectPhotoAsync (new CameraMediaStorageOptions ()).ContinueWith (t => {
					if (t.IsCanceled) {
						return;
					}
					var mediaFile = t.Result;
					AddPhotoToDataBase (mediaFile.Path);
				});
			} else {
				
			}
		}

		private async void TakePhotoClicked (object sender, EventArgs e)
		{
			if (_mediaPicker.IsCameraAvailable) {
				await _mediaPicker.TakePhotoAsync (new CameraMediaStorageOptions ()).ContinueWith (t => {
					if (t.IsCanceled) {
						return;
					}
					var mediaFile = t.Result;
					AddPhotoToDataBase (mediaFile.Path);
				}, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext ());
			} else {
				
			}
		}

		private void AddPhotoToDataBase (string path)
		{
			var newPath = _savePhotoService.CopyPhotoTo (path);
			Xamarin.Forms.Device.BeginInvokeOnMainThread (() => {
				AddImageView (newPath);
			});

			_dataBaseService.Add (new PhotoItem {
				Source = newPath
			});
		}

		private void AddImageView (string path)
		{
			var img = new Image {
				Source = ImageSource.FromFile (_savePhotoService.GetPath () + "/" + path),
				HeightRequest = 70,
				MinimumHeightRequest = 70,
//				BackgroundColor = Color.Green
			};
		
			mainStack.Children.Add (img);
		}
	}
}

