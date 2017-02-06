/*
 * Author: Andre Moazed         Matricualtion number: 40216327
 * Class description:
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD2CW2
{
    public class booking_class
    {
        private int reference;
        private string arrivaldate;
        private string depedate;
        private string occupants;
        private string num_of_guests;

        public int Ref { get; set; }
        public string Arrivaldate { get; set; }
        public string DepDate { get; set; }
        public string Num_of_guests { get; set; }
        public int CustRef { get; set; }
    }
}
