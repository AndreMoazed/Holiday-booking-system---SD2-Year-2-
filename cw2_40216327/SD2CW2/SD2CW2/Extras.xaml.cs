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
using System.Windows.Shapes;

namespace SD2CW2
{
    /// <summary>
    /// Interaction logic for Extras.xaml
    /// </summary>
    public partial class Extras : Window
    {
        DatabaseFacade dbcon = new DatabaseFacade();
        public Extras()
        {
            InitializeComponent();
            dbcon.OpenCon();
        }

        private void btn_load_extras_Click(object sender, RoutedEventArgs e)
        /*
         * A load button made more sense in this scenario as it allows for more efficient less messy code
         * As well as a lower risk of errors occuring via null values
         * As opposed to loading the data in on
         */
        {
            dbcon.load_extras(Int32.Parse(txtBox_booking_ref.Text));
            this.Close();
        }

        private void btn_extra_save_Click(object sender, RoutedEventArgs e)
        {
            int breakfast = 0;
            int evening_meal = 0;
            string start_date = "0001-01-01";
            string end_date = "0001-01-01";
            string car_name = "";

            if(txtBox_breakfast.Text != "")
            {
                breakfast = Int32.Parse(txtBox_breakfast.Text);
            }
            else
            {
                breakfast = 0;
            }

            if(txtBox_evening_meal.Text != "")
            {
                evening_meal = Int32.Parse(txtBox_evening_meal.Text);   
            }
            else
            {
                evening_meal = 0;
            }

            if(datepicker_start.Text != "")
            {
                start_date = Convert.ToDateTime(datepicker_start.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                start_date = "0001-01-01";
            }

            if(datepicker_end.Text != "")
            {
                end_date = Convert.ToDateTime(datepicker_end.Text).ToString("yyyy-MM-dd");
            }
            else
            {
                end_date = "0001-01-01";
            }

            if(txtBox_car_hire_name.Text != "")
            {
                car_name = txtBox_car_hire_name.Text;
            }
            else
            {
                car_name = "";
            }

            if(txtBox_extras_ref.Text == "")
            /*
             * If the textbox containing the extra reference number is empty then the save method should run
             * this takes in the booking reference as an int, the number of breakfasts/evening meals as an int
             * as well as the start and end dates of the car hire, including the name that the car is registered under
             * once again the date time conversion needs to be done becaus eof how c# interacts with MySql
             * the place_extras_ref method will place the newly created extras reference in the extras reference textbox for identification purposes.
             */
            {
                dbcon.save_extras(Int32.Parse(txtBox_booking_ref.Text), breakfast, evening_meal, start_date, end_date, car_name);
                txtBox_extras_ref.Text = dbcon.place_extras_ref().ToString(); //sets the extras_ref text box equal to the result from the DatabaseFaceade method
            }
            else
            /*
             * If there exists a extras reference then the update_extras method will be run, this is like the save method but takes in the
             * extras reference in order to properly update the correct row in the database
             */
            {
                dbcon.update_extras(Int32.Parse(txtBox_booking_ref.Text), breakfast, evening_meal, start_date, end_date, car_name, Int32.Parse(txtBox_extras_ref.Text));
            }
            MessageBox.Show("The extras has been sucessfully saved");
        }

        private void btn_extra_delete_Click(object sender, RoutedEventArgs e)
        /*
         * This method will run the method in the databse facade to delete the row with the corresponding extra reference
         * hence the need to include the value in the extras reference
         */
        {
            dbcon.del_extras(Int32.Parse(txtBox_extras_ref.Text));
        }
    }
}
