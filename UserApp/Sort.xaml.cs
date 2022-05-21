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
    /// Логика взаимодействия для Sort.xaml
    /// </summary>
    enum ParamToSort
    {
        Name,
        Cost,
        VendorCode,
        Rating
    }
    public partial class Sort : Window
    {
        int counter = 0;
        ParamToSort choosenParam;
        private IDataViewAccess module;
        List<DatabaseEntities.Car> cars;
        public Sort(IDataViewAccess module)
        {
            choosenParam = ParamToSort.Name;
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

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToSort.Name;
            TopMenuItem.Header = "Сортировка по названию";
            cars = cars.OrderBy(c => c.Name).ToList();
        }

        private void SortByRating_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToSort.Rating;
            TopMenuItem.Header = "Сортировка по рейтингу";
            cars = cars.OrderByDescending(c => c.TotalRate).ToList();
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
            if(cars.Count == 0)
            {
                MessageBox.Show("Ошибка!");
            }
            else
            {
                Show(cars[0]);
            }
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SortByCost_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToSort.Cost;
            TopMenuItem.Header = "Сортировка по цене";
            cars = cars.OrderBy(c => c.Cost).ToList();
        }

        private void SortByVendorCode_Click(object sender, RoutedEventArgs e)
        {
            choosenParam = ParamToSort.VendorCode;
            TopMenuItem.Header = "Сортировка по артикулу";
            cars = cars.OrderBy(c => c.VendorCode).ToList();
        }
    }
}
