using System;
using System.Collections.Generic;
using System.Text;

namespace OctOcean.Utils
{
    public class OctOceanGlobal
    {

        public static void SetConfig(string defaultConnectionString, string fileRoot, string urlRoot,string articlePreviewUrl)
        {
            if (_config == null)
            {
                _defaultConnectionString = defaultConnectionString;
                _fileRoot = fileRoot;
                _urlRoot = urlRoot;
                _articlePreviewUrl = articlePreviewUrl;

                InitConfig();

            }
        }

        private static void InitConfig()
        {
            _config = new OctOceanConfig(
                defaultConnectionString: _defaultConnectionString
               , fileRoot: _fileRoot
               , urlRoot: _urlRoot
               , articlePreviewUrl: _articlePreviewUrl
               );
        }


        private static string _defaultConnectionString;
        private static string _fileRoot;
        private static string _urlRoot;
        private static string _articlePreviewUrl;
        private static OctOceanConfig _config = null;
        public static OctOceanConfig Config
        {
            get
            {
                if (_config == null)
                {
                    InitConfig();
                }
                return _config;

            }
        }
    }
}
