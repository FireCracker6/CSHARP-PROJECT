using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using _02_WPF.Models;
using _02_WPF.Services;
using Newtonsoft.Json;

namespace _02_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        private readonly FileService file = new();
  


        public MainWindow()
        {
            InitializeComponent();
            file.FilePath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\content.json";

            PopulateEmployeesList();
           
        }


        private void PopulateEmployeesList()
        { 
            try
            {
                var items = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(file.Read());
                if (items != null)
                {
                    employees = items;
                }
 
            }
            catch { }

            lv_Employees.ItemsSource= employees;

        
        }


        /// <summary>
        /// Den här lägger till en användare i listan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
             employees.Add(new Employee
            {
                FirstName = tb_FirstName.Text,
                LastName = tb_LastName.Text,
                Email = tb_Email.Text
            });

            file.Save(JsonConvert.SerializeObject(employees));
            ClearForm();

        }
        private void ClearForm()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
        }
    }
}
