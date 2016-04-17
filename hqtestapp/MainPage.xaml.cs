using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UniversalBeaconLibrary.Beacon;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace hqtestapp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Bluetooth Beacons
        private readonly BluetoothLEAdvertisementWatcher _watcher;

        private readonly BeaconManager _beaconManager;

        public MainPage()
        {
            // [...]
            // Construct the Universal Bluetooth Beacon manager
            _beaconManager = new BeaconManager();

            // Create & start the Bluetooth LE watcher from the Windows 10 UWP
            _watcher = new BluetoothLEAdvertisementWatcher {ScanningMode = BluetoothLEScanningMode.Active,  };
            _watcher.Received += WatcherOnReceived;
            _watcher.Start();
            dt.Tick += Dt_Tick;
            dt.Start();

            this.InitializeComponent();
        }

        DispatcherTimer dt = new DispatcherTimer() {Interval = TimeSpan.FromTicks(1)};

        private void WatcherOnReceived(BluetoothLEAdvertisementWatcher sender,
            BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            // Let the library manager handle the advertisement to analyse & store the advertisement
            _beaconManager.ReceivedAdvertisement(eventArgs);
            //PrintBeaconInfo();


        }

        private void Dt_Tick(object sender, object e)
        {
            PrintBeaconInfo();
        }



        // Call this method e.g., when tapping a button
        private void PrintBeaconInfo()
        {

            var beacons = _beaconManager.BluetoothBeacons.ToList().OrderByDescending(x => x.Rssi).ToList();


            image.Visibility = Visibility.Collapsed;
            image1.Visibility = Visibility.Collapsed;
            image2.Visibility = Visibility.Collapsed;
            image3.Visibility = Visibility.Collapsed;
            image4.Visibility = Visibility.Collapsed;
            image5.Visibility = Visibility.Collapsed;

            // textBox.Text = "";
            //textBox1.Text = "";
            int im = 1;
            int a = 0;
            //Debug.WriteLine("Beacons discovered so far\n-------------------------");
            foreach (var bluetoothBeacon in beacons)
            {
                image.Visibility = Visibility.Collapsed;
                image1.Visibility = Visibility.Collapsed;
                image2.Visibility = Visibility.Collapsed;
                image3.Visibility = Visibility.Collapsed;
                image4.Visibility = Visibility.Collapsed;
                image5.Visibility = Visibility.Collapsed;


                ////if(bluetoothBeacon.BluetoothAddressAsString.StartsVith(GridStrings))


                a++;
                int cell = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (bluetoothBeacon.BluetoothAddressAsString.StartsWith(GridStrings[i]))
                    {
                        cell = i + 1;
                        break;
                    }
                }

                if (a == 1)
                {
                    im = cell;
                }

                

              //  textBox.Text += cell.ToString() + "\n";
              //  textBox1.Text += bluetoothBeacon.Rssi.ToString() + "\n";


                switch (im)
                {
                    case 1:
                        image.Visibility=Visibility.Visible;
                        break;
                    case 2:
                        image1.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        image2.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        image3.Visibility = Visibility.Visible;
                        break;
                    case 5:
                        image4.Visibility = Visibility.Visible;
                        break;
                    case 6:
                        image5.Visibility = Visibility.Visible;
                        break;

                }

                    

                
            }
        }

        public string[] GridStrings = new string[] {"E1:E9", "EE:D9", "CA:C5", "F0:20", "CC:34", "F8:DD"};


        private void button_Click(object sender, RoutedEventArgs e)
        {
            PrintBeaconInfo();
        }


    }
}

