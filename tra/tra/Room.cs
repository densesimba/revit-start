using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tra
{
   public class Room
    {

        public int index=0;
        public float LocationX { get; set; }
        public float LocationY { get; set; }

        public float LengthX { get; set; }
        public float LengthY { get; set; }
        public string Name { get; set; }

        public virtual string Show()
        {
          

            return $"nume : {Name} cu locatia : {LocationX} si {LocationY} dar si lunjimi de  {LengthX} x {LengthY}";
        }

    }

   
}
