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

        Counter newCounter = new Counter();

        private void OnLoad(object sender, RoutedEventArgs e)
        {

            //Testing.TestCall();
            //text_1.Text = Testing.pickles;

            //int[] test = ManagedObject.GetPlayerDistro();
            //string[] testString = { "Bronze", "Silver", "Gold", "Platinum", "Diamond", "Masters", "Challenger", "Total" };
            //double perc = 0D;

            //for (int i = 0; i < test.Length; ++i)
            //{
            //    perc = (double)test[i] / (double)test[7] * 100;
            //    text_2.Text += String.Format("{0,-10} {1,-15} {2, -10:00.00}\n", testString[i], test[i], perc);
            //}


            
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

            button.IsEnabled = true;
            button.Content = "Make Players";
        }

        private int MakePlayers(int input)
        {
            //call make players function
            ManagedObject.GetMakePlayer(input);

            return 0;
        }

        private void Button_GetDistro_Click(object sender, RoutedEventArgs e)
        {
            int[] playerDistro = ManagedObject.GetPlayerDistro();
            double perc = 0D;
            //reset text_Output, playerDistro
            text_Output.Text = "";


            text_Output.Text += String.Format("{0,-15} {1,-15} {2, -10:00.00}\n", "Division:", "# of Players", "%");
            for (int i = 0; i < playerDistro.Length; ++i)
            {
                perc = (double)playerDistro[i] / (double)playerDistro[34] * 100;
                text_Output.Text += String.Format("{0,-15} {1,-15} {2, -10:00.00}\n", ManagedObject.distroStrings[i], playerDistro[i], perc);
            }
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
