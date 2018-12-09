using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Breweries.Models.DB
{
    public partial class Brewery
    {
        //ensure correct length and additional checks on user input
        [StringLength(36)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get;  set; }

        [Phone]
        [StringLength(50)]
        public string Telephone { get; set; }

        public string _website;

        [Url]
        [StringLength(50)]
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
