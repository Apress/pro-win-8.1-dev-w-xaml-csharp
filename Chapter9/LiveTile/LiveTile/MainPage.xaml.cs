using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using NotificationsExtensions.TileContent;
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

namespace LiveTile
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

		private void CreateTileWithXML_Click(object sender, RoutedEventArgs e)
		{
			XmlDocument tileSquareXML = TileUpdateManager.GetTemplateContent(
				TileTemplateType.TileSquare150x150Text01); //Header, 3 lines (no wrap)
			/*
			*<tile>
			*	<visual version="2">
			*		<binding template="TileSquare150x150Text01" fallback="TileSquareText01">
			*			<text id="1"></text>
			*			<text id="2"></text>
			*			<text id="3"></text>
			*			<text id="4"></text>
			*		</binding>
			*	</visual>
			*</tile>"
			*/
			XmlNodeList tileTextAttributes = tileSquareXML.GetElementsByTagName("text");
			tileTextAttributes[0].InnerText = "Square Tile XML";
			tileTextAttributes[1].InnerText = "Line 1";
			tileTextAttributes[2].InnerText = "Line 2";
			tileTextAttributes[3].InnerText = "Line 3";

			XmlDocument tileWideXML = TileUpdateManager.GetTemplateContent(
				TileTemplateType.TileWide310x150Text01); //Header, 4 lines (no wrap)
			/*<tile>
			*  <visual version="2">
			*    <binding template="TileWide310x150Text01" fallback="TileWideText01">
			*      <text id="1"></text>
			*      <text id="2"></text>
			*      <text id="3"></text>
			*      <text id="4"></text>
			*      <text id="5"></text>
			*    </binding>
			*  </visual>
			*</tile>
			*/
			XmlNodeList wideTileTextAttributes = tileWideXML.GetElementsByTagName("text");
			wideTileTextAttributes[0].InnerText = "Wide Tile XML";
			wideTileTextAttributes[1].InnerText = "Line 1";
			wideTileTextAttributes[2].InnerText = "Line 2";
			wideTileTextAttributes[3].InnerText = "Line 3";
			wideTileTextAttributes[4].InnerText = "Line 4";
			
			////Version must be set to 2
			XmlDocument tileLargeXML = TileUpdateManager.GetTemplateContent(
				TileTemplateType.TileSquare310x310Text01); //Header, 9 lines (no wrap)
			/*<tile>
			*  <visual version="2">
			*    <binding template="TileSquare310x310Text01">
			*      <text id="1"></text>
			*      <text id="2"></text>
			*      <text id="3"></text>
			*      <text id="4"></text>
			*      <text id="5"></text>
			*      <text id="6"></text>
			*      <text id="7"></text>
			*      <text id="8"></text>
			*      <text id="9"></text>
			*      <text id="10"></text>
			*    </binding>
			*  </visual>
			*</tile>
			*/
			XmlNodeList largeTileTextAttributes = tileLargeXML.GetElementsByTagName("text");
			largeTileTextAttributes[0].InnerText = "Large Tile XML";
			largeTileTextAttributes[1].InnerText = "Line 1";
			largeTileTextAttributes[2].InnerText = "Line 2";
			largeTileTextAttributes[3].InnerText = "Line 3";
			largeTileTextAttributes[4].InnerText = "Line 4";
			largeTileTextAttributes[5].InnerText = "Line 5";
			largeTileTextAttributes[6].InnerText = "Line 6";
			largeTileTextAttributes[7].InnerText = "Line 7";
			largeTileTextAttributes[8].InnerText = "Line 8";
			largeTileTextAttributes[9].InnerText = "Line 9";
			//Combine all into one payload
			IXmlNode squareNode = tileLargeXML.ImportNode(tileSquareXML.GetElementsByTagName("binding").Item(0), true);
			IXmlNode wideNode = tileLargeXML.ImportNode(
				tileWideXML.GetElementsByTagName("binding").Item(0), true);
			var visual = tileLargeXML.GetElementsByTagName("visual").Item(0);
			visual.AppendChild(wideNode);
			visual.AppendChild(squareNode);
			//Send to the queue
			//Remove the Icon from the lower left corner
			var bindings = tileLargeXML.GetElementsByTagName("binding");
			for (var x = 0; x < bindings.Length; x++)
			{
				((XmlElement)bindings[x]).SetAttribute("branding", "none");
			}

			var updater = TileUpdateManager.CreateTileUpdaterForApplication();
			updater.EnableNotificationQueue(true);
			TileNotification notification = new TileNotification(tileLargeXML);
			updater.Update(notification);
			
			TextBasedXML.Text = XDocument.Parse(tileLargeXML.GetXml()).ToString();
		}

		private void CreateTileWithExtensions_Click(object sender, RoutedEventArgs e)
		{
			// Create a notification for the Square150x150 tile using one of the available templates for the size.
			ITileSquare150x150Text01 square150x150Content = TileContentFactory.CreateTileSquare150x150Text01();
			square150x150Content.Branding = TileBranding.None;
			square150x150Content.TextHeading.Text = "Square Tile EXT";
			square150x150Content.TextBody1.Text = "Line 1";
			square150x150Content.TextBody2.Text = "Line 2";
			square150x150Content.TextBody3.Text = "Line 3";

			// Create a notification for the Wide310x150 tile using one of the available templates for the size.
			ITileWide310x150Text01 wide310x150Content = TileContentFactory.CreateTileWide310x150Text01();
			wide310x150Content.Branding = TileBranding.None;
			wide310x150Content.TextHeading.Text = "Wide Tile EXT";
			wide310x150Content.TextBody1.Text = "Line 1";
			wide310x150Content.TextBody2.Text = "Line 2";
			wide310x150Content.TextBody3.Text = "Line 3";
			wide310x150Content.TextBody4.Text = "Line 4";

			// Create a notification for the Square310x310 tile using one of the available templates for the size.
			ITileSquare310x310Text01 tileContent = TileContentFactory.CreateTileSquare310x310Text01();
			tileContent.Branding = TileBranding.None;
			tileContent.TextHeading.Text = "Large Tile EXT";
			tileContent.TextBody1.Text = "Line 1";
			tileContent.TextBody2.Text = "Line 2";
			tileContent.TextBody3.Text = "Line 3";
			tileContent.TextBody4.Text = "Line 4";
			tileContent.TextBody5.Text = "Line 5";
			tileContent.TextBody6.Text = "Line 6";
			tileContent.TextBody7.Text = "Line 7";
			tileContent.TextBody8.Text = "Line 8";
			tileContent.TextBody9.Text = "Line 9";

			// Attach the Square150x150 template to the Wide310x150 template.
			wide310x150Content.Square150x150Content = square150x150Content;

			// Attach the Wide310x150 template to the Square310x310 template.
			tileContent.Wide310x150Content = wide310x150Content;

			// Send the notification to the application’s tile.
			//TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());

			var updater = TileUpdateManager.CreateTileUpdaterForApplication();
			updater.EnableNotificationQueue(true);
			updater.Update(tileContent.CreateNotification());

			ExtensionBasedXML.Text = XDocument.Parse(tileContent.GetContent()).ToString();
		}

		private void ClearTiles_Click(object sender, RoutedEventArgs e)
		{
			TileUpdateManager.CreateTileUpdaterForApplication().Clear();
			TextBasedXML.Text = "";
		}

		public void ClearTilesExt_Click(object sender, RoutedEventArgs e)
		{
			TileUpdateManager.CreateTileUpdaterForApplication().Clear();
			ExtensionBasedXML.Text = "";
		}
	}
}