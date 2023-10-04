using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Media;
using Music_Player.Methods;

    /*[assembly: Dependency(typeof(FilePickerService))]

    namespace Music_Player.Methods
    {
        public class FilePickerService
        {

            public void none()
            {
                FilePickerService ss = new FilePickerService();
            }
            
            public async Task PickMusicFileAsync()
            {
                try
                {
                    Intent intent = new Intent(Intent.ActionOpenDocument);
                    intent.AddCategory(Android.Content.Intent.CategoryOpenable);
                    intent.SetType("audio/*"); // Filter by MIME type

                    //MainActivity.inte.StartActivityForResult(intent, 0);

                    // Handle the result in MainActivity.OnActivityResult
                    // Return the selected file path from there.
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
*/