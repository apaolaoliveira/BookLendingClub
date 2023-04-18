using BookLendingClub.BoxesModule;
using BookLendingClub.Share;
using System.Collections;

namespace BookLendingClub.MagazinesModule
{
    public class Magazines : Entity
    {
        public string Title { get; set; }
        public string Collection { get; set; }
        public int EditionNumber { get; set; }
        public int PublicationYear { get; set; }        
        public Boxes Box { get; set; }

        public Magazines(int magazineId, string title, string collection, int editionNumber, int publicationYear, Boxes box) 
        {
            id = magazineId;
            Title = title;
            Collection = collection;
            EditionNumber = editionNumber;
            PublicationYear = publicationYear;
            Box = box;
        }
    }
}
