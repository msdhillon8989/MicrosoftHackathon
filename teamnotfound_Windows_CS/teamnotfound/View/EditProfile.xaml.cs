using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using teamnotfound.Common;
using teamnotfound.DataModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
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
    public sealed partial class EditProfile : Page
    {
        private IMobileServiceTable<User> usertable = App.MobileService.GetTable<User>();

        public EditProfile()
        {
            this.InitializeComponent();
            FillProfileInfo();
        }
        private async void FillProfileInfo()
        {
            User user=null;
            try
            {
                string jsonData = (string)Global.GetRepositoryValue("userProfile");
                 user = JsonConvert.DeserializeObject<User>(jsonData);
            }
            catch(Exception e)
            {

                string userName = (string)Global.GetRepositoryValue("userName");
                var userRes = await usertable.Where(usr => usr.Email == userName).ToEnumerableAsync();
                 user = userRes.SingleOrDefault();
            }
            FirstNameTextBox.Text = user.Fname;
            LastNameTextBox.Text = user.Lname;
            EmailTextBox.Text = user.Email;
            MobileTextBox.Text = user.Mobile;
            SummaryTextBox.Text = user.Summary;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = null;
            try
            {
                file = await folder.GetFileAsync(user.Email + ".png");
                var bitmap = new BitmapImage(new Uri(file.Path));
                ProfileImage.Source = bitmap;
            }
            catch(FileNotFoundException ex)
            {
                return;
            }
            
        }
        private void FirstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FirstNameTextBox.Text == "")
            {
                FnameErrorTextBox.Text = "First Name can't be empty";
            }
            else
            {
                FnameErrorTextBox.Text = "";
            }
        }

        private void MobileTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (MobileTextBox.Text == "")
            {
                MobileErrorTextBox.Text = "Please provide mobile number";
            }
            else
            {
                MobileErrorTextBox.Text = "";
            }
        }
        private static CloudBlobContainer GetContainer()
        {
            
            var account = new CloudStorageAccount(new StorageCredentials("teamnotfound",
                "2NzBRdZDr43TeKpLQG3C4z2LO968jsU42mGmt1K6NxDyzO+GPmZKpj1kjCBYpB+rwHVojZXgwiC0mUUArDTv7Q=="),
                    true);
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("profile");
            return container;
        }
        public async void Upload(string key, StorageFile pickerResult)
        {

            var picker = new FileOpenPicker();          
            if (pickerResult == null)
                return;
            var container = GetContainer();
            var stream = await pickerResult.OpenSequentialReadAsync();
            var blob = container.GetBlockBlobReference(key+".png");
            await blob.UploadFromStreamAsync(stream.AsStreamForRead());
            //Store the profile in local file
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = null;
            try
            {
                file = await folder.GetFileAsync(key + ".png");
                await pickerResult.CopyAndReplaceAsync(file);

            }
            catch (FileNotFoundException ex)
            {
                await pickerResult.CopyAsync(folder, key + ".png");
            }
            //await  pickerResult.CopyAsync(folder, key + ".png");     
        }

        private async void ProfileImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            BasicProperties properties = await file.GetBasicPropertiesAsync();
            //if(properties.Size>)
          //  if(file.)
            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();
                image.SetSource(stream);
                stream.Dispose();
                string userName = (string)Global.GetRepositoryValue("userName");
                ProfileImage.Source = image;
                Upload(userName, file);
            }
            else
            {
                return;
            }
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.Fname = FirstNameTextBox.Text;
            user.Lname = LastNameTextBox.Text;
            user.Mobile = MobileTextBox.Text;
            user.Summary = SummaryTextBox.Text;
            user.Email = EmailTextBox.Text;
            try
            {
                string jsData = (string)Global.GetRepositoryValue("userProfile");
                var usr = JsonConvert.DeserializeObject<User>(jsData);
                user.ID = usr.ID;
            }
           catch(Exception ex)
            {
                var userRes = await usertable.Where(u => u.Email == user.Email).ToEnumerableAsync();
                User usr = userRes.SingleOrDefault();
                user.ID = usr.ID;
            }

            //Store updated profile in local storage
            string jsonData = JsonConvert.SerializeObject(user);
            Global.SetRepositoryValue("userProfile", jsonData);
           await usertable.UpdateAsync(user);
            Frame.Navigate(typeof(AddSkill));
        }
    }

}
