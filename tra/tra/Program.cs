using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace denis.practica.dictionarincaperi
{
    public class Program
    {
        private Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        private static string jsonFilepath = "rooms.json";

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                jsonFilepath = args[0];
            }

            Program p = new Program();
            p.Meniu();
        }

        public void Meniu()
        {
            string input = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("C - Creare incapere");
                Console.WriteLine("A - Afisare incaperi, in ordinea introducerii");
                Console.WriteLine("O - Afisare incaperi, ordonate dupa nume");
                Console.WriteLine("N - Afisare o incapere cu un anumit index");
                Console.WriteLine("F - Cautare si afisare incaperi cu un anumit nume");
                Console.WriteLine("U - Actualizare incapere de la un anumit index");
                Console.WriteLine("S - Stergere incapere de la un anumit index");
                Console.WriteLine("D - Stergere totala si reinitializare dictionar");
                Console.WriteLine("I - Info cale aplicatie, numar elemente dictionar, indexul ultimei incaperi ");
                Console.WriteLine("W - Salvare dictionar intr-un fisier .json");
                Console.WriteLine("L - Incarcare dictionar din .json");
                Console.WriteLine("X - Iesire");

                string userOption = Console.ReadLine().ToUpper();

                switch (userOption)
                {
                    case "C":
                        Console.WriteLine("C. Creare incapere");
                        CreateRoom();
                        break;

                    case "O":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("Afisare incaperi, ordonate dupa nume");
                            OrderByName();
                        }
                        break;

                    case "N":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("Afisare o incapere cu un anumit index");
                            Console.WriteLine("Index:");
                            input = Console.ReadLine();
                            IsNumberValid(input);
                            int.TryParse(input, out int iInput);

                            SearchByIndex(iInput);
                        }
                        break;

                    case "A":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("\n\nA. Afisare incaperi, in ordinea introducerii");
                            ShowRooms();
                        }

                        break;

                    case "F":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("Cautare si afisare incaperi cu un anumit nume");
                            Console.WriteLine("Nume:");
                            string name = Console.ReadLine();
                            SearchbyName(name);
                        }
                        break;

                    case "U":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("Actualizare incapere de la un anumit index");

                            input = Console.ReadLine();
                            IsNumberValid(input);

                            if (int.TryParse(input, out int indexToUpdate))
                            {
                                UpdateRoomByIndex(indexToUpdate);
                            }
                        }
                        break;

                    case "S":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("Stergere incapere de la un anumit index");
                            input = Console.ReadLine();
                            if (int.TryParse(input, out int indexToUpdate))
                            {
                                if (rooms.ContainsKey(indexToUpdate))
                                {
                                    DeleteByIndex(indexToUpdate);
                                }
                                else
                                {
                                    Console.WriteLine("Nu exista o incapere la acest index!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nu este numar intreg!");
                            }
                        }
                        break;

                    case "D":
                        if (rooms.Keys.Count == 0)
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            Console.WriteLine("Stergere totala");
                            DeleteAll();
                        }
                        break;

                    case "I":
                        Console.WriteLine("I - Info cale aplicatie, numar elemente dictionar, indexul ultimei incaperi ");
                        var path = Directory.GetCurrentDirectory();
                        Console.WriteLine(path);

                        if (!rooms.Any())
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                        }
                        else
                        {
                            int RoomCount = rooms.Count;
                            Console.WriteLine("Nr elemente dictionar : ", RoomCount);
                            int lastIndexKey = rooms.Keys.Last();
                            Console.WriteLine(lastIndexKey);
                        }
                        break;

                    case "W":
                        if (!rooms.Any())
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");

                        }
                        else
                        {
                            CreateJson();
                            Console.WriteLine($"Incaperile au fost salvate in fisierul {jsonFilepath}");
                        }
                        break;

                    case "L":
                        JsonRead();
                        Console.WriteLine($"Incaperile au fost incarcate din fisierul {jsonFilepath}");
                        break;

                    case "X":
                        Console.WriteLine("Iesire din aplicatie");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Comanda introdusa nu se afla in lista.");
                        continue;
                }

                _ = Console.ReadKey(true);
            }
        }

        public string ValName(string str)
        {
            string input;
            do
            {
                Console.WriteLine(str);
                input = Console.ReadLine();

            }
            while (!IsNameValid(input));

            return input;
        }

        public string ValNumber(string str)
        {
            string input;

            do
            {
                Console.WriteLine(str);
                input = Console.ReadLine();

            }
            while (!IsNumberValid(input));

            return input;
        }

        public void CreateRoom()
        {
            string input;
            Room newRoom = new Room();

            if (rooms.Keys.Count > 0)
            {
                newRoom.index = rooms.Keys.Max() + 1;
            }

            input = ValName("Name");
            newRoom.Name = input;

            input = ValNumber("LocationX");
            newRoom.LocationX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = ValNumber("LocationY");
            newRoom.LocationY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = ValNumber("LengthX");
            newRoom.LengthX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = ValNumber("LengthY");
            newRoom.LengthY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);

            rooms.Add(newRoom.index, newRoom);
            Console.WriteLine("vrei sa mai adaugi o incapere? Y/N");

            ConsoleKeyInfo ck;

            while (true)
            {
                ck = Console.ReadKey();
                if (ck.Key == ConsoleKey.Y)
                {
                    CreateRoom();
                }
                Console.WriteLine();
            }
        }

        public bool IsNameValid(string input)
        {
            if (input.All(char.IsDigit))
            {
                Console.WriteLine("Valoarea introdusa nu trebuie sa fie nula sau sa contina DOAR cifre");

                return false;
            }

            return true;
        }

        public bool IsNumberValid(string s)
        {
            float number;
            if (s.Count() == 0)
            {
                Console.WriteLine(" Adauga date, campul nu poate fi gol");
                return false;
            }

            else if (float.TryParse(s, out number))
            {

                if (number < 0)
                {
                    Console.WriteLine("Valoarea nu poate fi mai mica ca 0");
                    return false;
                }
                else if (s.Length >= 6)
                {
                    Console.WriteLine("Valoarea nu poate fi mai mare de 6 cifre");
                    return false;
                }

                return true;
            }
            else
            {
                Console.WriteLine("introdu o valoare de tip float");
            }

            return false;
        }

        public void ShowRooms()
        {
            string s = "Dict";
            int consoleWidth = (Console.WindowWidth - s.Length) / 2;
            string dash = new string('-', consoleWidth);
            s = $"{dash}{s}{dash}";

            Console.WriteLine(s);
            foreach (var room in rooms)
            {
                Console.WriteLine(room);
            }

            Console.WriteLine("\n");
        }

        public void SearchByIndex(int noIndex)
        {
            Console.WriteLine("------------------------Afisare dupa index_____________");

            var indexSearchedVal = rooms.Where(r => r.Key.Equals(noIndex));
            foreach (var room in indexSearchedVal)
            {
                Console.WriteLine(room);
                Console.WriteLine();
            }
        }

        public void SearchbyName(string name)
        {
            Console.WriteLine("------------------------Afisare dupa nume______________");

            var filteredRooms = rooms.Where(r => r.Value.Name.ToLower().Contains(name.ToLower()));

            foreach (var room in filteredRooms)
            {
                Console.WriteLine(room);
            }
        }

        public void OrderByName()
        {
            Console.WriteLine("------------------------Afisare dupa nume ordonate______________");
            var orderByNamelst = rooms.OrderBy(r => r.Value.Name);

            foreach (var room in orderByNamelst)
            {
                Console.WriteLine(room);
            }
        }

        public void UpdateRoomByIndex(int indexUpdate)
        {
            Room newRoom = new Room();

            string input;

            input = ValName("Name");
            newRoom.Name = input;

            input = ValNumber("LocationX");
            newRoom.LocationX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = ValNumber("LocationY");
            newRoom.LocationY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = ValNumber("LengthX");
            newRoom.LengthX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = ValNumber("LengthY");
            newRoom.LengthY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);

            var indexSearchedVal = rooms.Where(r => r.Key == indexUpdate);

            if (indexSearchedVal.Any())
            {
                rooms[indexUpdate] = newRoom;
            }
        }

        public void DeleteByIndex(int indexDelete)
        {
            var indexSearchedVal = rooms.Where(r => r.Key == indexDelete);

            if (rooms.Remove(indexDelete))
            {
                Console.WriteLine($"incaperea de la indexul { indexDelete} a fost stearsa cu success!");
            }
            else
            {
                Console.WriteLine($"nu exista intrari pt indexul { indexDelete}");
            }
        }

        public void DeleteAll()
        {
            rooms.Clear();
        }

        public void CreateJson()
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            using (StreamWriter sw = new StreamWriter(jsonFilepath))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, rooms);
                }
            }
        }

        public void JsonRead()
        {
            if (File.Exists(jsonFilepath))
            {
                string txt = File.ReadAllText(jsonFilepath);

                rooms = JsonConvert.DeserializeObject<Dictionary<int, Room>>(txt);
            }
        }
    }
}
