using Microsoft.EntityFrameworkCore;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Microsoft.Extensions.DependencyInjection;
using SQLite;

namespace Music_Player ;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //CheckAndRequestPermissionAsync();
            SongPlayer player = new SongPlayer();
            //RequestPermissions();

            MainPage = new NavigationPage(new Home(player));
        }

        /*public async void RequestPermissions()
        {
            await CheckAndRequestReadStoragePermission();
            await CheckAndRequestWriteStoragePermission();
        }

        public async Task<PermissionStatus> CheckAndRequestReadStoragePermission()
        {
            PermissionStatus status = await PermissionRequest.CheckStatusAsync<PermissionRequest.StorageRead>();
            PermissionStatus status2 = await PermissionRequest.CheckStatusAsync<PermissionRequest.StorageWrite>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (PermissionRequest.ShouldShowRationale<PermissionRequest.StorageRead>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await PermissionRequest.RequestAsync<PermissionRequest.StorageRead>();

            return status;
        }

        public async Task<PermissionStatus> CheckAndRequestWriteStoragePermission()
        {
            PermissionStatus status = await PermissionRequest.CheckStatusAsync<PermissionRequest.StorageRead>();
            PermissionStatus status2 = await PermissionRequest.CheckStatusAsync<PermissionRequest.StorageWrite>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (PermissionRequest.ShouldShowRationale<PermissionRequest.StorageRead>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await PermissionRequest.RequestAsync<PermissionRequest.StorageRead>();

            return status;
        }*/
        /*
         public async Task RequestStoragePermissions()
        {
            try
            {
                var status = await PermissionRequest.RequestAsync<PermissionRequest.StorageRead>();
                if (status == PermissionStatus.Granted)
                {
                    // Permission granted, you can now access external storage.
                }
                else
                {
                    // Permission denied.
                }
            }
            catch (Exception ex)
            {
                // Handle exception, if any.
            }
        }
        
         async Task CheckAndRequestPermissionAsync()
        {
            try
            {
                var status = await PermissionRequest.RequestAsync<PermissionRequest.StorageRead>();
                if (status == PermissionStatus.Granted)
                {
                    await DisplayAlert("ss", "yeees", "fff");
                }
                else
                {
                    await DisplayAlert("ss", "noooo", "fff");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("sqwertuiop", ex.ToString(), "ma boy");
            }
        }*/
    }