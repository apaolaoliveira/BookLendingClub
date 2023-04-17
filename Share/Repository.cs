using System.Collections;

namespace BookLendingClub.Share
{
    public class Repository
    {
        public ArrayList list = new ArrayList();

        public int idCounter = 1;

        public void increaseId() { idCounter++; }
    }
}
