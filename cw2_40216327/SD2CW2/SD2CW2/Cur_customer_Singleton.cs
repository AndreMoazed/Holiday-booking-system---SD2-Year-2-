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
    public class Cur_customer_Singleton
    //This class will be used to reduce the number of customers that can be created on the heap
    //Since only one customer can be viewd and changed at one time there is no need to allow there 
    //to exist more than one value of customer on the memory heap
    {
        private static Cur_customer_Singleton instance; //create an instance of the class
        customer_class cust; //calling the customer class and setting it to cust

        public static Cur_customer_Singleton Instance()
        //Used for the creation of an instance of this class
        {
            if (instance == null)
            //the following if statemnts will check if the instance already exist
            //if the instance doesn't exist then a new instance is created
            {
                instance = new Cur_customer_Singleton();
                return instance;
            }
            else
            //otehrwise the current existing instance will be returned
            {
                return instance;
            }
        }

        public static Cur_customer_Singleton Instance(customer_class cust)
        //Instance of customer where the customer data exists
        //a new instance of customer will be made, calling the previous method
        //if the customer instance is already non-existant
        {
            if (instance == null)
            {
                instance = new Cur_customer_Singleton();
                instance.cust = cust;
                return instance;
            }
            else
            //otherwise the existing instance of customer will be called
            {
                instance.cust = cust;
                return instance;
            }
        }

        public int GetRefNum()
        //method to return the reference number of the customer, only one is required
        //at any given time, therefore it can exist in this singleton pattern class
        {
            return cust.Ref;
        }
    }
}

