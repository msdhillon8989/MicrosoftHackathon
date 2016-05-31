using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using teamnotfound.DataModel;
using System.Threading.Tasks;
using teamnotfound.Common;
using System.Diagnostics;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostProject : Page
    {
        private IMobileServiceTable<Event> projectTable = App.MobileService.GetTable<Event>();
       private IMobileServiceTable<Country> countryTAble = App.MobileService.GetTable<Country>();
       private MobileServiceCollection<Country, Country> items;
        public PostProject()
        {
            this.InitializeComponent();
           getCategory();
        }


        private async Task getCategory()
        {
            items = await countryTAble.ToCollectionAsync();
            location.ItemsSource = items;
        //    type.ItemsSource = items;
        }


        private async Task InsertEvent(Event project)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Apps has assigned an Id, the item is added to the CollectionView
            await projectTable.InsertAsync(project);


            //await App.MobileService.SyncContext.PushAsync(); // offline sync
        }


        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var selected = location.SelectedIndex;
            Debug.Write(selected);
            var selectedValue = items.ElementAtOrDefault(selected);
            Debug.Write(selectedValue.CountryName + "  "+ selectedValue.Id);
       //     var project = new Project {  Description = description.Text, Bid = Int32.Parse(bid.Text), Owner = Global.GetRepositoryValue("userName").ToString() , Type= selectedValue.Id, Status="Bidding" };
        //    await InsertProject(project);

            var event1 = new Event();

            event1.Location = selectedValue.CountryName;
            DateTimeOffset sourceTime = endDate.Date;
           
            event1.EndDate = sourceTime.DateTime;

            sourceTime = startDate.Date;
            event1.StartDate = sourceTime.DateTime;

            sourceTime = bidEndDate.Date;
            event1.CloseDate = sourceTime.DateTime;

            event1.Title = tile.Text;
            event1.BasePrice = Int32.Parse(bid.Text);


            InsertEvent(event1);

            






        }
    }
}
