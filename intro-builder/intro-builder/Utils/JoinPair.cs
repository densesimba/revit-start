using System.Collections.Generic;

namespace eConFaire.RevitBuilder.Intro.Utils
{
    public class JoinPair
    {
        public List<string> FinishesId { get; set; } = new List<string>();
        public List<string> FinishIdTerasa { get; set;} = new List<string>();
        public string WallId { get; set; } = "";
        public JoinPair()
        {
        }
    }
}

