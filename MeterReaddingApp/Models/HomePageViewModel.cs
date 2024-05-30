using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using System.Windows.Input;
using Microsoft.Maui.Controls;
using MeterReaddingApp.Services;

namespace MeterReaddingApp.Models
{
    

    public class Customer
    {
        public long RecordId { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustReference { get; set; }
    }

    public class HomePageViewModel : BindableObject
    {
        private ObservableCollection<Customer> customers;
        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand AddMeterReadingsCommand { get; }

        public HomePageViewModel()
        {
            // Initialize the customer collection
            Customers = new ObservableCollection<Customer>();

            // Define the commands
            UpdateCommand = new Command<Customer>(OnUpdate);
            AddMeterReadingsCommand = new Command<Customer>(OnAddMeterReadings);

            // Load customers by district code (SBU)
            LoadCustomersByDistrictCode();
        }

        private async void LoadCustomersByDistrictCode()
        {
            string sbu = Preferences.Get("sbu", string.Empty);
            var customersByDistrict = await ApiService.GetCustomersByDistrictCode(sbu);
            foreach (var customer in customersByDistrict)
            {
                Customers.Add(new Customer
                {
                    RecordId = customer.RecordId,
                    CustomerName = customer.CustomerName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    CustomerAddress = customer.CustomerAddress,
                    CustReference = customer.CustReference
                });
            }
        }

        private void OnUpdate(Customer customer)
        {
            // Handle the update logic here
        }

        private void OnAddMeterReadings(Customer customer)
        {
            // Handle the add meter readings logic here
        }
    }
}