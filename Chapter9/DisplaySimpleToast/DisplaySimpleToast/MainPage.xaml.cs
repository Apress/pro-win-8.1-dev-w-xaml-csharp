using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using NotificationsExtensions.ToastContent;
using Windows.ApplicationModel.Activation;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DisplaySimpleToast
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		static LaunchActivatedEventArgs _launchArgs;

		public MainPage()
		{
			this.InitializeComponent();
		}
		public static LaunchActivatedEventArgs LaunchArgs
		{
			get
			{
				return _launchArgs;
			}
			set
			{
				_launchArgs = value;
				
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			if (e != null && e.Parameter != null && !String.IsNullOrEmpty(e.Parameter.ToString()))
			{
				LaunchParameter.Text = e.Parameter.ToString();
			}
			base.OnNavigatedTo(e);
		}

		public void CreateToastWithXML_Click(object sender, RoutedEventArgs e)
		{
			XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText04);
			/*
			 * <toast>
			 *	<visual>
			 *		<binding template="ToastText04">
			 *			<text id="1"></text>
			 *			<text id="2"></text>
			 *			<text id="3"></text>
			 *		</binding>
			 *	 </visual>
			 *	</toast>
			 */
			XmlNodeList nodes = toastXML.GetElementsByTagName("text");
			nodes[0].InnerText = "This is my header";
			nodes[1].InnerText = "Line 1";
			nodes[2].InnerText = "Line 2";
			//This does nothing in Toast Notifications
			//var binding = (XmlElement)toastXML.GetElementsByTagName("binding")[0];
			//binding.SetAttribute("branding", "none");
			
			//Add launch parameter
			toastXML.DocumentElement.SetAttribute("launch", "Activated from XML Toast");
			ToastNotification toast = new ToastNotification(toastXML);
			toast.Activated += toast_Activated;
			ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
			notifier.Show(toast);
			TextBasedXML.Text = XDocument.Parse(toastXML.GetXml()).ToString();
		}

		public void ClearToast_Click(object sender, RoutedEventArgs e)
		{
			TextBasedXML.Text = "";
		}

		public void CreateToastWithExtensions_Click(object sender, RoutedEventArgs e)
		{
			IToastText04 toastContent = ToastContentFactory.CreateToastText04();
			toastContent.TextHeading.Text = "This is my XML Header";
			toastContent.TextBody1.Text = "Line 1";
			toastContent.TextBody2.Text = "Line 2";
			toastContent.Launch = "Activated from Toast";
			ToastNotification toast = toastContent.CreateNotification();
			//This is for apps that are already running
			toast.Activated += toast_Activated;
			ToastNotifier notifier = ToastNotificationManager.CreateToastNotifier();
			notifier.Show(toast);
			ExtensionBasedXML.Text = XDocument.Parse(toastContent.GetContent()).ToString();
		}

		async void toast_Activated(ToastNotification sender, object args)
		{
			await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,()=>LaunchParameter.Text =((ToastActivatedEventArgs)args).Arguments);
		}

		public void ClearToastExt_Click(object sender, RoutedEventArgs e)
		{
			ExtensionBasedXML.Text = "";
		}
	}
}