using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Movies
{
    [Activity(Label = "Movies", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

     

            Button button = FindViewById<Button>(Resource.Id.getMoviesButton);

            button.Click += async (sender, e) => {

            

                string url= "https://facebook.github.io/react-native/movies.json";

                JsonValue json = await FetchMoviesAsync (url);
                ParseAndDisplay (json);
            };
        }
 
        private async Task<JsonValue> FetchMoviesAsync (string url)
        {
           
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
            request.ContentType = "application/json";
            request.Method = "GET";

           
            using (WebResponse response = await request.GetResponseAsync ())
            {
              
                using (Stream stream = response.GetResponseStream ())
                {
                 
                    JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString ());

                    return jsonDoc;
                }
            }
        }

    
        private void ParseAndDisplay (JsonValue json)
        {
        
          
            ListView list = FindViewById<ListView>(Resource.Id.listView1);

        
            List<ListViewData> items = new List<ListViewData>(); 
            JsonValue results = json["movies"];
          
            foreach (JsonValue item in results)
            {
                items.Add(new ListViewData { Title = item["title"], ReleaseYear = item["releaseYear"] });

        
            }

            ListViewClass adapter = new ListViewClass(this, Resource.Layout.Second_row, items);

            list.Adapter = adapter;





        }
    }
}
