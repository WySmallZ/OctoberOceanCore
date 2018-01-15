using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OctOcean.Utils
{
    public class OctOceanConfig
    {
        public OctOceanConfig(string defaultConnectionString, string fileRoot, string urlRoot)
        {
            this.DefaultConnectionString = defaultConnectionString;
            this.FileRoot = fileRoot;
            this.UrlRoot = urlRoot;
        }
        public readonly string DefaultConnectionString = string.Empty;
        public readonly string FileRoot = string.Empty;
        public readonly string UrlRoot = string.Empty;



        private string _fileRoot_cache_image;
        public string FileRoot_Cache_Image
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_fileRoot_cache_image))
                {
                    _fileRoot_cache_image = Path.Combine(FileRoot, "Cache/image");
                    if (!Directory.Exists(_fileRoot_cache_image))
                    {
                        Directory.CreateDirectory(_fileRoot_cache_image);
                    }

                }
                return _fileRoot_cache_image;
            }

        }

        public string UrlRoot_Cache_Image
        {
            get { return UrlRoot + "/Cache/image"; }
        }



    }
}
