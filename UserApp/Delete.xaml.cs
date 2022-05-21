using DatabaseEntities;
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
using TCPConnectionAPIClientModule_C_sharp_;

namespace Front_End_Three
{
    /// <summary>
    /// Логика взаимодействия для Delete.xaml
    /// </summary>
    public partial class Delete : Window
    {
        int counter = 0;
        ParamToFind choosenParam;
        bool isEmpty = true;
        private IAdminAccess module;
        List<DatabaseEntities.Car> cars;
        public Delete(IAdminAccess module)
        {
            choosenParam = ParamToFind.Name;
            this.module = module;
            cars = module.GetAllCars();
            InitializeComponent();
            if (cars == null || cars.Count == 0)
            {
                MessageBox.Show("Нет данных!");
            }
            else
                Show(cars[0]);
        }

        private void SomeInput_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isEmpty)
            {
                SomeInput.Text = "";
                isEmpty = false;
            }
        }

        private void Show(DatabaseEntities.Car car)
        {
            if (car == null)
            {
                MessageBox.Show("Не найдено!");
            }
            else
            {
                CarImage.Source = App.ConvertToBitmapImage(car.Photo);
                CarName.Text = car.Name;
                CarVendorCode.Text = car.VendorCode.ToString();
                CarCost.Text = car.Cost.ToString();
                CarRate.Text = car.TotalRate.ToString();
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFind.None;
            TopMenuItem.Header = "Показать всё";
        }

        private void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (cars.Count == 0)
            {
                MessageBox.Show("Не найдено!");
            }
            else
            {
                if (counter == 0)
                {
                    Show(cars[0]);
                }
                else
                {
                    counter--;
                    Show(cars[counter]);
                }
            }
        }

        private void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (cars.Count == 0)
            {
                MessageBox.Show("Не найдено!");
            }
            else if (cars.Count == 1)
            {
                Show(cars[counter]);
            }
            else
            {
                if (counter + 1 == cars.Count)
                {
                    Show(cars[counter]);
                }
                else
                {
                    counter++;
                    Show(cars[counter]);
                }
            }
        }

        private void FindSome_Click(object sender, RoutedEventArgs e)
        {
            cars.Clear();
            switch (choosenParam)
            {
                case ParamToFind.Name:
                    {
                        cars = module.GetAllCars();
                        cars = cars.FindAll(c => (c.Name == SomeInput.Text)).ToList();
                        break;
                    }
                case ParamToFind.VendorCode:
                    {
                        cars = module.GetAllCars();
                        cars = cars.FindAll(c => (c.VendorCode == int.Parse(SomeInput.Text))).ToList();
                        break;
                    }
                case ParamToFind.Cost:
                    {
                        cars = module.GetAllCars();
                        cars = cars.FindAll(c => (c.Cost.ToString() == SomeInput.Text)).ToList();
                        break;
                    }
                case ParamToFind.None:
                    {
                        cars = module.GetAllCars();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Повторите попытку!");
                        break;
                    }
            }
        }

        private void FindByName_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFind.Name;
            TopMenuItem.Header = "Поиск по имени";
        }

        private void FindByCost_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFind.Cost;
            TopMenuItem.Header = "Поиск по цене";
        }

        private void FindByVendorCode_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFind.VendorCode;
            TopMenuItem.Header = "Поиск по артикулу";
        }
        private void DeleteObject_Click(object sender, RoutedEventArgs e)
        {
            if (cars.Count == 0)
                MessageBox.Show("Ошибка!");
            else
            {
                module.DeleteCar(cars[counter].Id);
                MessageBox.Show("Удаление успешно!");
                if (counter + 1 == cars.Count)
                {
                    Show(cars[counter]);
                }
                else if (counter + 1 > cars.Count)
                {
                    MessageBox.Show("Ошибка!");
                }
                else
                {
                    counter++;
                    Show(cars[counter]);
                }
            }
            cars = module.GetAllCars();
        }
    }
}
