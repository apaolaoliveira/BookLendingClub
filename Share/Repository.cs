using System.Collections;

namespace BookLendingClub.Share
{
    public class Repository
    {
        public ArrayList list = new ArrayList();

        public int idCounter = 1;

        public void increaseId() { idCounter++; }

        public Entity GetId(int selectedId, Repository repository)
        {
            Entity entity = null;

            foreach (Entity entityAdded in repository.list)
            {
                if (entityAdded.id == selectedId)
                {
                    entity = entityAdded;
                    break;
                }
            }
            return entity;
        }

        public bool HasEntity()
        {
            if (list.Count == 0) { return false; }
            else { return true; }
        }

        public void AddNewEntity(Entity entity)
        {
            list.Add(entity);
            entity.id = idCounter;
            increaseId();
        }

        public void RemoveEntity(int selectedId, Repository repository)
        {
            Entity entity = GetId(selectedId, repository);

            if (entity != null) { repository.list.Remove(entity); }
        }

        public int isValidId(int selectedId, Repository repository)
        {
            do
            {
                if (selectedId <= 0 || selectedId > repository.idCounter - 1)
                {
                    Interface.ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                    selectedId = Convert.ToInt32(Console.ReadLine());
                }

                else { break; }

            } while (true);

            return selectedId;
        }

    }
}
