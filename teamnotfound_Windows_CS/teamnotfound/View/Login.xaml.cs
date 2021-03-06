﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using teamnotfound;
using teamnotfound.Common;
using teamnotfound.DataModel;
using teamnotfound.View;

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
    public sealed partial class Login : Page
    {
        // private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();

        public Login()
        {
               this.InitializeComponent();
            PassportStatusText.Text = "";
        }
        private IMobileServiceTable<UserCred> userCredtable = App.MobileService.GetTable<UserCred>();
        private IMobileServiceTable<User> userTable = App.MobileService.GetTable<User>();

        private bool DoUserValidation(string user)
        {

            return true;
        }
        private async void PassportSignInButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            //IMobileServiceTableQuery<string> res = from uc in userCredtable where uc.UserName == "yuyuy" select uc.UserName;
            // userCredtable.Where();
            // res.IncludeTotalCount;  
            //ErrorTextBox.Text = res;
            if (userName == "")
            {
                ErrorTextBox.Visibility = Visibility.Visible;
                ErrorTextBox.Text = "Enter the user name.";
            }
            else
            {
                ErrorTextBox.Visibility = Visibility.Collapsed;
            }
            if (password == "")
            {
                ErrorTextBox.Text = "Enter the password.";
            }

            var items = await userCredtable
                            .Where(userCred => userCred.UserName == userName)
                            //.Where(userCred => userCred.Type == userType)
                            .Select(userCred => userCred.Password).ToEnumerableAsync();
            string pass = items.SingleOrDefault();
            if (pass == password)
            {
                PassportStatusText.Text = "Account Login Successful";
                var user = await userTable.Where(User => User.Email == userName).ToCollectionAsync<User>();
                Debug.WriteLine("IN PASSWORD ");
                Debug.WriteLine("Writing username" + userName);
                Global.SetRepositoryValue("userName", userName);
                if (user.ElementAt(0).UserType=="Admin")
                {
                    Frame.Navigate(typeof(DashBoardAdmin));
                }
                else
                {
                    Frame.Navigate(typeof(DashBoard));
                }
              
                
            }
            else
            {
                ErrorTextBox.Text = "Incorrect credentials";
            }
            ErrorTextBox.Text = "";

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registration));
        }

       
    }
}
