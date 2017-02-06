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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
//TO LOAD DB
//cd C:\xampp\mysql\bin
//mysql -u root -p --database=40216327

//TO RELOAD
//\. 40216327.SQL


namespace SD2CW2
{
    public class DatabaseFacade //all methods within this class will be public in order for them to be called in other classes
    {
        /* 
         * The next line will be used to connect to the database that will be used in the program
         * It is a SQL connection string that connects to the localhost server (the server on this laptop)
         * It will automatically use the user id root, standard for when dealing with localhost servers
         * The password has been set to password in the server and so is automatically inputed by the SQL connection string
         * The database to be used is called 40216327 (my matriculation number) and the ER diagram for this database can be found in the included pdf
         */
        private static string SQLConnect = "server=localhost;user id=root;password=password;database=40216327;persistsecurityinfo=False";
        private MySqlConnection con = new MySqlConnection(SQLConnect); //variable to create a new connection with the SQLconnect connection string
        private MySqlCommand cmd; //a variavle that will be used for SQL commands
        private MySqlDataReader sdr; //the data reader variable
        private string sql; //A new string, sql, that will be used to contain sql queries or sql non-queries 

        public DatabaseFacade()
        //constructor method
        {
            cmd = new MySqlCommand(sql, con); //constructor will create a new instance of an sql command that will take in a string for queries/non-queries and that will use the connection string.
        }

        public bool OpenCon()
        //opening the database connection method
        {
            try //error handeling to ensure the connection is opened
            {
                con.Open(); //open the connection to the database
                return true; //If the connection was sucessfully opened then set the boolean value to true
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //show the exception message in a message box
                return false;   //set the value to false
            }
        }

        public bool CloseCon()
        //closing the connection to the database method
        {
            try
            {
                con.Close(); //attempt to close the connection
                return true; //if connection is closed set value to true 
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message); //Show the exeption message is the connection fails to close
                return false; //the boolean value is set to false to indicate an unsucesessful close
            }
        }

        public MySqlCommand InitSqlCommand(string sql)
        //Method that will allow the sql string to be used
        {
            cmd = new MySqlCommand(sql, con); //setting the cmd (command) to include the sql string and the connection to the database
            return cmd;
        }

        public void End()
        //Method to dispose of any database conenction as this cannot be done by the c# garabge collector
        {
            if (con != null && con.State == ConnectionState.Closed)
            //If the connection has a value and the state is unclosed then do the following:
            {
                con.Dispose(); //dispose of the connection
            }
        }

        public customer_class Select_Cust(int cust_ref)
        {
            //cust_ref = Int32.Parse(txtBox_select_cust.Text); //sets the value of variable cust ref to what is in the select text box
            bool custExists = false;
            sql = "SELECT * FROM customer WHERE cust_ref = " + cust_ref + ";"; //Sql query to set the value of the sql variable in the database_facade
            cmd.CommandText = sql; //set the cmd command text to the sql statement
            sdr = cmd.ExecuteReader(); // set the data reader equal tot the execute reader method
            customer_class cust = new customer_class();
            while (sdr.Read())
            {
                if (sdr.GetString(0) == cust_ref.ToString()) //if the value of the first column of the database table is equal to the customer reference
                {
                    custExists = true;
                    cust.Ref = Int32.Parse(sdr.GetString(0)); //Get the value of the first column from the database table
                    cust.LastName = sdr.GetString(1); //Get the value of the second column from the database table
                    cust.Firstname = sdr.GetString(2);
                    cust.Address = sdr.GetString(3);
                }
            }

            if (custExists == false) //if the customer does not exist
            {
                MessageBox.Show("Customer does not exist. Try a diffrent customer reference");
            }
            else
            {
                Cur_customer_Singleton cur_cust = Cur_customer_Singleton.Instance(cust);
                Customer cust_Win = new Customer(); //create a new customer window
                //cust_Win.Owner = this;
                cust_Win.Show();

                /* 
                 * The next lines will set the value of the text boxes to the previously save values
                 * This will make the text boxes fill in with their correct values
                 */
                cust_Win.txtBox_cust_ref.Text = cust.Ref.ToString();    
                cust_Win.txtBox_cust_last.Text = cust.LastName;
                cust_Win.txtBox_cust_first.Text = cust.Firstname;
                cust_Win.txtBox_cust_address.Text = cust.Address;
            }
            sdr.Close(); //closes the data reader - prevents leaks 
            return cust;
        }

