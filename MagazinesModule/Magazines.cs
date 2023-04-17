using BookLendingClub.BoxesModule;
using System.Collections;

namespace BookLendingClub.MagazinesModule
{
    public class Magazines
    {
        public string Title { get; set; }
        public string Collection { get; set; }
        public int EditionNumber { get; set; }
        public int PublicationYear { get; set; }        
        public Boxes Box { get; set; }
        
        public int id;

        public Magazines(int id, string title, string collection, int editionNumber, int publicationYear, Boxes box) 
        {
            this.id = id;
            Title = title;
            Collection = collection;
            EditionNumber = editionNumber;
            PublicationYear = publicationYear;
            Box = box;
        }
    }
}
