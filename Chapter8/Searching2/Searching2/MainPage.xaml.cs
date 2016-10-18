using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Searching2
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private SearchPane searchPane;
		internal const int SEARCH_PANE_MAX_SUGGESTIONS = 5;
		private static readonly string[] suggestionList =
		{
			"Shanghai", "Istanbul", "Karachi", "Delhi", "Mumbai", "Moscow", "São Paulo", "Seoul", "Beijing", "Jakarta",
			"Tokyo", "Mexico City", "Kinshasa", "New York City", "Lagos", "London", "Lima", "Bogota", "Tehran", "Ho Chi Minh City",
			"Hong Kong", "Bangkok", "Dhaka", "Cairo", "Hanoi", "Rio de Janeiro", "Lahore", "Chonquing", "Bangalore", "Tianjin",
			"Baghdad", "Riyadh", "Singapore", "Santiago", "Saint Petersburg", "Surat", "Chennai", "Kolkata", "Yangon", "Guangzhou",
			"Alexandria", "Shenyang", "Hyderabad", "Ahmedabad", "Ankara", "Johannesburg", "Wuhan", "Los Angeles", "Yokohama",
			"Abidjan", "Busan", "Cape Town", "Durban", "Pune", "Jeddah", "Berlin", "Pyongyang", "Kanpur", "Madrid", "Jaipur",
			"Nairobi", "Chicago", "Houston", "Philadelphia", "Phoenix", "San Antonio", "San Diego", "Dallas", "San Jose",
			"Jacksonville", "Indianapolis", "San Francisco", "Austin", "Columbus", "Fort Worth", "Charlotte", "Detroit",
			"El Paso", "Memphis", "Baltimore", "Boston", "Seattle Washington", "Nashville", "Denver", "Louisville", "Milwaukee",
			"Portland", "Las Vegas", "Oklahoma City", "Albuquerque", "Tucson", "Fresno", "Sacramento", "Long Beach", "Kansas City",
			"Mesa", "Virginia Beach", "Atlanta", "Colorado Springs", "Omaha", "Raleigh", "Miami", "Cleveland", "Tulsa", "Oakland",
			"Minneapolis", "Wichita", "Arlington", " Bakersfield", "New Orleans", "Honolulu", "Anaheim", "Tampa", "Aurora",
			"Santa Ana", "St. Louis", "Pittsburgh", "Corpus Christi", "Riverside", "Cincinnati", "Lexington", "Anchorage",
			"Stockton", "Toledo", "St. Paul", "Newark", "Greensboro", "Buffalo", "Plano", "Lincoln", "Henderson", "Fort Wayne",
			"Jersey City", "St. Petersburg", "Chula Vista", "Norfolk", "Orlando", "Chandler", "Laredo", "Madison", "Winston-Salem",
			"Lubbock", "Baton Rouge", "Durham", "Garland", "Glendale", "Reno", "Hialeah", "Chesapeake", "Scottsdale",
			"North Las Vegas", "Irving", "Fremont", "Irvine", "Birmingham", "Rochester", "San Bernardino", "Spokane",
			"Toronto", "Montreal", "Vancouver", "Ottawa-Gatineau", "Calgary", "Edmonton", "Quebec City", "Winnipeg", "Hamilton"
		};

		public MainPage()
		{
			this.InitializeComponent();
			searchPane = SearchPane.GetForCurrentView();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			searchPane.QuerySubmitted +=
				new TypedEventHandler<SearchPane, SearchPaneQuerySubmittedEventArgs>(OnQuerySubmitted);

			searchPane.SuggestionsRequested +=
				new TypedEventHandler<SearchPane, SearchPaneSuggestionsRequestedEventArgs>(OnSearchSuggestionsRequested);

			searchPane.ResultSuggestionChosen += OnSearchSuggestionChosen;
		}

		internal void OnQuerySubmitted(object sender, SearchPaneQuerySubmittedEventArgs args)
		{
			ProcessQueryText(args.QueryText);
		}

		internal void ProcessQueryText(string queryText)
		{
			NotifyUser("Query submitted: " + queryText);
		}

		public void NotifyUser(string strMessage)
		{
			StatusBlock.Text = strMessage;
		}

		void OnSearchSuggestionChosen(
			SearchPane sender, SearchPaneResultSuggestionChosenEventArgs args)
		{
			NotifyUser("Recommendation picked: " + args.Tag);
		}

		private void OnSearchSuggestionsRequested(
			SearchPane sender, SearchPaneSuggestionsRequestedEventArgs e)
		{
			var queryText = e.QueryText;
			if (string.IsNullOrEmpty(queryText))
			{
				NotifyUser("Use the search pane to submit a query");
			}
			else
			{
				var request = e.Request;
				bool isRecommendationFound = false;

				var match = suggestionList
								.Where(x => x.CompareTo(queryText) == 0)
								.Select(x => x).FirstOrDefault();

				if (match != null)
				{
					RandomAccessStreamReference image =
						RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/windows-sdk.png"));
					isRecommendationFound = true;
					string item = match.ToString();
					request.SearchSuggestionCollection
						.AppendResultSuggestion(
						item, // text to display 
						item, // details
						item, // tags usable when called back by ResultSuggestionChosen event
						image, // image if any
						"image of " + item);
				}
				else
				{
					var results = suggestionList
									  .Where(x => x.StartsWith(queryText, StringComparison.CurrentCultureIgnoreCase))
									  .Select(x => x).Take(SEARCH_PANE_MAX_SUGGESTIONS);
					foreach (var itm in results)
					{
						request.SearchSuggestionCollection
							.AppendQuerySuggestion(itm);
					}
				}
				if (request.SearchSuggestionCollection.Size > 0)
				{
					string prefix = isRecommendationFound ? "* " : "";
					NotifyUser(prefix + "Suggestions provided for query: " + queryText);
				}
				else
				{
					NotifyUser("No suggestions provided for query: " + queryText);
				}
			}
		}
	}
}