
namespace BookLendingClub.BoxesModule
{
    public class Boxes
    {
        public string Color { get; set; }
        public string Tag { get; set; }

        public int id;

        public Boxes(int id,string color, string tag)
        {
            this.id = id;
            Color = color;
            Tag = tag;
        }
    }
}
