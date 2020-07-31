using PokeTypeWeakness.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PokeTypeWeakness.Models
{
    public class Attribution
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }

        public string PrimaryColourHex { get; set; }
        public string SecondaryColourHex { get; set; }

        public Color PrimaryColour { get { return CalculateColour(PrimaryColourHex); } }
        public Color SecondaryColour { get { return CalculateColour(SecondaryColourHex); } }

        Color CalculateColour(string colourHex)
        {
            if (string.IsNullOrEmpty(colourHex))
                return Color.Transparent;
            return Color.FromHex(colourHex);
        }
    }
}
