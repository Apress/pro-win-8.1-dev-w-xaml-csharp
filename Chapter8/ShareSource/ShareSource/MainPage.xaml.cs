using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ShareSource
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			DataTransferManager dtm = DataTransferManager.GetForCurrentView();
			dtm.DataRequested += dtm_DataRequested;
		}
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			DataTransferManager dtm = DataTransferManager.GetForCurrentView();
			dtm.DataRequested -= dtm_DataRequested;
		}

		void dtm_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
		{
			DataRequestDeferral deferral = args.Request.GetDeferral();
			try
			{
				DataPackage requestData = args.Request.Data;
				requestData.Properties.Title = "My data from my application";
				requestData.Properties.Description = "Description of my data from my application";
				//requestData.SetApplicationLink();
				requestData.Properties.ContentSourceWebLink = new Uri("http://www.apress.com");
				requestData.SetText("This is text from my source");
				args.Request
					.Data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(
						"<b>Important</b> data from  my application placed in <i>HTML</i>"));
			}
			catch (Exception ex)
			{
			}
			finally
			{
				deferral.Complete();
			}
		}
	}
}