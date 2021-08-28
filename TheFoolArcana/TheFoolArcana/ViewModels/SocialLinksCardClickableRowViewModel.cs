using System.Collections.Generic;

namespace TheFoolArcana.ViewModels
{
    public class SocialLinksCardClickableRowViewModel
    {
        public IEnumerable<SocialLinksCardClickableViewModel> Cards { get; set; }

        public SocialLinksCardClickableRowViewModel(IEnumerable<SocialLinksCardClickableViewModel> cards)
        {
            Cards = cards;
        }
    }
}
