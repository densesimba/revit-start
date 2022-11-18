using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using System.Globalization;

namespace tra
{
    class Program
    {
        Dictionary<int, Room> rooms = new Dictionary<int, Room>();
        string pathJson = @"D:\Deni\ecfsrc\revit-start\tra\tra\json.json";


        static void Main(string[] args)
        {

            Program p = new Program();
           

            p.Meniu();

        }

        public void Meniu()
        {
            Console.WriteLine("");

            while (true)
            {

                Console.WriteLine("\n\nC . Creare incapere");
                Console.WriteLine("A . Afisare incaperi, in ordinea introducerii");
                Console.WriteLine("O - Afisare incaperi, ordonate dupa nume");
                Console.WriteLine("N - Afisare o incapere cu un anumit index");
                Console.WriteLine("F - Cautare si afisare incaperi cu un anumit nume");
                Console.WriteLine("U - Actualizare incapere de la un anumit index");
                Console.WriteLine("S. Stergere incapere de la un anumit index");
                Console.WriteLine("D. Stergere totala si reinitializare dictionar");
                Console.WriteLine("I - Info cale aplicatie, numar elemente dictionar, indexul ultimei incaperi ");
                Console.WriteLine("W. Salvare dictionar intr-un fisier .json");
                Console.WriteLine("L.Incarcare dictionar din .json\n\n");
                Console.WriteLine("Camere : {0} ", rooms.Keys.Count.ToString());

                string n = Console.ReadLine().ToUpper();




                switch (n)
                {
                    case "C":

                        Console.WriteLine("C. Creare incapere");

                        CreateRoom();

                        break;

                    case "O":


                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Afisare incaperi, ordonate dupa nume");
                            OrderByName();
                            break;
                        }


                    case "N":

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("N- Afisare o incapere cu un anumit index");
                            Console.WriteLine("Index:");
                            int nrIndex = Convert.ToInt32(Console.ReadLine());

                            SearchByIndex(nrIndex);

                            break;
                        }

                    case "A":

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\n\nA. Afisare incaperi, in ordinea introducerii");
                            ShowRooms();

                            break;
                        }

                    case "F":

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("F - Cautare si afisare incaperi cu un anumit nume");
                            Console.WriteLine("Nume:");
                            string name = Console.ReadLine();
                            SearchbyName(name);
                            break;
                        }

                    case "U":

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Actualizare incapere de la un anumit index");

