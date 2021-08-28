using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.ViewModels
{
    public class SocialLinksCardClickableViewModel
    {
        public string Arcana { get; set; }
        public int? Height { get; set; }

        public SocialLinksCardClickableViewModel(string arcana)
        {
            Arcana = arcana;
        }
    }
}
