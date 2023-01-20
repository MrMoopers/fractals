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

namespace Fractal_Factory_V2._0
{

    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public string creationMethod = "";

       public  StartUpWindow()
        {
            InitializeComponent();
        }

        private void BtnDecimal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            creationMethod = "Decimal";

            this.Close();
            //storage

            ////return "Decimal";
            //MainWindow mainWindow = new MainWindow("Decimal");
            //mainWindow.Activate();
        }

        private void BtnDouble_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            creationMethod = "Double";

            this.Close();
            //return "Double";
            //MainWindow mainWindow = new MainWindow("Double");
            //mainWindow.Activate();
        }
    }
}
