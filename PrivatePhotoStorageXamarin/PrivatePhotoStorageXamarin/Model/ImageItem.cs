﻿using System;
using SQLite;
using Xamarin.Forms;

namespace PrivatePhotoStorageXamarin.Model
{
    [Table("ImageItem")]
    public class ImageItem
    {
        [PrimaryKey]
        private Guid ID { get; set; }

        public string Source { get; set; }
    }
}
