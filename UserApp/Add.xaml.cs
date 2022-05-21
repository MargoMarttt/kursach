using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using TCPConnectionAPIClientModule_C_sharp_;

namespace Front_End_Three
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        string fileName;
        bool isEmpty = true, isEmptyTwo = true, isEmptyThree = true;
        private IAdminAccess module;
        public Add(IAdminAccess module)
        {
            fileName = ConfigurationManager.AppSettings.Get("defaultPhotoPath");
            this.module = module;
            InitializeComponent();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                fileName = op.FileName;
            }
        }

         private void Add_Click(object sender, RoutedEventArgs e)
         {
            decimal CostValue;
            int VendorCodeValue;
            try
            {
                VendorCodeValue = int.Parse(VendorCode.Text);
                CostValue = decimal.Parse(Cost.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный ввод!");
                return;
            }
      
            Random random = new Random();
            var value = (random.Next(0, 9).ToString() + "," + random.Next(0,9).ToString());
            var answer = module.CreateCar(new DatabaseEntities.Car()
            {
                Name = Name.Text,
                Cost = CostValue,
                VendorCode = VendorCodeValue,
                Photo = new System.Drawing.Bitmap(fileName),
                TotalRate = double.Parse(value)
            });
            switch (answer)
            {
                case ClassLibraryForTCPConnectionAPI_C_sharp_.AnswerFromServer.Successfully:
                    {
                        MessageBox.Show("Успешно!");
                        break;
                    }
                case ClassLibraryForTCPConnectionAPI_C_sharp_.AnswerFromServer.Error:
                    {
                        MessageBox.Show("Ошибка!");
                        break;
                    }
                case ClassLibraryForTCPConnectionAPI_C_sharp_.AnswerFromServer.UnknownCommand:
                    {
                        MessageBox.Show("Ошибка!");
                        break;
                    }
                default:
                    break;
            }
        }

        private void VendorCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isEmptyThree)
            {
                VendorCode.Text = "";
                isEmptyThree = false;
            }
            if (VendorCode.Text == "Введите артикул")
            {
                VendorCode.Text = "";
            }
            if (Cost.Text == "")
            {
                Cost.Text = "Введите цену";
            }
            if (Name.Text == "")
            {
                Name.Text = "Введите название";
            }
        }

        private void Cost_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isEmptyTwo)
            {
                Cost.Text = "";
                isEmptyTwo = false;
            }
            if (Cost.Text == "Введите цену")
            {
                Cost.Text = "";
            }
            if (VendorCode.Text == "")
            {
                VendorCode.Text = "Введите артикул";
            }
            if (Name.Text == "")
            {
                Name.Text = "Введите название";
            }
        }

        private void Name_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isEmpty)
            {
                Name.Text = "";
                isEmpty = false;
            }
            if (Name.Text == "Введите название")
            {
                Name.Text = "";
            }
            if (VendorCode.Text == "")
            {
                VendorCode.Text = "Введите артикул";
            }
            if (Cost.Text == "")
            {
                Cost.Text = "Введите цену";
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
