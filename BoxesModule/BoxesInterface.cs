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
                int selectedOption = SetMenu("boxes' options", "Add new box", "View boxes' list", "Edit boxes' information", "Remove a box", "Go back");

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
            SetHeader("add new box");

            string color = SetStringField("Color:", ConsoleColor.Cyan);

            string tag = SetStringField("Tag:", ConsoleColor.Cyan);

            Boxes newBox = new Boxes(boxesRepository.idCounter, color, tag);

            boxesRepository.AddNewEntity(newBox);

            ColorfulMessage("\nNew box successfully added!", ConsoleColor.Green);

            SetFooter();
        }

        public void ViewBoxes()
        {
            ColorfulMessage("\n\tBOXES' LIST :)\n\n", ConsoleColor.Cyan);

            ColorfulMessage(" ----------------------------------- \n", ConsoleColor.Cyan);
            ColorfulMessage("| NUMBER | TAG           | COLOR    |\n", ConsoleColor.Cyan);
            ColorfulMessage(" ----------------------------------- \n", ConsoleColor.Cyan);

            bool hasBoxes = boxesRepository.HasEntity();

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

            SetFooter();
        }

        private void EditBox()
        {
            SetHeader("edit box");

            bool hasBoxes = boxesRepository.HasEntity();

            if (hasBoxes == false)
            {
                ColorfulMessage("\nYou don't have any box yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewBoxes();

            int selectedId = SetIntField("\nEnter the box number:", ConsoleColor.Cyan);

            int newSelectedId = boxesRepository.isValidId(selectedId, boxesRepository);

            boxesRepository.GetId(newSelectedId, boxesRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                 "\n[1] Color"
                                 + "\n[2] Tag\n"
                                 , ConsoleColor.Cyan);

            bool validOption = false;

            foreach (Boxes box in boxesRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            string newColor = SetStringField("New color:", ConsoleColor.Gray);
                            box.Color = newColor;

                            ColorfulMessage("\nColor updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            string newTag = SetStringField("New tag:", ConsoleColor.Gray);
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
            SetFooter();
        }

        private void RemoveBox()
        {
            SetHeader("remove box");

            bool hasBoxes = boxesRepository.HasEntity();

            if (hasBoxes == false)
            {
                ColorfulMessage("\nYou don't have any box yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewBoxes();

            int selectedId = SetIntField("\nEnter the box number:", ConsoleColor.Cyan);

            int newSelectedId = boxesRepository.isValidId(selectedId, boxesRepository);

            boxesRepository.RemoveEntity(newSelectedId, boxesRepository);

            ColorfulMessage("\nBox sucessfully removed!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
