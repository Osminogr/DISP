using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App1.Domain
{
    public class VideoRequest
    {
        public string text;
        public List<Stream> photos;
        public Adv adv;

        public VideoRequest() { }
    }
}
