using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Bidding : Page
    {
        private IMobileServiceTable<Country> countryTable = App.MobileService.GetTable<Country>();
        private MobileServiceCollection<Country, Country> items;
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
        private MobileServiceCollection<Bid, Bid> bids;
        private IMobileServiceTable<admin_key> keyTable = App.MobileService.GetTable<admin_key>();
        private IMobileServiceTable<Event> eventTable = App.MobileService.GetTable<Event>();
        //private MobileServiceCollection<Country, Country> items;
        List<Country> country = new List<Country>();
        List<Bid> bid = new List<Bid>();
        int i = 1;
        List<String> parameter;
        List<string> countries = new List<string>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            parameter = e.Parameter as List<String>;
            Debug.Write("param1: " + parameter);
            //getProjects(parameter);
            //createCountry();
            getCountry();
            getEvent(parameter);

        }
        public Bidding()
        {
            this.InitializeComponent();
        }
        /*private async void createCountry()
        {
            var count = new admin_key {Id="a1b2c3d4" ,Key="qazwsxed1234"};
            await InsertCountry(count);
            

        }
        private async Task InsertCountry(admin_key count)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Apps has assigned an Id, the item is added to the CollectionView
            await keyTable.InsertAsync(count);


            //await App.MobileService.SyncContext.PushAsync(); // offline sync
        }*/
        private async void getCountry()
        {
            items = await countryTable
                   .ToCollectionAsync();
            country = items.ToList();
            type.ItemsSource = country;

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string value = (sender as CheckBox).Content.ToString();
            Debug.Write("Value: " + value);
            countries.Add(value);
            StackPanel sp = new StackPanel();
            sp.Name = "panel-" + value;
            sp.Orientation = Orientation.Horizontal;
            TextBlock countryTextBlock = new TextBlock();
            countryTextBlock.Text = value;
            countryTextBlock.Name = "country-" + value;
            sp.Children.Add(countryTextBlock);

            TextBox bidTextBlock = new TextBox();
            Thickness margin = bidTextBlock.Margin;
            margin.Left = 20;
            bidTextBlock.Margin = margin;
            bidTextBlock.Name = "bid-" + value;
            Debug.Write("Bid: " + bidTextBlock.Name);
            sp.Children.Add(bidTextBlock);

            TextBox highestTextBlock = new TextBox();
            highestTextBlock.Text = "value";
            Thickness margin1 = highestTextBlock.Margin;
            margin1.Left = 20;
            highestTextBlock.Margin = margin1;
            highestTextBlock.Name = "highest-" + value;
            sp.Children.Add(highestTextBlock);
            sPanel.Children.Add(sp);
            i++;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string value = (sender as CheckBox).Content.ToString();
            countries.Remove(value);
            UIElement child = sPanel.FindName("panel-" + value) as UIElement;

            sPanel.Children.Remove(child);

        }
        private async void getEvent(List<String> parameter)
        {
            List<Event> items = await eventTable
                    .Where(Event => Event.Id == parameter[0])
                    .ToListAsync();

            title.Text = items[0].Title+"\n"+items[0].StartDate+"-"+items[0].EndDate+"\n"+items[0].Location;

            if (parameter[1] == "Add")
            {
                btnSubmit.Content = "Bid";
            }
            else if (parameter[1] == "Update")
            {
                btnSubmit.Content = "Update";
                /* bids = await bidTable
                     .Where(Bid => Bid.ProjectId == parameter[0])
                     .Where(Bid => Bid.Bidder == "diksha.bajaj@hpe.com")
                     .ToCollectionAsync();
                 bid = bids.ToList();
                 txtAmount.Text = (bid[0].BiddAmt).ToString();
                 txtTime.Text = (bid[0].TimePeriod).ToString();*/

            }
            //gridView.ItemsSource = items;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Debug.Write("Countries count: " + countries.Count);
            string user = (string)Global.GetRepositoryValue("userName");
            for (var i = 0; i < countries.Count; i++)
            {
                Debug.WriteLine(countries[i]);
            }
            for(i=0;i<countries.Count;i++)
            {
                StackPanel child = sPanel.FindName("panel-" + countries[i]) as StackPanel;
                TextBlock child1 = child.FindName("country-" + countries[i]) as TextBlock;
                TextBox child2 = child.FindName("bid-" + countries[i]) as TextBox;
                TextBox child3 = child.FindName("highest-" + countries[i]) as TextBox;
                Debug.Write("Bid1: " + child2.Name);
                Debug.Write("Bid2: " + child2.Text);
                var bidding = new Bid {  BiddAmt = Int32.Parse(child2.Text), Bidder = user, EventId = parameter[0], Countr=countries[i] };

                if ((btnSubmit.Content).ToString() == "Bid")
                {
                     await InsertBid(bidding);
                }
                 else if ((btnSubmit.Content).ToString() == "Update")
                {
                   bid = await bidTable
                            .Where(Bid => Bid.EventId == parameter[0])
                            .Where(Bid => Bid.Bidder == user)
                            .Where(Bid => Bid.Countr == countries[i])
                            .ToListAsync();
                    if (bid.Count != 0)
                    {
                        bidding = new Bid { Id = bid[0].Id, BiddAmt = Int32.Parse(child2.Text), Bidder = user, EventId = parameter[0], Countr = countries[i] };
                        await UpdateBid(bidding);
                    }
                    
                    else
                    {
                        bidding = new Bid { BiddAmt = Int32.Parse(child2.Text), Bidder = user, EventId = parameter[0], Countr = countries[i] };
                        await InsertBid(bidding);
                    }


                }
            
            Frame.Navigate(typeof(MySportEvents));
           
        }
        }
        private async Task InsertBid(Bid bidding)
        {
            await bidTable.InsertAsync(bidding);
        }

        private async Task UpdateBid(Bid bidding)
        {
           bids = await bidTable
                            .Where(Bid => Bid.EventId == bidding.EventId)
                            .ToCollectionAsync();
            Bid bd= bids.FirstOrDefault();
            await bidTable.UpdateAsync(bidding);
        }
        
    }
}
