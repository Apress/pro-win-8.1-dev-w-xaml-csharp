using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GridView2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IList<Person> people;
        private IList<Group<object, Person>> groupedPeople;

        public MainPage()
        {
            this.InitializeComponent();
            people = Person.CreatePeople(200).ToList();
            groupedPeople = (from person in people
                group person by person.City
                into g orderby g.Key
                select new Group<object, Person>
                {
                    Key = g.Key.ToString(),
                    Items = g.ToList()
                }).ToList();
            cvs.Source = groupedPeople;
            this.SizeChanged += MainPage_SizeChanged;
        }

        void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width <= 500)
            {
                VisualStateManager.GoToState(this, "MinimalLayout", true);
            }
            else if (e.NewSize.Width < e.NewSize.Height)
            {
                VisualStateManager.GoToState(this, "PortraitLayout", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "DefaultLayout", true);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void cmdSwitch_Click(object sender, RoutedEventArgs e)
        {
            //myGridView.Visibility = (myGridView.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            //myListView.Visibility = (myListView.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}