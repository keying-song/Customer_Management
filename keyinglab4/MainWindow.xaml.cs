using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

namespace keyinglab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database1Entities4 db = new Database1Entities4();
            var customerRecords = from c in db.Customers
                                  join b in db.CustomerAddresses
                                  on c.CustomerId equals b.CustomerId
                                  join a in db.Addresses
                                  on b.AddressId equals a.AddressId
                                  select new
                                  {
                                      CustomerId = c.CustomerId,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      CompanyName = c.CompanyName,
                                      EmailAddress = c.EmailAddress,
                                      Phone = c.Phone,
                                      AddressLine1 = a.AddressLine1,
                                      AddressLine2 = a.AddressLine2,
                                      City = a.City,
                                      StateProvince = a.StateProvince,
                                      CountryRegion = a.CountryRegion,
                                      PostalCode = a.PostalCode,                                   
                                  };
            this.gridCustomers.ItemsSource = customerRecords.ToList();
        }
        //add record
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Database1Entities4 db = new Database1Entities4();
            Customer customerobject = new Customer()
            {
                FirstName = fnbox.Text,
                LastName = lnbox.Text,
                CompanyName = cnbox.Text,
                EmailAddress = embox.Text,
                Phone = phbox.Text,

            };
            Address addressobject = new Address()
            {
                AddressLine1 = addbox1.Text,
                AddressLine2 = addbox2.Text,
                City = citybox.Text,
                StateProvince = provbox.Text,
                CountryRegion = countrybox.Text,
                PostalCode = pcodebox.Text
            };
            CustomerAddress customeraddressobject = new CustomerAddress()
            {
                CustomerId = customerobject.CustomerId,
                AddressId = addressobject.AddressId
            };
            db.Customers.Add(customerobject);
            db.Addresses.Add(addressobject);
            db.CustomerAddresses.Add(customeraddressobject);
            db.SaveChanges();
            var customerRecords = from c in db.Customers
                                  join b in db.CustomerAddresses
                                  on c.CustomerId equals b.CustomerId
                                  join a in db.Addresses
                                  on b.AddressId equals a.AddressId
                                  select new
                                  {
                                      CustomerId = c.CustomerId,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      CompanyName = c.CompanyName,
                                      EmailAddress = c.EmailAddress,
                                      Phone = c.Phone,
                                      AddressLine1 = a.AddressLine1,
                                      AddressLine2 = a.AddressLine2,
                                      City = a.City,
                                      StateProvince = a.StateProvince,
                                      CountryRegion = a.CountryRegion,
                                      PostalCode = a.PostalCode
                                  };

            this.gridCustomers.ItemsSource = customerRecords.ToList();
        }

        //select items from datagrid
        private int updatingid = 0;
        private void gridCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (this.gridCustomers.SelectedItem != null)
            {
                string[] a = this.gridCustomers.SelectedItem.ToString().Split(',');


                List<string> values = new List<string>();
                for (int i = 0; i < a.Length; i++)
                {
                    string[] item = a[i].Split('=');


                    values.Add(item[1].Trim());

                }
                if (this.gridCustomers.SelectedIndex >= 0)
                {
                    if (this.gridCustomers.SelectedItems.Count >= 0)
                    {
                        updatingid =int.Parse(values[0]);
                        fnbox.Text = values[1];
                        lnbox.Text = values[2];
                        cnbox.Text = values[3];
                        embox.Text = values[4];
                        phbox.Text = values[5];
                        addbox1.Text = values[6];
                        addbox2.Text = values[7];
                        citybox.Text = values[8];
                        provbox.Text = values[9];
                        countrybox.Text = values[10];
                        pcodebox.Text = values[11].Substring(0, values[11].Length - 1);
                    }
                }
            }
        }
        //update record
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Database1Entities4 db = new Database1Entities4();
            //update Customer info
            var r1 = db.Customers.Where(a => a.CustomerId == updatingid).FirstOrDefault();
            if (r1 != null)
            {
                r1.FirstName = fnbox.Text;
                r1.LastName = lnbox.Text;
                r1.CompanyName = cnbox.Text;
                r1.Phone = phbox.Text;
                r1.EmailAddress = embox.Text;

                db.SaveChanges();
            }
            //update Address info
            var r2 = db.CustomerAddresses.Where(a => a.CustomerId == updatingid).FirstOrDefault();
            if (r2 != null) { 
            int aid = r2.AddressId;
            var r3 = db.Addresses.Where(a=>a.AddressId == aid).FirstOrDefault();
            r3.AddressLine1 = addbox1.Text;
            r3.AddressLine2 = addbox2.Text;
            r3.City = citybox.Text;
            r3.StateProvince = provbox.Text;
            r3.CountryRegion = countrybox.Text;
            r3.PostalCode = pcodebox.Text;
            db.SaveChanges();
            }
            var customerRecords = from c in db.Customers
                                  join b in db.CustomerAddresses
                                  on c.CustomerId equals b.CustomerId
                                  join a in db.Addresses
                                  on b.AddressId equals a.AddressId
                                  select new
                                  {
                                      CustomerId = c.CustomerId,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      CompanyName = c.CompanyName,
                                      EmailAddress = c.EmailAddress,
                                      Phone = c.Phone,
                                      AddressLine1 = a.AddressLine1,
                                      AddressLine2 = a.AddressLine2,
                                      City = a.City,
                                      StateProvince = a.StateProvince,
                                      CountryRegion = a.CountryRegion,
                                      PostalCode = a.PostalCode,
                                      
                                  };
            this.gridCustomers.ItemsSource = customerRecords.ToList();

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("are you sure you wnat to delete?", "delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (msgBoxResult == MessageBoxResult.Yes)
            {
                Database1Entities4 db = new Database1Entities4();
                var r1 = db.CustomerAddresses.Where(a => a.CustomerId == updatingid).FirstOrDefault();
                if (r1 != null)
                {
                    int aid = r1.AddressId;
                    db.CustomerAddresses.Remove(r1);
                    var r2 = db.Customers.Where(a => a.CustomerId == updatingid).FirstOrDefault();
                    db.Customers.Remove(r2);
                    var r3 = db.Addresses.Where(a => a.AddressId == aid).FirstOrDefault();
                    db.Addresses.Remove(r3);

                    db.SaveChanges();
                }
                var customerRecords = from c in db.Customers
                                      join b in db.CustomerAddresses
                                      on c.CustomerId equals b.CustomerId
                                      join a in db.Addresses
                                      on b.AddressId equals a.AddressId
                                      select new
                                      {
                                          CustomerId = c.CustomerId,
                                          FirstName = c.FirstName,
                                          LastName = c.LastName,
                                          CompanyName = c.CompanyName,
                                          EmailAddress = c.EmailAddress,
                                          Phone = c.Phone,
                                          AddressLine1 = a.AddressLine1,
                                          AddressLine2 = a.AddressLine2,
                                          City = a.City,
                                          StateProvince = a.StateProvince,
                                          CountryRegion = a.CountryRegion,
                                          PostalCode = a.PostalCode,

                                      };
                this.gridCustomers.ItemsSource = customerRecords.ToList();
            }
        }
        //search record
        private void searchBox_Click(object sender, RoutedEventArgs e)
        {
            int searchid = int.Parse(searchBox.Text);
            Database1Entities4 db = new Database1Entities4();
            var customerRecords = from c in db.Customers
                                  where c.CustomerId == searchid
                                  join b in db.CustomerAddresses
                                  on c.CustomerId equals b.CustomerId
                                  join a in db.Addresses
                                  on b.AddressId equals a.AddressId
                                  select new
                                  {
                                      CustomerId = c.CustomerId,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      CompanyName = c.CompanyName,
                                      EmailAddress = c.EmailAddress,
                                      Phone = c.Phone,
                                      AddressLine1 = a.AddressLine1,
                                      AddressLine2 = a.AddressLine2,
                                      City = a.City,
                                      StateProvince = a.StateProvince,
                                      CountryRegion = a.CountryRegion,
                                      PostalCode = a.PostalCode,

                                  };
            this.gridCustomers.ItemsSource = customerRecords.ToList();
        }
        //get all employees from canada
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Database1Entities4 db = new Database1Entities4();
            var customerRecords = from c in db.Customers
                                  join b in db.CustomerAddresses
                                  on c.CustomerId equals b.CustomerId
                                  join a in db.Addresses
                                  on b.AddressId equals a.AddressId
                                  where a.CountryRegion == "CA"
                                  select new
                                  {
                                      CustomerId = c.CustomerId,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      CompanyName = c.CompanyName,
                                      EmailAddress = c.EmailAddress,
                                      Phone = c.Phone,
                                      AddressLine1 = a.AddressLine1,
                                      AddressLine2 = a.AddressLine2,
                                      City = a.City,
                                      StateProvince = a.StateProvince,
                                      CountryRegion = a.CountryRegion,
                                      PostalCode = a.PostalCode,

                                  };
            this.gridCustomers.ItemsSource = customerRecords.ToList();
        }
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            fnbox.Text = "";
            lnbox.Text = "";
            cnbox.Text = "";
            embox.Text = "";
            phbox.Text = "";
            addbox1.Text = "";
            addbox2.Text = "";
            citybox.Text = "";
            provbox.Text = "";
            countrybox.Text = "";
            pcodebox.Text = "";

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Database1Entities4 db = new Database1Entities4();
            var customerRecords = from c in db.Customers
                                  join b in db.CustomerAddresses
                                  on c.CustomerId equals b.CustomerId
                                  join a in db.Addresses
                                  on b.AddressId equals a.AddressId
                                  select new
                                  {
                                      CustomerId = c.CustomerId,
                                      FirstName = c.FirstName,
                                      LastName = c.LastName,
                                      CompanyName = c.CompanyName,
                                      EmailAddress = c.EmailAddress,
                                      Phone = c.Phone,
                                      AddressLine1 = a.AddressLine1,
                                      AddressLine2 = a.AddressLine2,
                                      City = a.City,
                                      StateProvince = a.StateProvince,
                                      CountryRegion = a.CountryRegion,
                                      PostalCode = a.PostalCode,


                                  };
            this.gridCustomers.ItemsSource = customerRecords.ToList();

        }

      
    }
}
