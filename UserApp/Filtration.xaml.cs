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
    /// Логика взаимодействия для Filtration.xaml
    /// </summary>
    enum ParamToFilter
    {
        Rating,
        Cost,
        VendorCode
    }
    public partial class Filtration : Window
    {
        int counter = 0;
        ParamToFilter choosenParam;
        private IDataViewAccess module;
        List<DatabaseEntities.Car> cars;
        public Filtration(IDataViewAccess module)
        {
            choosenParam = ParamToFilter.Rating;
            this.module = module;
            cars = module.GetAllCars();
            InitializeComponent();
            if (cars == null || cars.Count == 0 )
            {
                MessageBox.Show("Нет данных!");
            }
            else
                Show(cars[0]);
        }

        private void FilterByRating_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFilter.Rating;
            TopMenuItem.Header = "Фильтрация по рейтингу";
        }

        private void FindSome_Click(object sender, RoutedEventArgs e)
        {
            double value1, value2;
            try
            {
                value1 = double.Parse(FirstValue.Text);
                value2 = double.Parse(SecondValue.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода!");
                return;
            }
            switch (choosenParam)
            {
                case ParamToFilter.Rating:
                    {
                        cars = cars.Where(c => (c.TotalRate >= value1 && c.TotalRate <= value2)).ToList();
                        break;
                    }
                case ParamToFilter.Cost:
                    {
                        cars = cars.Where(c => c.Cost >= (decimal)value1 && c.Cost <= (decimal)value2).ToList();
                        break;
                    }
                case ParamToFilter.VendorCode:
                    {
                        cars = cars.Where(c => ((int)c.VendorCode >= value1 && (int)c.VendorCode <= value2)).ToList();
                        break;
                    }
                default:
                    break;
            }
          
            counter = 0;
            if (cars.Count == 0)
            {
                MessageBox.Show("Нет данных!");
                FirstValue.Text = "";
                SecondValue.Text = "";
                cars = module.GetAllCars();
            }
            if (cars.Count == 0)
                MessageBox.Show("Ошибка!");
            else
                Show(cars[counter]);

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
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FilterByCost_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFilter.Cost;
            TopMenuItem.Header = "Фильтрация по цене";
        }

        private void FilterByVendorCode_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToFilter.VendorCode;
            TopMenuItem.Header = "Фильтрация по артикулу";
        }
    }
}
