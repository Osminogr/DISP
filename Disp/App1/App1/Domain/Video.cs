using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace App1.Domain
{
    public class Video : Entity
    {
        public string url;
        public string path;
        public bool validated;
        public Stream data;
        public Adv adv;

        public Video() { }
    }
}
