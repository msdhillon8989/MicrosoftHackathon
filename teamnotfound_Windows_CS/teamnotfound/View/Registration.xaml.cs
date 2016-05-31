using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using teamnotfound;
using teamnotfound.Common;
using teamnotfound.DataModel;
using TeamNotFound.Models;
using TeamNotFound.View;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Registration : Page
    {
        //Fetch the blob storage
         private IMobileServiceTable<User> usertable = App.MobileService.GetTable<User>();
        private IMobileServiceTable<UserCred> userCredtable = App.MobileService.GetTable<UserCred>();

        public Registration()
        {
            this.InitializeComponent();
           


        }
       
        


      






        private async Task InsertUserCred(UserCred userCred)
        {
           await userCredtable.InsertAsync(userCred);
        }
        private async Task InsertUser(User user)
        {
            await usertable.InsertAsync(user);
        }
        private async void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            UserCred userCred = new UserCred();
  //          user.Fname = FirstNameTextBox.Text;
  //          user.Lname = LastNameTextBox.Text;
  //          user.Mobile = MobileTextBox.Text;
  //          user.Email = EmailTextBox.Text;
  //          userCred.UserName = user.Email;
  //          userCred.Password = PasswordTextBox.Password;

            var userName = usertable.Where(usr => usr.Email == user.Email);
            if(userName != null)
            {

            }
            else
            {
                await InsertUser(user);
                Global.SetRepositoryValue("userName", user.Email);
                await InsertUserCred(userCred);
                Frame.Navigate(typeof(AddSkill));
            }
            
        }

       

        private void RePasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string pass = PasswordTextBox.Password;
            string repass = RePasswordTextBox.Password;
            if(pass != repass)
            {
                RePassErrorTextBox.Text = "Password doesn't match";
                //PasswordTextBox.
            }else
            {
                PassErrorTextBox.Text = "";
            }
        }

        private async void RegisterButton_click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            UserCred userCred = new UserCred();
            user.Fname = name.Text;
            /* string temp;
            SummaryTextBox.Document.GetText(TextGetOptions.None, out temp);
            var range = SummaryTextBox.Document.GetRange(0, temp.Length - 1);
            user.Summary = range.Text;*/
            user.Email = EmailTextBox.Text;
            userCred.UserName = user.Email;
            userCred.Password = PasswordTextBox.Password;
            var userNameList = await usertable.Where(usr => usr.Email == user.Email).ToListAsync();
            if (userNameList.Count !=0)
            {
                var dialog = new MessageDialog("User is already present");
                dialog.Title="Alert";
                dialog.Commands.Add(new UICommand("Ok") { Id=1});
                var result = await dialog.ShowAsync();
            }
            else
            {
                await InsertUser(user);
                Global.SetRepositoryValue("userName", user.Email);
                await InsertUserCred(userCred);
                Frame.Navigate(typeof(DashBoard));
            }
        }

     

      

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text == "")
            {
                EmailErrorTextBox.Visibility = Visibility.Visible;
                EmailErrorTextBox.Text = "Enter email Id";
            }
            else
            {
                EmailErrorTextBox.Visibility = Visibility.Collapsed;
                EmailErrorTextBox.Text = "";
            }
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password == "")
            {
                PassErrorTextBox.Visibility = Visibility.Visible;
                PassErrorTextBox.Text = "Enter the password";
            }
            else
            {
                PassErrorTextBox.Visibility = Visibility.Collapsed;
                PassErrorTextBox.Text = "";
            }
        }
      

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // string userType= (sender as ComboBox).SelectedItem as string;
            ComboBoxItem item = comboBox.SelectedItem as ComboBoxItem;
            string userType = item.Content.ToString();
            Debug.Write("User type: " + userType);
            if (userType == "Admin")
            {
                AdminKey.Visibility = Visibility.Visible;
            }
            else if (userType == "User")
            {
                AdminKey.Visibility = Visibility.Collapsed;
            }
        }

        private void AdminKeyErrorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (AdminKeyTextBox.Text == "")
            {
                AdminKeyErrorTextBox.Text = "Enter the admin key";
            }
            else
            {
                AdminKeyErrorTextBox.Text = "";
            }
        }
    }
}
