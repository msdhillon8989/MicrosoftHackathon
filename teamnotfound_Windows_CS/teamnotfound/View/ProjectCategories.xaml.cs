using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ProjectCategories : Page
    {
        private IMobileServiceTable<Category> categoryTable = App.MobileService.GetTable<Category>();
        private MobileServiceCollection<Category, Category> items;
        private IMobileServiceTable<Project> projectTable = App.MobileService.GetTable<Project>();
        private MobileServiceCollection<Project, Project> projects;
        public ProjectCategories()
        {
            this.InitializeComponent();
            getCategories();
        }
        private async void getCategories()
        {
            items = await categoryTable
                    .ToCollectionAsync();
            List<Category> categoryList = new List<Category>();
            categoryList = items.ToList();
            

            for (var i = 0; i < categoryList.Count; i++)
            {
                //getCount(proj[i].Name);
                string cat = categoryList[i].Name;
                Debug.Write("Category: " + cat);
                items = await categoryTable
                    .Where(Category => Category.Name == cat)
                        .ToCollectionAsync();

                List<Category> category = new List<Category>();
                category = items.ToList();
                String categoryId = category[0].Id;
                //Debug.Write("Id: " + proj[0].CatId);

                projects = await projectTable
                    .Where(Project => Project.Type == categoryId)
                        .ToCollectionAsync();

                List<Project> project = new List<Project>();
                project = projects.ToList();
                Debug.Write("Count: " + project.Count);
                categoryList[i].count = project.Count;
            }
            CategoryGridView.ItemsSource = categoryList;
        }
       
        private void Category_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string text=((sender as StackPanel).FindName("categoryName") as TextBlock).Text;
            Debug.Write("Text: " + text);
            //search.Text = text;
            //Frame.Navigate(typeof(SearchProject),text);
        }
    }
}
