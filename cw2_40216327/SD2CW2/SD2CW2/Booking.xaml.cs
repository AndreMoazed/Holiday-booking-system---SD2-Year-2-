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
    /// Interaction logic for booking.xaml
    /// </summary>
    public partial class Booking : Window
    {
        DatabaseFacade dbcon = new DatabaseFacade();

        private string cust_ref { get; set; }

        public Booking()
        {
            InitializeComponent();
            Cur_customer_Singleton cur_cust = Cur_customer_Singleton.Instance();
            this.cust_ref = cur_cust.GetRefNum().ToString();
            dbcon.OpenCon();
        }

        private void btn_save_booking_Click(object sender, RoutedEventArgs e)
        /*call the method form the database facade
         * In this the date picker needs to be formated to convert it to a date time and then into a string with the format
         * yyyy-MM-dd - M is capitilized as this refers to months rather than minutes
         * this is because the c# and MySQL database types don't mix well and as such ocasionally requires conversions
         */
        {
            if (txtBox_booking_ref.Text == "")
            {
                dbcon.save_booking(Convert.ToDateTime(datepicker_arrival.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(datepicker_dep.Text).ToString("yyyy-MM-dd"), Int32.Parse(txtBox_num_of_guests.Text), Int32.Parse(cust_ref));
                txtBox_booking_ref.Text = dbcon.place_booking_ref().ToString();
            }
            else
            {
                dbcon.update_booking(Convert.ToDateTime(datepicker_arrival.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(datepicker_dep.Text).ToString("yyyy-MM-dd"), Int32.Parse(txtBox_num_of_guests.Text), Int32.Parse(txtBox_booking_ref.Text));
            }
        }

        private void bt_del_booking_Click(object sender, RoutedEventArgs e)
        //button that calls the method from the database facade
        {
            dbcon.del_booking(Int32.Parse(txtBox_booking_ref.Text));
            this.Close();
            MessageBox.Show("Booking has been deleted.");   //message box that confirms deleted booking
        }

        private void btn_view_guests_Click(object sender, RoutedEventArgs e)
        //method is called from the database facade when the button is clicked
        {
            Guests guestWin = new Guests();
            guestWin.txtBox_booking_ref.Text = txtBox_booking_ref.Text;
            guestWin.Show();
        }

        private void btn_extras_Click(object sender, RoutedEventArgs e)
        {
            Extras extrasWin = new Extras();
            extrasWin.txtBox_booking_ref.Text = txtBox_booking_ref.Text;
            extrasWin.Show();
        }

        private void btn_invoice_Click(object sender, RoutedEventArgs e)
        {
            dbcon.view_invoice(Int32.Parse(txtBox_booking_ref.Text));
        }
    }
}
