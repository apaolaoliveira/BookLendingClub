using BookLendingClub.Share;

namespace BookLendingClub.BoxesModule
{
    public class BoxesRepository : Repository
    {      
        public bool HasBoxes()
        {
            if (list.Count == 0) { return false; }
            else { return true; }
        }

        public void AddNewBox(Boxes box)
        {
            list.Add(box);
            box.id = idCounter;
            increaseId();
        }

        public void RemoveBox(int selectedId) 
        { 
            Boxes box = GetBoxesId(selectedId);
            list.Remove(box); 
        }

        public Boxes GetBoxesId(int id)
        {
            Boxes box = null;

            foreach (Boxes boxAdded in list)
            {
                if (boxAdded.id == id)
                {
                    box = boxAdded;
                    break;
                }
            }
            return box;
        }     
    }
}
