using BookLendingClub.BoxesModule;
using BookLendingClub.FriendsModule;
using BookLendingClub.Share;

namespace BookLendingClub.MagazinesModule
{
    public class MagazinesInterface : Interface
    {
        public MagazinesRepository magazinesRepository = null;
        public BoxesRepository boxesRepository = null;
        public BoxesInterface boxesInterface = null;

        public void MagazinesOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                Console.Clear();
                SetMenu("MAGAZINES' OPTIONS", "Add new magazine.", "View magazines' list.", "Edit magazines' information.", "Remove a magazine.", "Go back.");

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1: AddNewMagazine(); break;
                    case 2: ViewMagazines(); break;
                    case 3: EditMagazine(); break;
                    case 4: RemoveMagazine(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        private void AddNewMagazine()
        {
            Console.Clear();
            ColorfulMessage(
              "\n\tADD NEW MAGAZINE"
            + "\n------------------------------\n", ConsoleColor.Cyan);

            bool verifyBoxList = magazinesRepository.VerifyBoxList();

            if (verifyBoxList == true)
            {
                Console.ReadKey();
                return;
            }

            ColorfulMessage("\nTitle:" + "\n→ ", ConsoleColor.Cyan);
            string title = Console.ReadLine();

            ColorfulMessage("\nCollection:" + "\n→ ", ConsoleColor.Cyan);
            string collection = Console.ReadLine();

            ColorfulMessage("\nEdition Number:" + "\n→ ", ConsoleColor.Cyan);
            int editionNumber = Convert.ToInt32(Console.ReadLine());

            ColorfulMessage("\nPublication Year:" + "\n→ ", ConsoleColor.Cyan);
            int publicationYear = Convert.ToInt32(Console.ReadLine());

            boxesInterface.ViewBoxes();

            ColorfulMessage("\n\nBox Number:" + "\n→ ", ConsoleColor.Cyan);
            int selectedNumber = Convert.ToInt32(Console.ReadLine());

            Boxes boxNumber = boxesRepository.GetBoxesId(selectedNumber);

            Magazines newMagazine = new Magazines(magazinesRepository.idCounter, title, collection, editionNumber, publicationYear, boxNumber);

            magazinesRepository.AddNewMagazine(newMagazine);

            ColorfulMessage("\nNew magazine successfully added!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        public void ViewMagazines()
        {
            Console.Clear();
            ColorfulMessage("\n\tMAGAZINE'S LIST :)\n\n", ConsoleColor.Cyan);

            ColorfulMessage(" --------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
            ColorfulMessage("| ID | TITLE                        | COLLECTION                   | EDITION | YEAR | BOX TAG       - COLOR     |\n", ConsoleColor.Cyan);
            ColorfulMessage(" --------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

            bool hasMagazines = magazinesRepository.HasMagazines();

            if (hasMagazines == true)
            {
                foreach (Magazines magazine in magazinesRepository.list)
                {
                    Console.WriteLine("| {0,-3}| {1,-29}| {2,-29}| {3,-8}| {4,-5}| {5,-13} - {6,-10}|", magazine.id, magazine.Title, magazine.Collection, magazine.EditionNumber, magazine.PublicationYear, magazine.Box.Tag, magazine.Box.Color);
                }
            }
            else
            {
                ColorfulMessage("\n                          You don't have any magazine yet :(\n\n", ConsoleColor.Gray);
            }
            Console.Write(" ---------------------------------------------------------------------------------------------------------------");

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        private void EditMagazine()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\tEDIT MAGAZINE"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasMagazines = magazinesRepository.HasMagazines();

            if (hasMagazines == false)
            {
                ColorfulMessage("\nYou don't have any magazine yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewMagazines();

            ColorfulMessage("\n\nEnter your magazine's ID:" + "\n→ ", ConsoleColor.Cyan);
            int selectedId = Convert.ToInt32(Console.ReadLine());

            while (selectedId <= 0 || selectedId > magazinesRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedId = Convert.ToInt32(Console.ReadLine());
            }

            magazinesRepository.GetMagazineId(selectedId);

            ColorfulMessage("\nWhat information would you like to change?\n", ConsoleColor.Cyan);

            ColorfulMessage(
                  "\n[1] Title."
                + "\n[2] Collection."
                + "\n[3] Edition number."
                + "\n[4] Publication Year."
                + "\n[5] Box."
                + "\n\n→ "
                , ConsoleColor.Cyan);

            int selectedChange = Convert.ToInt32(Console.ReadLine());

            bool validOption = false;

            foreach (Magazines magazine in magazinesRepository.list)
            {
                if (magazine.id == selectedId)
                {
                    while (!validOption)
                    {
                        switch (selectedChange)
                        {
                            case 1:
                                ColorfulMessage("\nNew title:" + "\n→ ", ConsoleColor.Gray);
                                string newTitle = Console.ReadLine();

                                magazine.Title = newTitle;

                                ColorfulMessage("\nTitle updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 2:
                                ColorfulMessage("\nNew collection:" + "\n→ ", ConsoleColor.Gray);
                                string newCollection = Console.ReadLine();

                                magazine.Collection = newCollection;

                                ColorfulMessage("\nCollection updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 3:
                                ColorfulMessage("\nNew Edition Number:" + "\n→ ", ConsoleColor.Gray);
                                int newEditionNumber = Convert.ToInt32(Console.ReadLine());

                                magazine.EditionNumber = newEditionNumber;

                                ColorfulMessage("\nEdition Number updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 4:
                                ColorfulMessage("\nNew Publication Year:" + "\n→ ", ConsoleColor.Gray);
                                int newPublicationYear = Convert.ToInt32(Console.ReadLine());

                                magazine.PublicationYear = newPublicationYear;

                                ColorfulMessage("\nPublication Year updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 5:
                                ColorfulMessage("\nNew Box, enter the box Number:" + "\n→ ", ConsoleColor.Gray);
                                int boxId = Convert.ToInt32(Console.ReadLine());

                                Boxes newBox = boxesRepository.GetBoxesId(boxId);
                                magazine.Box = newBox;

                                ColorfulMessage("\nBox updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            default:
                                ColorfulMessage("\nInvalid option selected! Try again:" + "\n→ ", ConsoleColor.Red);
                                selectedChange = Convert.ToInt32(Console.ReadLine());
                                break;
                        }
                        break;
                    }
                }
            }
            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        private void RemoveMagazine()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nREMOVE MAGAZINE"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasMagazines = magazinesRepository.HasMagazines();

            if (hasMagazines == false)
            {
                ColorfulMessage("\nYou don't have any magazine yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewMagazines();

            ColorfulMessage("\n\nEnter your magazine's ID:" + "\n→ ", ConsoleColor.Cyan);
            int selectedId = Convert.ToInt32(Console.ReadLine());

            while (selectedId <= 0 || selectedId > magazinesRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedId = Convert.ToInt32(Console.ReadLine());
            }

            magazinesRepository.RemoveMagazine(selectedId);

            ColorfulMessage("\nMagazine sucessfuly removed!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }
    }
}
