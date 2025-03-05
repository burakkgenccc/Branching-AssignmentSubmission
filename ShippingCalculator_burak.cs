// Package Express Shipping Calculator
// Created by: Rachel Martinez
// Version: 2.0.0
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PackageExpress.MVVM
{
    // Model
    public class Package : INotifyPropertyChanged
    {
        private double _weight;
        private double _width;
        private double _height;
        private double _length;

        public double Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // ViewModel
    public class ShippingViewModel
    {
        private readonly Package _package;

        public ShippingViewModel()
        {
            _package = new Package();
            _package.PropertyChanged += Package_PropertyChanged;
        }

        private void Package_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // You could add validation logic here
        }

        public bool ValidateWeight() => _package.Weight <= 50;
        public bool ValidateSize() => (_package.Width + _package.Height + _package.Length) <= 50;
        public double CalculateShippingCost() => (_package.Width * _package.Height * _package.Length * _package.Weight) / 100;

        public Package Package => _package;
    }

    // View
    class Program
    {
        static void Main(string[] args)
        {
            // Display welcome message
            Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

            var viewModel = new ShippingViewModel();

            try
            {
                // Input package weight
                Console.WriteLine("Please enter the package weight:");
                if (!double.TryParse(Console.ReadLine(), out double weightInput))
                {
                    Console.WriteLine("Invalid weight input.");
                    return;
                }
                viewModel.Package.Weight = weightInput;

                // Check maximum weight
                if (!viewModel.ValidateWeight())
                {
                    Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
                    return;
                }

                // Input package dimensions
                Console.WriteLine("Please enter the package width:");
                if (!double.TryParse(Console.ReadLine(), out double widthInput))
                {
                    Console.WriteLine("Invalid width input.");
                    return;
                }
                viewModel.Package.Width = widthInput;

                Console.WriteLine("Please enter the package height:");
                if (!double.TryParse(Console.ReadLine(), out double heightInput))
                {
                    Console.WriteLine("Invalid height input.");
                    return;
                }
                viewModel.Package.Height = heightInput;

                Console.WriteLine("Please enter the package length:");
                if (!double.TryParse(Console.ReadLine(), out double lengthInput))
                {
                    Console.WriteLine("Invalid length input.");
                    return;
                }
                viewModel.Package.Length = lengthInput;

                // Check maximum size
                if (!viewModel.ValidateSize())
                {
                    Console.WriteLine("Package too big to be shipped via Package Express.");
                    return;
                }

                // Calculate and display shipping cost
                double shippingTotal = viewModel.CalculateShippingCost();
                Console.WriteLine($"Your estimated total for shipping this package is: ${shippingTotal:F2}");
                Console.WriteLine("Thank you!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
} 