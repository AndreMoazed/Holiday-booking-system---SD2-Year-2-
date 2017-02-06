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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Window
    {
        DatabaseFacade dbcon = new DatabaseFacade();

        public Customer()
        {
            InitializeComponent();
            dbcon.OpenCon();
        }

        private void btn_cust_save_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_cust_ref.Text == "")
            {
                dbcon.save_cust(txtBox_cust_last.Text, txtBox_cust_first.Text, txtBox_cust_address.Text);
                txtBox_cust_ref.Text = dbcon.place_cust_ref().ToString();
                this.Close();
            }
            if(txtBox_cust_ref.Text != "")
            {
                dbcon.update_cust(Int32.Parse(txtBox_cust_ref.Text), txtBox_cust_last.Text, txtBox_cust_first.Text, txtBox_cust_address.Text);
            }
        }

        private void btn_cust_del_Click(object sender, RoutedEventArgs e)
        {
            dbcon.del_cust(Int32.Parse(txtBox_cust_ref.Text));
            this.Close();
            MessageBox.Show("The customer has been removed.");
        }

        private void btn_new_booking_Click(object sender, RoutedEventArgs e)
        {
            if(txtBox_cust_ref.Text != "") //If there exists a value in the cust_ref text box
            {
                new Booking().Show(); //open the booking window
            }
            else
            {
                MessageBox.Show("The customer must be saved before a new booking can be created.");
            }
        }

        private void btn_select_booking_Click(object sender, RoutedEventArgs e)
        {
            dbcon.Select_booking(Int32.Parse(txtBox_select_booking.Text), Int32.Parse(txtBox_cust_ref.Text));
        }
        
    }
}
