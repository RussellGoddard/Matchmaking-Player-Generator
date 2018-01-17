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
        SynchronizeCounters syncCounter;


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //pass the callback functions
            getPlayers = new GetPlayerDistro(GetDistro);
            syncCounter = new SynchronizeCounters(newCounter.SyncCount);

            

            ManagedWrapper.AssignCallbacks(getPlayers, syncCounter);

            Binding counterBinding = new Binding("Count");
            counterBinding.Source = newCounter;
            counterBinding.Mode = BindingMode.OneWay;
            counterBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            text_3.SetBinding(TextBox.TextProperty, counterBinding);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            text_3_Copy.Text = newCounter.Count.ToString();
        }

        async private void Button_MakePlayers_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            button.IsEnabled = false;
            button.Content = "Calculating";

            int input = Convert.ToInt32(text_Input.Text);

            var result = await Task.Run(() => MakePlayers(input));
            //ManagedObject.GetMakePlayer(input);

            button.IsEnabled = true;
            button.Content = "Make Players";
        }

        private int MakePlayers(int input)
        {
            var makePlayers = new System.Threading.Thread(()=>ManagedObject.GetMakePlayer(input));
            makePlayers.Start();

            makePlayers.Join(); 
            
            return 0;
        }

        private void Button_GetDistro_Click(object sender, RoutedEventArgs e)
        {
            GetDistro();
        }

        private void GetDistro()
        {
            int[] playerDistro = ManagedObject.GetPlayerDistro();
            double perc = 0D;
            this.Dispatcher.Invoke(() => 
            {
                //reset text_Output, playerDistro
                text_Output.Text = "";


                text_Output.Text += String.Format("{0,-15} {1,-15} {2, -10:00.00}\n", "Division:", "# of Players", "%");
                for (int i = 0; i < playerDistro.Length; ++i)
                {
                    perc = (double)playerDistro[i] / (double)playerDistro[34] * 100;
                    text_Output.Text += String.Format("{0,-15} {1,-15} {2, -10:00.00}\n", ManagedObject.distroStrings[i], playerDistro[i], perc);
                }
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