        public void save_cust(string lastname, string firstname, string address)
        /*This method will take in a non-query and insert values into the database table
         * It will take in the values from the text boxes in the customer window
         * This is used for updating a customer and therfore will take in customer lastname, firstname, address and the customer reference
         */
        {
            /*
             * the next line of code is used by the database and is a simple INSERT statement that will insert the values taken in by the method (the
             * values in the textboxes from the customer window) and place them into the sql non-query
             */
            sql = @"INSERT INTO customer VALUES(
                  cust_ref,'" + lastname + "','" + firstname + "','" + address + "');";
            cmd.CommandText = sql;  
            cmd.ExecuteNonQuery();  //once again this is a non-query and therefore mus be executed as such
        }

        public int place_cust_ref()
        {
            int result = 0;
            sql = "SELECT MAX(cust_ref) FROM customer;"; //ovverides the sql cariable contents
            cmd.CommandText = sql; //set command text equal to the sql variable
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                result = Int32.Parse(sdr.GetString(0)); //set variable equal to the first column of the table
            }
            sdr.Close();
                
            //creating a new instance of the customer singelton 
            customer_class cust = Select_Cust(result);
            Cur_customer_Singleton cust_sin = Cur_customer_Singleton.Instance(cust);

            return result;
        }
        
        public void update_cust(int cust_ref, string lastname, string firstname, string address)
       /*
        * this method will be called by the customer window class and it will be used to update the MySql database given that the customer reference
        * is existing. The UPDATE allows for changing of data, the cust_ref is used to determine which row will be changed
        * the row to be changed will be that of the row containing the customer reference number
        */
        {
                sql = @"UPDATE customer SET
                        cust_lastname='" + lastname + "',"
                        + "cust_firstname='" + firstname + "',"
                        + "cust_address='" + address + "'"
                        + "WHERE cust_ref=" + cust_ref + ";";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery(); //Non-query being executed
        }

        public void del_cust(int cust_ref)
        /*
         * Method to delete rows from a database entity (table), this delete method will remove a row from the
         * customer table and will remove the row with the corresponding customer reference (essentially deleting all records of the customer
         * form the system)
         */
        {
            sql = "DELETE FROM customer WHERE cust_ref='" + cust_ref + "';";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }



        public booking_class Select_booking(int booking_ref, int cust_ref)
        //Method to select the booking, since each booking has a customer associated with it the customer reference must also be used
        {
            Cur_customer_Singleton cur_cust = Cur_customer_Singleton.Instance();
            sql = "SELECT * FROM booking WHERE booking_ref=" + booking_ref + " AND cust_ref=" + cust_ref + ";"; //Query to find the correct booking given the correct customer reference
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();
            bool bookExists = false;
            booking_class book = new booking_class();

            while(sdr.Read()) //while the data reader is reading through the database
            {
                if(sdr.GetString(0) == booking_ref.ToString())
                /*
                 * This if statement will ensure that the booking reference in the database will be equal to the booking reference in the c#
                 * The booking class will have the data from the database saved to the corresponding variable in the booking_class
                 * Ref = first column of the database (i.e the booking reference) etc.
                 */
                {
                    bookExists = true;
                    book.Ref = Int32.Parse(sdr.GetString(0));
                    book.Arrivaldate = sdr.GetString(1);
                    book.DepDate = sdr.GetString(2);
                    book.Num_of_guests = sdr.GetString(3);
                }
            }
            sdr.Close(); //data reader is no longer in use, therfore close it

            if(bookExists == false)
            //Error checking ensuring that the booking reference exists. 
            {
                MessageBox.Show("Booking does not exist given the selected reference number.");
            }
            else
            {
                Cur_booking_Singleton cur_booking = Cur_booking_Singleton.Instance(book);
                Booking bookingWin = new Booking(); //creating a new booking window
                bookingWin.Show(); //open the booking window

                bookingWin.txtBox_booking_ref.Text = book.Ref.ToString(); //set the text box value, booking ref, to the booking reference
                bookingWin.datepicker_arrival.Text = book.Arrivaldate;  //The arrival date is already a string and therefore does not need to be converted into a string
                bookingWin.datepicker_dep.Text = book.DepDate;
                bookingWin.txtBox_num_of_guests.Text = book.Num_of_guests;
            }
            return book;
        }


        public void save_booking(string arrival_date, string dep_date, int num_of_guests, int cust_ref)
        {
            /*
             * sql non-query that inserts values of into the database, the insert will not need to take in a booking reference as this is auto incremented
             * in the database itself. The arrival date and departure dates need to be inserted as strings as the date time conversions between c# and MySql 
             * do not work fully. 
             */
            sql = @"INSERT INTO booking VALUES(             
                  booking_ref,'" + arrival_date + "','" + dep_date + "'," + num_of_guests + "," + cust_ref + ");";

            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public int place_booking_ref()
        /*
         * This method will place the newly generated booking refernce into the booking reference text box
         */
        {
            int result = 0; //since the result of the query needs to be returned and is going to be an int the result varible is set as an int
            sql = "SELECT MAX(booking_ref) FROM booking;";  //This selects the most recently created booking reference from the database
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                result = Int32.Parse(sdr.GetString(0));
            }
            sdr.Close();
            return result; //returns the result
        }
        
        public void update_booking(string arrival_date, string dep_date, int num_of_guests, int booking_ref)
        /*
         * Another update method, however this one is used for the booking class
         * It uses the same prinicples as all other update methods, taking in the set of values that need to be updated in the 
         * databse and used an execute non-query command, where the unique identifier of the non-query will be the booking_ref
         */
        {
            sql =   @"UPDATE booking SET
                    arrival_date='" + arrival_date +
                    "',departure_date='" + dep_date +
                    "',num_of_guests='" + num_of_guests +
                    "'WHERE booking_ref=" + booking_ref + ";";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void del_booking(int booking_ref)
        /*
         * Removes the row of data from the booking table where the booking reference is equal to the booking_ref 
         */
        {
            sql = "DELETE FROM booking WHERE booking_ref=" + booking_ref + ";";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public List<string> guest_exist(int booking_ref)
        {
            sql = "SELECT pass_num FROM guest WHERE booking_ref=" + booking_ref + ";";
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();

            List<string> pass_numbers = new List<string>();

            while(sdr.Read())
            {
                pass_numbers.Add(sdr.GetString(0));
            }
            sdr.Close();

            return pass_numbers;
        }

        public string pass_num_exists(string pass_num)
        {
            sql = "SELECT pass_num FROM guest WHERE pass_num='" + pass_num + "';";
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();

            while(sdr.Read())
            {
                pass_num = sdr.GetString(0);
            }
            sdr.Close();

            if (pass_num == "")
            {
                return null;
            }
            else
            {
                return pass_num;
            }
        }

        public void load_guests(int booking_ref)                        //NEED TO FIND A WAY TO CHECK IF THERE ARE ANY GUESTS EXISTING
        /*
         * This view method will be slightly different as it will need to load multiple different text boxes from multiple different rows
         * in the database, within the same method. In order to achieve this a list will be used to iterate through all the four possible guests
         */
        {
            Cur_booking_Singleton curbooking = Cur_booking_Singleton.Instance();        //create a new instance of booking, as only one should be used at one time
            sql = "SELECT * FROM guest WHERE booking_ref=" + booking_ref + ";"; //SELECT statement tha searches for the guests with the correct booking reference
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();
            
            List<guest> guestList = new List<guest>();  //creation of the list 

            while(sdr.Read())
            {
                if(sdr.GetString(5) == booking_ref.ToString()) //if the value of the 5th column in the database table is equal to the booking ref then
                /*
                 * Same as previous reads however this time the identifier is the foreign key booking_reference as there may be multiple guests that need to be loaded in
                 */
                {
                    guest guest = new guest();
                    guest.Pass_num = sdr.GetString(0);
                    guest.LastName = sdr.GetString(1);
                    guest.Firstname = sdr.GetString(2);
                    guest.DoB = sdr.GetString(3);
                    guest.Dietreq = sdr.GetString(4);
                    guest.Booking_ref = booking_ref;
                    guestList.Add(guest); //adding all the values from the previous lines into the list, naming each group of values "guest"
                }
            }
            sdr.Close();

            Guests guestsWin = new Guests(); //creating a new guest window
            guestsWin.txtBox_booking_ref.Text = booking_ref.ToString(); //inserting the booking reference into the booking refernce text box in the guest window

            for(int i = 0; i < guestList.Count(); i++)
            /*
             * This for loop is used to iterate through the possible data that can be loaded into the guest window
             * It will only be the size of the count of the guest list. Therefore the data wont be loaded in if the data for the guest does not exist.
             */
            {
                if(i == 0)
                //for the first item in the list set the follwoing text boxes to the values in the list
                {
                    guestsWin.txtBox_g1_pass_num.Text = guestList[i].Pass_num; //i.e set the passs number text box to be equal to the passpot number of guest 1
                    guestsWin.txtBox_g1_last.Text = guestList[i].LastName;
                    guestsWin.txtBox_g1_first.Text = guestList[i].Firstname;
                    guestsWin.datepicker_g1_DoB.Text = guestList[i].DoB;
                    guestsWin.txtBox_g1_diet_req.Text = guestList[i].DoB;
                }
                if(i == 1)
                {
                    guestsWin.txtBox_g2_pass_num.Text = guestList[i].Pass_num;
                    guestsWin.txtBox_g2_last.Text = guestList[i].LastName;
                    guestsWin.txtBox_g2_first.Text = guestList[i].Firstname;
                    guestsWin.datepicker_g2_DoB.Text = guestList[i].DoB;
                    guestsWin.txtBox_g2_diet_req.Text = guestList[i].DoB;
                }
                if(i == 2)
                {
                    guestsWin.txtBox_g3_pass_num.Text = guestList[i].Pass_num;
                    guestsWin.txtBox_g3_last.Text = guestList[i].LastName;
                    guestsWin.txtBox_g3_first.Text = guestList[i].Firstname;
                    guestsWin.datepicker_g3_DoB.Text = guestList[i].DoB;
                    guestsWin.txtBox_g3_diet_req.Text = guestList[i].DoB;
                }
                if(i == 3)
                {
                    guestsWin.txtBox_g4_pass_num.Text = guestList[i].Pass_num;
                    guestsWin.txtBox_g4_last.Text = guestList[i].LastName;
                    guestsWin.txtBox_g4_first.Text = guestList[i].Firstname;
                    guestsWin.datepicker_g4_DoB.Text = guestList[i].DoB;
                    guestsWin.txtBox_g4_diet_req.Text = guestList[i].DoB;
                }
            }

            sdr.Close();

        }

        public void update_guest(string pass_num, string lastname, string firstname, string DoB, string diet_req)
        {
            sql =   @"UPDATE guest SET" +
                    "pass_num='" + pass_num +
                    "',guest_lastname='" + lastname +
                    "',guest_firstname='" + firstname +
                    "',guest_DoB='" + DoB +
                    "',guest_diet_req='" + diet_req +
                    "' WHERE pass_num='" + pass_num + "';";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void save_guest(string pass_num, string lastname, string firstname, string DoB, string diet_req, int booking_ref)
        {
            sql = @"INSERT INTO guest VALUES('" + pass_num + "','" + lastname + "','"
                        + firstname + "','" + DoB + "','" + diet_req + "'," + booking_ref + ";";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void del_guest(string pass_num)
        {
            if (pass_num != "")
            {
                sql = "DELETE FROM guest WHERE pass_num='" + pass_num + "';";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("This customer does not exist or hasn't been saved and therefore cannot be deleted");
            }
        }


        public extras load_extras(int booking_ref)
        {
            sql = "SELECT * FROM extras WHERE booking_ref='" + booking_ref + "';";
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();
            extras extra = new extras();

            while(sdr.Read())
            {
                if(booking_ref == Int32.Parse(sdr.GetString(7)))
                {
                    extra.Ref = Int32.Parse(sdr.GetString(0));
                    extra.Breakfast_price = Double.Parse(sdr.GetString(1));
                    extra.Breakfast_num = Int32.Parse(sdr.GetString(2));
                    extra.Evening_meal_price = Double.Parse(sdr.GetString(3));
                    extra.Evening_meal_num = Int32.Parse(sdr.GetString(4));
                    extra.Car_hire_price = Double.Parse(sdr.GetString(5));
                    extra.Car_hire_start = sdr.GetString(6);
                    extra.Car_hire_end = sdr.GetString(7);
                    extra.Car_hire_name = sdr.GetString(8);
                    extra.BookingRef = Int32.Parse(sdr.GetString(9));
                }
            }
            sdr.Close();
            return extra;

            Extras extraWin = new Extras();
            extraWin.Show();

            //extraWin.txtBox_extras_ref.Text = extra.Ref.ToString();
            extraWin.txtBox_booking_ref.Text = booking_ref.ToString();
            extraWin.txtBox_breakfast.Text = extra.Breakfast_num.ToString();
            extraWin.txtBox_evening_meal.Text = extra.Evening_meal_num.ToString();
            extraWin.datepicker_start.Text = extra.Car_hire_start;
            extraWin.datepicker_end.Text = extra.Car_hire_end;
            extraWin.txtBox_car_hire_name.Text = extra.Car_hire_name;
            if(extraWin.datepicker_start.Text == "0001-01-01")
            {
                extraWin.datepicker_start.Text = "";
            }
            if(extraWin.datepicker_end.Text == "0001-01-01")
            {
                extraWin.datepicker_end.Text = "";
            }
        }

        public void save_extras(int booking_ref, int breakfasts, int eveningmeals, string carhire_start, string carhire_end, string carhire_name)
        {
            sql =   @"INSERT INTO extras VALUES(extra_ref,5.00," + breakfasts + ",15.00,"
                    + eveningmeals + ",50.00,'" + carhire_start + "','" + carhire_end + "','" 
                    + carhire_name + "'," + booking_ref + ");";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

            public int place_extras_ref()
        {
            int result = 0;
            sql = "SELECT MAX(extra_ref) FROM extras;";
            cmd.CommandText = sql;
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                result = Int32.Parse(sdr.GetString(0));
            }
            sdr.Close();
            return result;
        }

            public void update_extras(int booking_ref, int breakfasts, int eveningmeals, string carhire_start, string carhire_end, string carhire_name, int extra_ref)
        {
            sql = @"UPDATE extras SET" 
                  + "breakfast_num=" + breakfasts + ",evening_meal_num" + eveningmeals
                  + "car_hire_start='" + carhire_start + "',car_hire_end='" + carhire_end + "'"
                  + ",car_hire_name='" + carhire_name + "' WHERE booking_ref=" + booking_ref 
                  + " AND extra_ref=" + extra_ref + ";";
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        public void del_extras(int extra_ref)
        {
            if (extra_ref.ToString() != "")
            {
                sql = "DELETE FROM extras WHERE extra_ref='" + extra_ref + "';";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("This extra does not exist or hasn't been saved and therefore cannot be deleted");
            }
        }

        public void view_invoice(int booking_ref)
        {

        }
    }
}