                            int indexToUpdate = Convert.ToInt32(Console.ReadLine());
                            UpdateRoomByIndex(indexToUpdate);
                            break;
                        }

                    case "S":

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Stergere incapere de la un anumit index");
                            int indexToDelete = Convert.ToInt32(Console.ReadLine());
                            DeleteByIndex(indexToDelete);
                            break;
                        }

                    case "D":

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Stergere totala");
                            DeleteAll();
                            break;
                        }


                    case "I":

                        Console.WriteLine("I - Info cale aplicatie, numar elemente dictionar, indexul ultimei incaperi ");
                        var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                        Console.WriteLine(path);

                        int RoomCount = rooms.Count;
                        Console.WriteLine("Nr elemente dictionar : ", RoomCount);

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            int lastIndexKey = rooms.Keys.Last();
                            Console.WriteLine(lastIndexKey);

                            break;
                        }

                    case "W":

                        Console.WriteLine("W. Salvare dictionar intr-un fisier .json");

                        if (rooms.Keys.Count.ToString() == "0")
                        {
                            Console.WriteLine("NU sunt intrari in dictionar \n");
                            break;
                        }
                        else
                        {
                            CreateJson();
                            break;
                        }

                    case "L":


                        JsonRead();

                        break;

                    default:
                        Console.WriteLine("Comanda introdusa nu se afla in lista.");
                        continue;
                }
            }
        }

        public string valName(string str)
        {
            string input;
            do
            {
                Console.WriteLine(str);
                input = Console.ReadLine();

            }
            while (!ValidateName(input));

            return input;
        }

        public string valNumber(string str)
        {

            string input;

            do
            {

                Console.WriteLine(str);
                input = Console.ReadLine();

            }
            while (!ValidateNumber(input));

            return input;
        }

        public void CreateRoom()
        {

            string input;

            Room newRoom = new Room();

            // x++;
            // newRoom.index = x;
            if (rooms.Keys.Count.ToString() == "0")
            {
                newRoom.index = 1;
            }
            else
            {
                newRoom.index = rooms.Keys.Max() + 1;
            }




            Console.WriteLine("Id- ul este {0} :", newRoom.index);

            input = valName("Name");
            newRoom.Name = input;

            input = valNumber("LocationX");
            newRoom.LocationX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = valNumber("LocationY");
            newRoom.LocationY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = valNumber("LengthX");
            newRoom.LengthX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = valNumber("LengthY");
            newRoom.LengthY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);


            rooms.Add(newRoom.index, newRoom);

            string jsonOutput = JsonConvert.SerializeObject(newRoom);

            Console.WriteLine("vrei sa mai adaugi o incapere? Y/N");

            ConsoleKeyInfo ck;

            while (true)
            {
                ck = Console.ReadKey();
                if (ck.Key == ConsoleKey.Y)
                {

                    CreateRoom();
                }
                else if (ck.Key == ConsoleKey.N)
                {
                    Meniu();

                }
                Console.WriteLine();

            }
        }

        public bool ValidateName(string input)
        {
            if (input.All(char.IsDigit))
            {
                Console.WriteLine("Valoarea introdusa nu trebuie sa fie nula sau sa contina DOAR cifre");
                // Console.WriteLine("nume : ");

                return false;

            }

            return true;
        }

        public bool ValidateNumber(string input)
        {
            float inp;
            if (input.Count() == 0)
            {
                Console.WriteLine(" Adauga date.");
                return false;
            }
           
            else if (float.TryParse(input, out inp))
            {
               
                 if (inp < 0)
                {
                    Console.WriteLine("Valoarea nu poate fi mai mica ca 0");
                    return false;
                }
                else if (input.Count() >= 6)
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
            Console.WriteLine("-----------------------Dict______________");
            foreach (var room in rooms)
            {

                Console.WriteLine(" KEY ={0} ||index = {1}, Nume = {2}, LocationX = {3}", room.Key, room.Value.index, room.Value.Name, room.Value.LocationX);

            }

            Console.WriteLine("\n");
        }

        public void SearchByIndex(int noIndex)
        {

            Console.WriteLine("------------------------Afisare dupa index_____________");

            var indexSearchedVal = rooms.Where(r => r.Key.Equals(noIndex));
            foreach (var room in indexSearchedVal)
            {
                Console.WriteLine("ID = {0}, Nume = {1}", room.Key, room.Value.Name);
                Console.WriteLine();
            }

        }

        public void SearchbyName(string name)
        {
            Console.WriteLine("------------------------Afisare dupa nume______________");

            var filteredRooms = rooms.Where(r => r.Value.Name.ToLower().Contains(name.ToLower()));

            foreach (var room in filteredRooms)
            {
                Console.WriteLine("ID = {0}, Nume = {1}", room.Key, room.Value.Name);
            }

        }

        public void OrderByName()
        {

            Console.WriteLine("------------------------Afisare dupa nume ordonate______________");


            var orderByNamelst = rooms.OrderBy(r => r.Value.Name);

            foreach (var room in orderByNamelst)
            {
                Console.WriteLine("ID = {0}, Nume = {1}", room.Key, room.Value.Name);
            }
        }

        public void UpdateRoomByIndex(int indexUpdate)
        {
            Room newRoom = new Room();

            string input;

            input = valName("Name");
            newRoom.Name = input;

            input = valNumber("LocationX");
            newRoom.LocationX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = valNumber("LocationY");
            newRoom.LocationY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = valNumber("LengthX");
            newRoom.LengthX = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
            input = valNumber("LengthY");
            newRoom.LengthY = float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);

            var indexSearchedVal = rooms.Where(r => r.Key == indexUpdate);

            if (indexSearchedVal.Any())
                rooms[indexUpdate] = newRoom;

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
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(pathJson))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, rooms);
                }
            }
        }


        public void JsonRead()
        {
           // JsonSerializer serializer = new JsonSerializer();

            using (StreamReader r = new StreamReader(pathJson))
            {
                if (File.Exists(pathJson))
                {

                    string txt = File.ReadAllText(pathJson);

                    rooms = JsonConvert.DeserializeObject<Dictionary<int, Room>>(txt);



                }
            }


        }
    }
}



