namespace denis.practica.dictionarincaperi
{
    public class Room
    {
        public int index = 1;
        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float LengthX { get; set; }
        public float LengthY { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $" Camera cu Numele = {Name},  este la Locatia X = {LocationX} Y = {LocationY} cu  Length X= {LengthX} si Y= {LengthY} ";
        }
    }
}
