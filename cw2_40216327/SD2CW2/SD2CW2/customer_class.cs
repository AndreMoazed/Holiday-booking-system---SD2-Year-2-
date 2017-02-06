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
    public class customer_class
    {
        private string lastname;
        private string firstname;
        private int reference;
        private string address;
        
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
        public int Ref { get; set; }
        public string Address { get; set; }
    }
}
