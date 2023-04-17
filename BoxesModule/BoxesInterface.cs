using BookLendingClub.Share;

namespace BookLendingClub.BoxesModule
{
    public class BoxesInterface : Interface
    {
        public BoxesRepository boxesRepository = null;

        public void BoxesOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                Console.Clear();

                SetMenu("BOXES' OPTIONS", "Add new box.", "View boxes' list.", "Edit boxes' information.", "Remove a box.", "Go back.");

                int selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1: AddNewBox(); break;
                    case 2: ViewBoxes(); break;
                    case 3: EditBox(); break;
                    case 4: RemoveBox(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        private void AddNewBox()
        {
            Console.Clear();
            ColorfulMessage(
              "\n\tADD NEW BOX"
            + "\n------------------------------\n", ConsoleColor.Cyan);

            ColorfulMessage("\nColor:" + "\n→ ", ConsoleColor.Cyan);
            string color = Console.ReadLine();

            ColorfulMessage("\nTag:" + "\n→ ", ConsoleColor.Cyan);
            string tag = Console.ReadLine();

            Boxes newBox = new Boxes(boxesRepository.idCounter, color, tag);

            boxesRepository.AddNewBox(newBox);

            ColorfulMessage("\nNew box successfully added!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }

        public void ViewBoxes()
        {
            Console.Clear();
            ColorfulMessage("\n\tBOXES' LIST :)\n\n", ConsoleColor.Cyan);

            ColorfulMessage(" ----------------------------------- \n", ConsoleColor.Cyan);
            ColorfulMessage("| NUMBER | TAG           | COLOR    |\n", ConsoleColor.Cyan);
            ColorfulMessage(" ----------------------------------- \n", ConsoleColor.Cyan);

            bool hasBoxes = boxesRepository.HasBoxes();

            if (hasBoxes == true)
            {
                foreach (Boxes box in boxesRepository.list)
                {
                    Console.WriteLine("| {0,-7}| {1,-14}| {2,-9}|", box.id, box.Tag, box.Color);
                }
            }
            else
            {
                ColorfulMessage("\n   You don't have any box yet :(\n\n", ConsoleColor.Gray);
            }

            Console.Write(" -----------------------------------");

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        private void EditBox()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nEDIT BOX"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasBoxes = boxesRepository.HasBoxes();

            if (hasBoxes == false)
            {
                ColorfulMessage("\nYou don't have any box yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewBoxes();

            ColorfulMessage("\n\nEnter the box number:" + "\n→ ", ConsoleColor.Cyan);
            int selectedNumber = Convert.ToInt32(Console.ReadLine());

            while (selectedNumber <= 0 || selectedNumber > boxesRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis box's number doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedNumber = Convert.ToInt32(Console.ReadLine());
            }

            boxesRepository.GetBoxesId(selectedNumber);

            ColorfulMessage("\nWhat information would you like to change?\n", ConsoleColor.Cyan);

            ColorfulMessage(
                  "\n[1] Color"
                + "\n[2] Tag"
                + "\n\n→ "
                , ConsoleColor.Cyan);

            int selectedChange = Convert.ToInt32(Console.ReadLine());

            bool validOption = false;

            foreach (Boxes box in boxesRepository.list)
            {
                if (box.id == selectedNumber)
                {
                    while (!validOption)
                    {
                        switch (selectedChange)
                        {
                            case 1:
                                ColorfulMessage("\nNew color:" + "\n→ ", ConsoleColor.Gray);
                                string newColor = Console.ReadLine();

                                box.Color = newColor;

                                ColorfulMessage("\nColor updated!", ConsoleColor.Green);
                                validOption = true;
                                break;
                            case 2:
                                ColorfulMessage("\nNew tag:" + "\n→ ", ConsoleColor.Gray);
                                string newTag = Console.ReadLine();

                                box.Tag = newTag;

                                ColorfulMessage("\nTag updated!", ConsoleColor.Green);
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

        private void RemoveBox()
        {
            Console.Clear();
            ColorfulMessage(
                "\n\nREMOVE BOX"
              + "\n------------------------------\n", ConsoleColor.Cyan);

            bool hasBoxes = boxesRepository.HasBoxes();

            if (hasBoxes == false)
            {
                ColorfulMessage("\nYou don't have any box yet :(", ConsoleColor.Gray);
                ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
                Console.ReadKey();
                return;
            }

            ViewBoxes();

            ColorfulMessage("\n\nEnter the box number:" + "\n→ ", ConsoleColor.Cyan);
            int selectedNumber = Convert.ToInt32(Console.ReadLine());

            while (selectedNumber <= 0 || selectedNumber > boxesRepository.idCounter - 1)
            {
                ColorfulMessage("\nThis box's number doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                selectedNumber = Convert.ToInt32(Console.ReadLine());
            }

            boxesRepository.RemoveBox(selectedNumber);

            ColorfulMessage("\nBox sucessfully removed!", ConsoleColor.Green);

            ColorfulMessage("\n\n<-'", ConsoleColor.Cyan);
            Console.ReadLine();
        }
    }
}
