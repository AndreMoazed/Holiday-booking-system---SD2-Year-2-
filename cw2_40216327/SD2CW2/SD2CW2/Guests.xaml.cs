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
    /// Interaction logic for Guests.xaml
    /// </summary>
    public partial class Guests : Window
    {
        DatabaseFacade dbcon = new DatabaseFacade();
        public Guests()
        {
            InitializeComponent();
            dbcon.OpenCon();
        }


        private void btn_load_guests_Click(object sender, RoutedEventArgs e)
        {
            if (dbcon.guest_exist(Int32.Parse(txtBox_booking_ref.Text)) != null)
            {
                dbcon.load_guests(Int32.Parse(txtBox_booking_ref.Text));
            }
            else
            {
                MessageBox.Show("No guests exist in this booking.");
            }
            this.Close();
        }

        private void btn_g1_save_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_g1_pass_num.Text != dbcon.pass_num_exists(txtBox_g1_pass_num.Text))
            {
                dbcon.save_guest(txtBox_g1_pass_num.Text, txtBox_g1_last.Text, txtBox_g1_first.Text, Convert.ToDateTime(datepicker_g1_DoB.Text).ToString("yyy-MM-dd"), txtBox_g1_diet_req.Text, Int32.Parse(txtBox_booking_ref.Text));
            }
            else
            {
                dbcon.update_guest(txtBox_g1_pass_num.Text, txtBox_g1_last.Text, txtBox_g1_first.Text, Convert.ToDateTime(datepicker_g1_DoB.Text).ToString("yyy-MM-dd"), txtBox_g1_diet_req.Text);
            }
        }

        private void btn_g2_save_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_g2_pass_num.Text != dbcon.pass_num_exists(txtBox_g2_pass_num.Text))
            {
                dbcon.save_guest(txtBox_g2_pass_num.Text, txtBox_g2_last.Text, txtBox_g2_first.Text, Convert.ToDateTime(datepicker_g2_DoB.Text).ToString("yyy-MM-dd"), txtBox_g2_diet_req.Text, Int32.Parse(txtBox_booking_ref.Text));
            }
            else
            {
                dbcon.update_guest(txtBox_g2_pass_num.Text, txtBox_g2_last.Text, txtBox_g2_first.Text, Convert.ToDateTime(datepicker_g2_DoB.Text).ToString("yyy-MM-dd"), txtBox_g2_diet_req.Text);
            }
        }

        private void btn_g3_save_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_g3_pass_num.Text != dbcon.pass_num_exists(txtBox_g3_pass_num.Text))
            {
                dbcon.save_guest(txtBox_g3_pass_num.Text, txtBox_g3_last.Text, txtBox_g3_first.Text, Convert.ToDateTime(datepicker_g3_DoB.Text).ToString("yyy-MM-dd"), txtBox_g3_diet_req.Text, Int32.Parse(txtBox_booking_ref.Text));
            }
            else
            {
                dbcon.update_guest(txtBox_g3_pass_num.Text, txtBox_g3_last.Text, txtBox_g3_first.Text, Convert.ToDateTime(datepicker_g3_DoB.Text).ToString("yyy-MM-dd"), txtBox_g3_diet_req.Text);
            }
        }

        private void btn_g4_save_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_g4_pass_num.Text != dbcon.pass_num_exists(txtBox_g4_pass_num.Text))
            {
                dbcon.save_guest(txtBox_g4_pass_num.Text, txtBox_g4_last.Text, txtBox_g4_first.Text, Convert.ToDateTime(datepicker_g4_DoB.Text).ToString("yyy-MM-dd"), txtBox_g4_diet_req.Text, Int32.Parse(txtBox_booking_ref.Text));
            }
            else
            {
                dbcon.update_guest(txtBox_g4_pass_num.Text, txtBox_g4_last.Text, txtBox_g4_first.Text, Convert.ToDateTime(datepicker_g4_DoB.Text).ToString("yyy-MM-dd"), txtBox_g4_diet_req.Text);
            }
        }

        private void btn_g1_delete_Click(object sender, RoutedEventArgs e)
        {
            dbcon.del_guest(txtBox_g1_pass_num.Text);
        }

        private void btn_g2_delete_Click(object sender, RoutedEventArgs e)
        {
            dbcon.del_guest(txtBox_g2_pass_num.Text);
        }

        private void btn_g3_delete_Click(object sender, RoutedEventArgs e)
        {
            dbcon.del_guest(txtBox_g3_pass_num.Text);
        }

        private void btn_g4_delete_Click(object sender, RoutedEventArgs e)
        {
            dbcon.del_guest(txtBox_g4_pass_num.Text);
        }

    }
}
