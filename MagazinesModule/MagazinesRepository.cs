using BookLendingClub.BoxesModule;
using BookLendingClub.FriendsModule;
using BookLendingClub.Share;

namespace BookLendingClub.MagazinesModule
{
    public class MagazinesRepository : Repository
    {
        public BoxesRepository boxesRepository = null;

        public bool VerifyBoxList()
        {
            if (boxesRepository.list.Count == 0)
            {
                Interface.ColorfulMessage("\nYou need to have a box to add a magazine.", ConsoleColor.Red);
                Interface.ColorfulMessage("\n\n<-'", ConsoleColor.Red);                
                return true;
            } 
            else { return false; }
        }
    }
}
