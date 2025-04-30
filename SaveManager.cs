using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PetManager
{
    internal abstract class SaveManager
    {
        private static string saveFolderPath = null;
        //public static bool saveable = true;
        public static Dictionary<string, string> GetSaveInfo(Pet pet)
        {
            Dictionary<string, string> petInfo = new Dictionary<string, string>();

            petInfo.Add("name",pet.Name);
            petInfo.Add("breed", pet.Breed);
            petInfo.Add("type", pet.Type);
            petInfo.Add("isMale", pet.IsMale.ToString());

            return petInfo;
        }

        //public static List<string> GetTricksLearnt(Pet pet)
        //{
        //    return pet.tricksLearnt;
        //}

        public static string createSaveFileContent(Dictionary<string, string> saveInfo, List<string> tricksLearnt)
        {
            string content = "";

            foreach (var item in saveInfo)
            {
                content += $"{item.Key}:{item.Value}\n";
            }

            content += "\n";
            int i = 0;
            foreach (var trick in tricksLearnt)
            {
                i++;
                content += $"trick{i}:{trick}\n";
            }

            return content;
        }

        public static void SavePetsToFiles(List<Pet> pets)
        {
            if (pets.Count == 0)
            {
                return;
            }

            int i = 0;
            foreach (Pet pet in pets)
            {
                string content = createSaveFileContent(GetSaveInfo(pet), pet.tricksLearnt);
                i++;
                string fileName = $"pet_{i}";
                string filePath = Path.Combine(saveFolderPath, fileName);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(content);
                }
            }
        }

        public static List<Pet> GetPetsFromSaveFile()
        {
            

            List<Pet> pets = new List<Pet>();

            if (saveFolderPath == null) return pets;
            string[] files = Directory.GetFiles(saveFolderPath);
            foreach (string file in files)
            {
                Pet pet = null;
                List<string> tricks = new List<string>();

                using (StreamReader reader = new StreamReader(file))
                {
                    string content = reader.ReadToEnd();
                    string[] lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    string name = null;
                    string breed = null;
                    string type = null;
                    bool isMale = true;

                    //Pet pet;

                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length != 2)
                        {
                            continue;
                        }
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        switch (key)
                        {
                            case "name":
                                name = value;
                                break;
                            case "breed":
                                breed = value;
                                break;
                            case "isMale":
                                bool maleParse = bool.Parse(value);

                                if (maleParse)
                                {
                                    isMale = true;
                                }
                                else
                                {
                                    isMale = false;
                                }
                                //isMale = bool.Parse(value);
                                break;
                            case "type":
                                type = value.ToLower();
                                break;
                            default:
                                if (key.StartsWith("trick"))
                                {
                                    string trick = value;
                                    tricks.Add(trick);
                                    //pet.LearnTrick(trick, false);
                                }
                                break;
                        }

                        switch (type)
                        {
                            case "dog":
                                pet = new Dog(name, breed, isMale);
                                //pets.Add(pet);
                                break;
                        }


                    }
                }

                if (pet != null)
                {
                    foreach (var trick in tricks)
                    {
                        pet.LearnTrick(trick, false);
                    }
                    pets.Add(pet);
                }
            }
            return pets;
        }

        public static string InitSaveFolder()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string l_saveFolderPath = Path.Combine(appDataPath, "tnydevv", "PetManager");

            saveFolderPath = l_saveFolderPath;
            Directory.CreateDirectory(l_saveFolderPath);

            return l_saveFolderPath;
        }
    }
}
