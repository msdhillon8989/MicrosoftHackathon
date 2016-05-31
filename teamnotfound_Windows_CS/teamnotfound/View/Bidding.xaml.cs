using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace teamnotfound
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
        List<Country> country = new List<Country>();
        List<Bid> bid = new List<Bid>();
        int i = 1;
        List<String> parameter;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //parameter = e.Parameter as List<String>;
            //Debug.Write("param1: " + parameter);
            //getProjects(parameter);
            //createCountry();
            getCountry();

        }
        public Bidding()
        {
            this.InitializeComponent();
        }
        /*private async void createCountry()
        {
            var count = new Country {Id="1234" ,CountryName="Australia"};
            await InsertCountry(count);
            

        }
        private async Task InsertCountry(Country count)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Apps has assigned an Id, the item is added to the CollectionView
            await countryTable.InsertAsync(count);


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
            //StackPanel sp = new StackPanel();
            //sp.Orientation = Orientation.Horizontal;
            StackPanel sp = new StackPanel();
            sp.Name = "panel-" + value;
            sp.Orientation = Orientation.Horizontal;
            TextBlock countryTextBlock = new TextBlock();
            countryTextBlock.Text = value;
            countryTextBlock.Name = "countName"+i;
            sp.Children.Add(countryTextBlock);

            TextBox bidTextBlock = new TextBox();
            Thickness margin = bidTextBlock.Margin;
            margin.Left = 20;
            bidTextBlock.Margin = margin;
            
            sp.Children.Add(bidTextBlock);

            TextBox highestTextBlock = new TextBox();
            highestTextBlock.Text = "value";
            Thickness margin1 = highestTextBlock.Margin;
            margin1.Left = 20;
            highestTextBlock.Margin = margin1;
            sp.Children.Add(highestTextBlock);
            sPanel.Children.Add(sp);
            i++;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string value=(sender as CheckBox).Content.ToString();
            UIElement child = sPanel.FindName("panel-"+value) as UIElement;
            
            sPanel.Children.Remove(child);
            
        }
        /*private async void getProjects(List<String> parameter)
        {
            items = await projectTable
                    .Where(Project => Project.Id == parameter[0])
                    .ToCollectionAsync();
            proj = items.ToList();
            txtDesc.Text = proj[0].Description;
            txtBid.Text = (proj[0].Bid).ToString();

            if (parameter[1] == "Add")
            {
                btnSubmit.Content = "Bid";
                txtAmount.Text = "";
                txtTime.Text = "";
            }
            else if (parameter[1] == "Update")
            {
                btnSubmit.Content = "Update";
                bids = await bidTable
                    .Where(Bid => Bid.ProjectId == parameter[0])
                    .Where(Bid => Bid.Bidder == "diksha.bajaj@hpe.com")
                    .ToCollectionAsync();
                bid = bids.ToList();
                txtAmount.Text = (bid[0].BiddAmt).ToString();
                txtTime.Text = (bid[0].TimePeriod).ToString();

            }
            //gridView.ItemsSource = items;
        }

        private async void button_Click (object sender, RoutedEventArgs e)
        {
            var bidding = new Bid {  BiddAmt = Int32.Parse(txtAmount.Text), TimePeriod = Int32.Parse(txtTime.Text), Bidder = "diksha.bajaj@hpe.com", ProjectId = proj[0].Id };

            if ((btnSubmit.Content).ToString() == "Bid")
            {
                await InsertBid(bidding);
            }
            else if ((btnSubmit.Content).ToString() == "Update")
            {
                bids = await bidTable
                            .Where(Bid => Bid.ProjectId == proj[0].Id)
                            .Where(Bid => Bid.Bidder == "diksha.bajaj@hpe.com")
                            .ToCollectionAsync();
                bid = bids.ToList();
                bidding = new Bid { Id = bid[0].Id, BiddAmt = Int32.Parse(txtAmount.Text), TimePeriod = Int32.Parse(txtTime.Text), Bidder = "diksha.bajaj@hpe.com", ProjectId = proj[0].Id };

                await UpdateBid(bidding);
            }
            
            Frame.Navigate(typeof(MyProjects));
           
        }
        private async Task InsertBid(Bid bidding)
        {
            await bidTable.InsertAsync(bidding);
        }

        private async Task UpdateBid(Bid bidding)
        {
           /*bids = await bidTable
                            .Where(Bid => Bid.ProjectId == bid.ProjectId)
                            .ToCollectionAsync();
            Bid bd= bids.FirstOrDefault();*/
        /*await bidTable.UpdateAsync(bidding);
    }*/
    }
}
