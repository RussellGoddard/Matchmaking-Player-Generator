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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

using LiveCharts;
using LiveCharts.Wpf;

namespace Matchmaking_UI
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //things that need to be declared and can't go out of scope (if anything isn't initialized it should be in the OnLoad event
        Counter newCounter = new Counter();
        public delegate void GetPlayerDistro();
        GetPlayerDistro getPlayers;
        public delegate void SynchronizeCounters();
        SynchronizeCounters syncCounters;


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //pass the callback functions
            getPlayers = new GetPlayerDistro(GetDistro);
            syncCounters = new SynchronizeCounters(newCounter.SyncCount);

            ManagedObject.cw_AssignCallbacks(getPlayers, syncCounters);

            Binding counterBinding = new Binding("Count");
            counterBinding.Source = newCounter;
            counterBinding.Mode = BindingMode.OneWay;
            counterBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            text_3.SetBinding(TextBox.TextProperty, counterBinding);

            ManagedObject.cw_CreateCounter(1000, 10);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            text_3_Copy.Text = ManagedObject.cw_GetCount().ToString();
        }

        async private void Button_MakePlayers_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            button.IsEnabled = false;
            button.Content = "Calculating";
    
            if (int.TryParse(text_Input.Text, out int input))
            {
                if (input != 0)
                {
                    var result = await Task.Run(() => MakePlayers(input));
                }
            }
            else
            {
                text_Error_Output.Text = "Input must be numeric"; //TO DO this should be a variable
            }

            button.IsEnabled = true;
            button.Content = "Make Players";
        }

        private int MakePlayers(int input)
        {
            var makePlayers = new System.Threading.Thread(()=>ManagedObject.cw_GetMakePlayer(input));
            makePlayers.Start();

            makePlayers.Join(); 
            
            return 0;
        }

        private void GetDistro()
        {
            int[] playerDistro = ManagedObject.cw_GetPlayerDistro();
            double perc = 0D;
            this.Dispatcher.Invoke(() => 
            {
                //reset text_PlayerDistro_Text, playerDistro
                text_PlayerDistro_Text.Text = "";

                text_PlayerDistro_Text.Text += String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", "Division:", "# of Players", "%");
                text_PlayerDistro_Text.Text += Environment.NewLine;
                for (int i = 0; i < playerDistro.Length - 1; ++i)
                {
                    if (i == 26)
                    {
                        text_PlayerDistro_Text.Text += Environment.NewLine;
                    }
                    perc = (double)playerDistro[i] / (double)playerDistro[34] * 100;
                    text_PlayerDistro_Text.Text += String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", ManagedObject.distroStrings[i], playerDistro[i], perc);
                }
                text_PlayerTotal_Output.Text = playerDistro.Last().ToString();
            });
        }
    }

    //public class Testing
    //{
    //    public static string pickles;

    //    [DllImport("Matchmaking Player Generator.dll")]
    //    public static extern void CallbackTest(IntPtr ptr);

    //    public delegate void TestDelegate();

    //    public static void WriteTest()
    //    {
    //        pickles = "Callback success";
    //    }

    //    static TestDelegate ff;

    //    public static void TestCall()
    //    {
    //        ff = new TestDelegate(WriteTest);
    //        CallbackTest(Marshal.GetFunctionPointerForDelegate(ff));
    //    }
    //}
}
