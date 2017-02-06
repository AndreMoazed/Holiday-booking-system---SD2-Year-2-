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
using MySql.Data.MySqlClient;

namespace SD2CW2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DatabaseFacade dbcon = new DatabaseFacade(); //new instance of database facade so that its methods can be used within Main
        
        public MainWindow()
        {
            InitializeComponent();
            dbcon.OpenCon(); //call open connection method from database facade class
        }

        private void btn_select_cust_Click(object sender, RoutedEventArgs e)
        //Button to select a customer and open a new customer window
        {
            dbcon.Select_Cust(Int32.Parse(txtBox_select_cust.Text));
        }

        private void btn_new_cust_Click(object sender, RoutedEventArgs e)
        {
            new Customer().Show(); //open a new customer window
        }
    }
}
