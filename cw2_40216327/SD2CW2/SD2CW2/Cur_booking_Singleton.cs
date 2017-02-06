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
    public class Cur_booking_Singleton
    //This class will be used to reduce the number of bookings that can be created on the heap
    //Since only one booking can be viewd and changed at one time there is no need to allow there 
    //to exist more than one value of booking on the memory heap
    {
        private static Cur_booking_Singleton instance;
        booking_class book; //calling the booking class and setting it to book
        public string bookingref { get; set; }

        public static Cur_booking_Singleton Instance()
        {
            if (instance == null)
            {
                instance = new Cur_booking_Singleton();
                return instance;
            }
            else
            {
                return instance;
            }
        }

        public static Cur_booking_Singleton Instance(booking_class book)
        {
            if (instance == null)
            {
                instance = new Cur_booking_Singleton();
                instance.book = book;
                return instance;
            }
            else
            {
                instance.book = book;
                return instance;
            }
        }

        public int GetRefNum()
        {
            return book.Ref;
        }
    }
}
