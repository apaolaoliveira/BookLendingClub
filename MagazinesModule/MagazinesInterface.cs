using BookLendingClub.BoxesModule;
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
                int selectedOption = SetMenu("magazines' options", "Add new magazine", "View magazines' list", "Edit magazines' information", "Remove a magazine", "Go back");

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
            SetHeader("add new magazine");

            bool verifyBoxList = magazinesRepository.VerifyBoxList();

            if (verifyBoxList == true)
            {
                Console.ReadKey();
                return;
            }

            string title = SetStringField("Title:", ConsoleColor.Cyan);

            string collection = SetStringField("Collection:", ConsoleColor.Cyan);

            int editionNumber = SetIntField("Edition Number:", ConsoleColor.Cyan);

            int publicationYear = SetIntField("Publication Year:", ConsoleColor.Cyan);

            boxesInterface.ViewBoxes();

            int selectedNumber = SetIntField("\nBox Number:", ConsoleColor.Cyan);
            int newSelectedNumber = boxesRepository.isValidId(selectedNumber, boxesRepository);

            Boxes boxNumber = (Boxes)boxesRepository.GetId(newSelectedNumber, boxesRepository);

            Magazines newMagazine = new Magazines(magazinesRepository.idCounter, title, collection, editionNumber, publicationYear, boxNumber);

            magazinesRepository.AddNewEntity(newMagazine);

            ColorfulMessage("\nNew magazine successfully added!", ConsoleColor.Green);

            SetFooter();
        }

        public void ViewMagazines()
        {
            ColorfulMessage("\n\tMAGAZINE'S LIST :)\n\n", ConsoleColor.Cyan);

            ColorfulMessage(" --------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
            ColorfulMessage("| ID | TITLE                        | COLLECTION                   | EDITION | YEAR | BOX TAG       - COLOR     |\n", ConsoleColor.Cyan);
            ColorfulMessage(" --------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

            bool hasMagazines = magazinesRepository.HasEntity();

            if (hasMagazines == true)
            {
                foreach (Magazines magazine in magazinesRepository.list)
                {
                    Console.WriteLine("| {0,-3}| {1,-29}| {2,-29}| {3,-8}| {4,-5}| {5,-13} - {6,-10}|", magazine.id, magazine.Title, magazine.Collection, magazine.EditionNumber, magazine.PublicationYear, magazine.Box.Tag, magazine.Box.Color);
                }
            }
            else
            {
                ColorfulMessage("\n                                      You don't have any magazine yet :(\n\n", ConsoleColor.Gray);
            }
            Console.Write(" ---------------------------------------------------------------------------------------------------------------");

            SetFooter();
        }

        private void EditMagazine()
        {
            SetHeader("edit magazine");

            bool hasMagazines = magazinesRepository.HasEntity();

            if (hasMagazines == false)
            {
                ColorfulMessage("\nYou don't have any magazine yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewMagazines();

            int selectedId = SetIntField("\nEnter your magazine's ID:", ConsoleColor.Cyan);

            int newSelectedId = magazinesRepository.isValidId(selectedId, magazinesRepository);

            magazinesRepository.GetId(newSelectedId, magazinesRepository);

            ColorfulMessage("\nWhat information would you like to change?\n", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                   "\n[1] Title."
                                 + "\n[2] Collection."
                                 + "\n[3] Edition number."
                                 + "\n[4] Publication Year."
                                 + "\n[5] Box."
                                 + "\n\n→ "
                                 , ConsoleColor.Cyan);

            bool validOption = false;

            foreach (Magazines magazine in magazinesRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            string newTitle = SetStringField("New title:", ConsoleColor.Gray);
                            magazine.Title = newTitle;

                            ColorfulMessage("\nTitle updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            string newCollection = SetStringField("New collection:", ConsoleColor.Gray);
                            magazine.Collection = newCollection;

                            ColorfulMessage("\nCollection updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            int newEditionNumber = SetIntField("New Edition Number:", ConsoleColor.Gray);
                            magazine.EditionNumber = newEditionNumber;

                            ColorfulMessage("\nEdition Number updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            int newPublicationYear = SetIntField("New Publication Year:", ConsoleColor.Gray);
                            magazine.PublicationYear = newPublicationYear;

                            ColorfulMessage("\nPublication Year updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 5:
                            int boxId = SetIntField("New Box, enter the number:", ConsoleColor.Gray);
                            Boxes newBox = (Boxes)boxesRepository.GetId(boxId, boxesRepository);
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
            SetFooter();
        }

        private void RemoveMagazine()
        {
            SetHeader("remove magazine");

            bool hasMagazines = magazinesRepository.HasEntity();

            if (hasMagazines == false)
            {
                ColorfulMessage("\nYou don't have any magazine yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewMagazines();

            int selectedId = SetIntField("\nEnter your magazine's ID:", ConsoleColor.Cyan);

            int newSelectedId = magazinesRepository.isValidId(selectedId, magazinesRepository);

            magazinesRepository.RemoveEntity(newSelectedId, magazinesRepository);

            ColorfulMessage("\nMagazine sucessfuly removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
