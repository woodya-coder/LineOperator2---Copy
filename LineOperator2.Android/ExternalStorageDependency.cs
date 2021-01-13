using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LineOperator2.Services;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(LineOperator2.Droid.ExternalStorageDependency))]
namespace LineOperator2.Droid
{
    
    public class ExternalStorageDependency : IExternalStoragePath
    {
        public ExternalStorageDependency() { }

        public List<string> GetExternalStoragePath()
        {
            Context context = Android.App.Application.Context;
            Java.IO.File[] filePath = context.GetExternalFilesDirs("");

            List<string> results = new List<string>(filePath.Length);

            foreach(var f in filePath)
            {
                results.Add(f.Path);
            }
            return results;
        }
    }

}