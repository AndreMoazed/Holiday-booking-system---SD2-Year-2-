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
    class guest
    {
        private string lastname;
        private string firstname;
        private string pass_num;
        private string dob;
        private string dietreq;
        private int booking_ref;

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string Pass_num { get; set; }
        public string DoB { get; set; }
        public string Dietreq { get; set; }
        public int Booking_ref { get; set; }
    }
}
