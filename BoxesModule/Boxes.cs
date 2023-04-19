
using BookLendingClub.Share;

namespace BookLendingClub.BoxesModule
{
    public class Boxes : Entity
    {
        public string Color { get; set; }
        public string Tag { get; set; }

        public Boxes(int boxId,string color, string tag)
        {
            id = boxId;
            Color = color;
            Tag = tag;
        } 
    }
}
