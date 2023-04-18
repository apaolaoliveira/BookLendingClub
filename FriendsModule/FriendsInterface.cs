using BookLendingClub.Share;

namespace BookLendingClub.FriendsModule
{
    public class FriendsInterface : Interface 
    {
        public FriendsRepository friendsRepository = null;

        public void FriendsOptions()
        {
            bool proceed = true;

            while (proceed)
            {
                int selectedOption = SetMenu("Friends' options", "Add new friend", "View friends' list", "Edit friends' information", "Remove a friend", "Go back");

                switch (selectedOption)
                {
                    case 1: AddNewFriend(); break;
                    case 2: ViewFriends(); break;
                    case 3: EditFriend(); break;
                    case 4: RemoveFriend(); break;
                    case 5: proceed = false; break;
                }
            }
        }

        private void AddNewFriend()
        {
            SetHeader("add new friend");

            string name = SetStringField("Name:", ConsoleColor.Cyan);

            string guardian = SetStringField("Guardian:", ConsoleColor.Cyan);

            string phone = SetStringField("Phone:", ConsoleColor.Cyan);

            string address = SetStringField("Address:", ConsoleColor.Cyan);

            Friends newfriend = new Friends(friendsRepository.idCounter, name, guardian, address, phone);

            friendsRepository.AddNewEntity(newfriend);

            ColorfulMessage("\nNew friend successfully added!", ConsoleColor.Green);

            SetFooter();
        }

        public void ViewFriends()
        {
            ColorfulMessage("\n\tFRIEND'S LIST :)\n\n", ConsoleColor.Cyan);

            ColorfulMessage(" ----------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);
            ColorfulMessage("| ID | NAME                         | GUARDIAN                     | ADDRESS                      | PHONE         |\n", ConsoleColor.Cyan);
            ColorfulMessage(" ----------------------------------------------------------------------------------------------------------------- \n", ConsoleColor.Cyan);

            bool hasFriends = friendsRepository.HasEntity();

            if (hasFriends == true)
            {
                foreach (Friends friend in friendsRepository.list)
                {
                    Console.WriteLine("| {0,-3}| {1,-29}| {2,-29}| {3,-29}| {4,-14}|", friend.id, friend.Name, friend.Guardian, friend.Address, friend.Phone);
                }
            }
            else
            {
                ColorfulMessage("\n                                         You don't have any friend yet :(\n\n", ConsoleColor.Gray);
            }
            Console.Write(" -----------------------------------------------------------------------------------------------------------------");

            SetFooter();
        }

        private void EditFriend()
        {
            SetHeader("edit friend");

            bool hasFriends = friendsRepository.HasEntity();

            if (hasFriends == false)
            {
                ColorfulMessage("\nYou don't have any friend yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewFriends();

            int selectedId = SetIntField("\nEnter your friend's ID:", ConsoleColor.Cyan);

            int newSelectedId = friendsRepository.isValidId(selectedId, friendsRepository);

            friendsRepository.GetId(newSelectedId, friendsRepository);

            ColorfulMessage("\nWhat information would you like to change?", ConsoleColor.Cyan);

            int selectedChange = SetIntField(
                                   "\n[1] Name"
                                 + "\n[2] Guardian"
                                 + "\n[3] Phone"
                                 + "\n[4] Address\n", ConsoleColor.Cyan);

            bool validOption = false;

            foreach (Friends friend in friendsRepository.list)
            {
                while (!validOption)
                {
                    switch (selectedChange)
                    {
                        case 1:
                            string newName = SetStringField("New name:", ConsoleColor.Gray);
                            friend.Name = newName;

                            ColorfulMessage("\nName updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 2:
                            string newGuardian = SetStringField("New guardian:", ConsoleColor.Gray);
                            friend.Guardian = newGuardian;

                            ColorfulMessage("\nGuardian updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 3:
                            string newPhone = SetStringField("New phone:", ConsoleColor.Gray);
                            friend.Phone = newPhone;

                            ColorfulMessage("\nPhone updated!", ConsoleColor.Green);

                            validOption = true;
                            break;
                        case 4:
                            string newAddress = SetStringField("New address:", ConsoleColor.Gray);
                            friend.Address = newAddress;

                            ColorfulMessage("\nAddress updated!", ConsoleColor.Green);

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

        private void RemoveFriend()
        {
            SetHeader("remove friend");

            bool hasFriends = friendsRepository.HasEntity();

            if (hasFriends == false)
            {
                ColorfulMessage("\nYou don't have any friend yet :(", ConsoleColor.Gray);
                SetFooter();
                return;
            }

            ViewFriends();

            int selectedId = SetIntField("\nEnter your friend's ID:", ConsoleColor.Cyan);

            int newSelectedId = friendsRepository.isValidId(selectedId, friendsRepository);

            friendsRepository.RemoveEntity(newSelectedId, friendsRepository);

            ColorfulMessage("\nBye, bye friend!", ConsoleColor.Green);

            SetFooter();
        }
    }
}
