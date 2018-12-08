using System;
using System.Collections.Generic;

namespace Breweries.Models.DB
{
    public partial class Brewery
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Email { get;  set; }
        public string Telephone { get; set; }
      
        public string _website;

        public string Website
        {
            //override get method to account for websites that don't include http:// at beginning
            get
            {
                if (_website != null)
                    if (!_website.StartsWith("http://") && _website.Length > 0)
                    {
                        return "http://" + _website;
                    }
                    else return _website;
                else return "";
            }
            set
            {
                _website = value;
            }
        }

    }

}
