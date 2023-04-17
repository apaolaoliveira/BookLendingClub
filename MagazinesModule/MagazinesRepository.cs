using BookLendingClub.BoxesModule;
using BookLendingClub.FriendsModule;
using BookLendingClub.Share;

namespace BookLendingClub.MagazinesModule
{
    public class MagazinesRepository : Repository
    {
        public BoxesRepository boxesRepository = null;

        public bool HasMagazines()
        {
            if (list.Count == 0) { return false; }
            else { return true; }
        }

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

        public void AddNewMagazine(Magazines magazine)
        {
            list.Add(magazine);
            magazine.id = idCounter;
            increaseId();
        }

        public void RemoveMagazine(int selectedId) 
        { 
            Magazines magazine = GetMagazineId(selectedId);
            list.Remove(magazine); 
        }

        public Magazines GetMagazineId(int id)
        {
            Magazines magazine = null;

            foreach (Magazines magazineAdded in list)
            {
                if (magazineAdded.id == id)
                {
                    magazine = magazineAdded;
                    break;
                }
            }
            return magazine;
        }
    }
}
