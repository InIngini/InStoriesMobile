using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Xamarin.Forms.Maps;

namespace Курсач.Core.Data.Maps
{
    public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
