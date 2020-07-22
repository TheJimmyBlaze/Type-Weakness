using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeTypeWeakness.ViewModels
{
    public class TypeDetailViewModel: BaseViewModel
    {
        public PokeType PokeType { get; set; }

        public String StrengthText { get { return string.Format("{0} types are effective against:", PokeType.DisplayName); } }
        public String WeaknessText { get { return string.Format("{0} types are weak to:", PokeType.DisplayName); } }

        public TypeDetailViewModel(PokeType pokeType)
        {
            PokeType = pokeType;
            Title = "Details";
        }
    }
}
