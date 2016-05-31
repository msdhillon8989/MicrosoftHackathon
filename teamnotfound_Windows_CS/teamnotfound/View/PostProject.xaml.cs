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
        private IMobileServiceTable<Project> projectTable = App.MobileService.GetTable<Project>();
        private IMobileServiceTable<Category> categoryTable = App.MobileService.GetTable<Category>();
        private MobileServiceCollection<Category, Category> items;
        public PostProject()
        {
            this.InitializeComponent();
        //    getCategory();
        }


        private async Task getCategory()
        {
            items = await categoryTable.ToCollectionAsync();
            
        //    type.ItemsSource = items;
        }


        private async Task InsertProject(Project project)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Apps has assigned an Id, the item is added to the CollectionView
            await projectTable.InsertAsync(project);


            //await App.MobileService.SyncContext.PushAsync(); // offline sync
        }


        private async void button_Click(object sender, RoutedEventArgs e)
        {
       /*     var selected = type.SelectedIndex;
            Debug.Write(selected);
            var selectedValue = items.ElementAtOrDefault(selected);
            Debug.Write(selectedValue.Name+"  "+ selectedValue.Id);
            var project = new Project {  Description = description.Text, Bid = Int32.Parse(bid.Text), Owner = Global.GetRepositoryValue("userName").ToString() , Type= selectedValue.Id, Status="Bidding" };
            await InsertProject(project);
        */
        }
    }
}
