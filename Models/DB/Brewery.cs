using System;
using System.Collections.Generic;

namespace Breweries.Models.DB
{
    public partial class Brewery
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Telephone { get; set; }
    }
}
