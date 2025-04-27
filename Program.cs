using System;
using System.ComponentModel.DataAnnotations;
using TestApp;

class Program
{
    static void Main()
    {
        List<Pet> pets = new List<Pet>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to my program");
            Console.WriteLine("Enter a command (add, remove, list, play, exit):");
            string command = Console.ReadLine();

            if (command == "exit")
            {
                Console.Clear();
                Console.WriteLine("Exiting the application...");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            else if (command == "list")
            {
                if (pets.Count == 0)
                {
                    Console.WriteLine("You don't own any pets!");
                    Console.WriteLine("Get a pet by using the 'add' command back in the menu!");
                }
                else
                {
                    listPets(pets);
                }
            }
            else if (command == "add")
            {
                Pet newPet = null;
                Console.WriteLine("Enter the type of pet (dog):");
                string type = Console.ReadLine().ToLower();

                string name;
                string breed;
                bool isMale;
                string genderInput;

                Console.WriteLine("Enter the name of the pet:");
                name = Console.ReadLine();

                if (name == null)
                {
                    name = "Rex";
                }

                Console.WriteLine("Enter the breed of the pet (optional):");
                breed = Console.ReadLine();

                if (breed == null)
                {
                    breed = "Unknown";
                }

                Console.WriteLine("Please enter the gender of your pet (m/f): ");
                genderInput = Console.ReadLine().ToLower();

                if (genderInput == "f")
                {
                    isMale = false;
                } else
                {
                    isMale = true;
                }

                switch(type)
                {
                    case "dog":
                        newPet = new Dog(name, breed, isMale);
                        break;
                    default:
                        Console.WriteLine("Invalid pet type. Please try again.");
                        break;
                }

                if (newPet != null)
                {
                    pets.Add(newPet);
                    Console.WriteLine("You now have a new pet!");
                    
                } else
                {
                    Console.WriteLine("Failed to add pet. Please try again.");
                }
                
            } else if(command == "play")
            {
                if (pets.Count == 0)
                {
                    Console.WriteLine("There are no pets to play with!");
                    Thread.Sleep(1000);
                    continue;
                }

                Console.WriteLine("Enter the id of the pet you want to play with:");
                Console.WriteLine("\n");
                listPets(pets);

                int petId;
                while (!int.TryParse(Console.ReadLine(), out petId) || petId < 1 || petId > pets.Count)
                {
                    Console.WriteLine("Invalid input. Please enter a valid pet ID:");
                }

                // Use the pet ID (adjusting for 0-based index)
                Pet selectedPet = pets[petId - 1];
                Console.WriteLine($"You selected: {selectedPet.Name} the {selectedPet.Type}.");
                Console.WriteLine("Please wait...");
                Thread.Sleep(2000);

                EnterPlayMode(selectedPet);
            }


            //Console.WriteLine("\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

        }

    }

    public static void listPets(List<Pet> pets)
    {
        drawLine();
        int i = 0;
        foreach (Pet pet in pets)
        {
            i++;
            Console.WriteLine($"Pet {i}: {pet.Type}");
            Console.WriteLine($"Name: {pet.Name}");
            Console.WriteLine($"Breed: {pet.Breed}");
            if (pet.IsMale)
            {
                Console.WriteLine("Gender: Male");
            }
            else
            {
                Console.WriteLine("Gender: Female");
            }

            drawLine();
        }
    }

    public static void drawLine()
    {
        Console.WriteLine("\n");
        Console.WriteLine("-------------------------");
        Console.WriteLine("\n");
    }

    public static void EnterPlayMode(Pet pet)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter a command (play, sleep, wake, learn, speak, exit):");
            Console.WriteLine($"You are playing with: {pet.Name} the {pet.Type}.");
            Console.WriteLine("Type exit to exit play mode \n");

            drawLine();

            string command = Console.ReadLine().ToLower();
            if (command == "exit")
            {
                break;
            }
            else if (command == "play")
            {
                Console.WriteLine("Enter the trick you want to play with your pet: ");
                string trick = Console.ReadLine().ToLower();

                pet.Trick(trick);
            }
            else if (command == "sleep")
            {
                pet.Sleep();
            }
            else if (command == "wake")
            {
                pet.Wake();
            }
            else if (command == "learn")
            {
                Console.WriteLine("Enter the trick you want to teach your pet:");
                string trick = Console.ReadLine();
                pet.LearnTrick(trick);
            }
            else if (command == "speak")
            {
                pet.Speak();
            }

            drawLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}