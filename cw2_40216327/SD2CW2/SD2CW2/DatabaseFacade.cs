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
        public MySqlConnection con = new MySqlConnection(SQLConnect); //variable to create a new connection with the SQLconnect connection string
        public MySqlCommand cmd; //a variavle that will be used for SQL commands
        public MySqlDataReader sdr; //the data reader variable
        public string sql; //A new string, sql, that will be used to contain sql queries or sql non-queries 
       
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
                con.Cloase(); //attempt to close the connection
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
            if(con != null && con.State == ConnectionState.Closed)
            //If the connection has a value and the state is unclosed then do the following:
            {
                con.Dispose(); //dispose of the connection
            }
        }
    }
}
