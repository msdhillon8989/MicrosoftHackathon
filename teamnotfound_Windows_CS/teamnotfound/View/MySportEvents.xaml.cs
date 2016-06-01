using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using teamnotfound.Common;
using teamnotfound.DataModel;
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

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MySportEvents : Page
    {
        private IMobileServiceTable<Event> eventTable = App.MobileService.GetTable<Event>();
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();

        public MySportEvents()
        {
            this.InitializeComponent();
            getBids();
        }
        private async void getBids()
        {
            // Bidder must come from global.cs
           string user = (string)Global.GetRepositoryValue("userName");
            List<Bid> bid = await bidTable
               .Where(Bid => Bid.Bidder == user)
                   .ToListAsync();

            List<String> PIdList = new List<String>();
            Debug.Write("Count: " + bid.Count);
            for (var i = 0; i < bid.Count; i++)
            {
                PIdList.Add(bid[i].EventId);
            }
            getEventDetails(PIdList);

        }
        private async void getEventDetails(List<String> PIdList)
        {
            List<Event> proj = new List<Event>();
            for (var i = 0; i < PIdList.Count; i++)
            {
                Debug.Write("Inside for loop");
                List<Event> events = await eventTable
                    .Where(Event => Event.Id == PIdList[i])
                    .ToListAsync();


                proj.AddRange(events);
            }
            Debug.Write("Total projects: " + proj.Count);
            listView.ItemsSource = proj;
        }
        private void Project_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string text = ((sender as StackPanel).FindName("txtStatus") as TextBlock).Text;
            Debug.Write("Status: " + text);
            string pId = ((sender as StackPanel).FindName("eventId") as TextBlock).Text;
            if (text == "Bidding")
            {
                List<string> param = new List<string>();
                param.Add(pId);
                param.Add("Update");
                Frame.Navigate(typeof(Bidding), param);
            }
            else if (text == "Assigned")
            {
                Frame.Navigate(typeof(BillingPage), pId);
            }
            else { }

        }
    }
}
