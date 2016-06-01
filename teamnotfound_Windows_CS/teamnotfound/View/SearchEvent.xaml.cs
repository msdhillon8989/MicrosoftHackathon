using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections;
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
    public sealed partial class SearchEvent : Page
    {
        private IMobileServiceTable<Event> eventTable = App.MobileService.GetTable<Event>();
       // private MobileServiceCollection<Event, Event> events;
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
       // private MobileServiceCollection<Bid, Bid> bids;
        public SearchEvent()
        {
            this.InitializeComponent();
            getProjects();
        }
        List<Event> events = new List<Event>();
        private async void getProjects()
        {

            events = await eventTable
               .Where(Event => Event.Status == "Bidding")
                   .ToListAsync();
            for (var i = 0; i < events.Count; i++)
            {
                string cat = events[i].Id;
                
                List<Bid> bids = await bidTable
                    .Where(Bid => Bid.EventId == cat)
                        .ToListAsync();

                
                Debug.Write("Count: " + bids.Count);
                events[i].total_bids = bids.Count;
            }
            listView.ItemsSource = events;
        }
        
        private async void Project_Tapped(object sender, RoutedEventArgs e)
        {
            List<String> param = new List<String>();
            string id = ((sender as StackPanel).FindName("eventId") as TextBlock).Text;
            Debug.Write("Ïd: " + id);
            param.Add(id);
            string user = (string)Global.GetRepositoryValue("userName");
            List<Bid> bids = await bidTable
                    .Where(Bid => Bid.EventId == id)
                    .Where(Bid => Bid.Bidder == user)           //Bidder should come from global.cs
                    .ToListAsync();
            
            if (bids.Count == 0)    // The user has not bid for this project before
            {
                Debug.Write("Add Bid");
                param.Add("Add");
            }
            else                  // The user wants to update the bid for this project
            {
                Debug.Write("Update Bid");
                param.Add("Update");
            }
            Debug.Write("Text: " + id);
            Frame.Navigate(typeof(Bidding), param);
        }
    }
}
