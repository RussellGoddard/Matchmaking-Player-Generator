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

            Testing.TestCall();
            text_1.Text = Testing.pickles;

            int[] test = ManagedObject.GetPlayerDistro();
            string[] testString = { "Bronze", "Silver", "Gold", "Platinum", "Diamond", "Masters", "Challenger", "Total" };
            double perc = 0D;

            for (int i = 0; i < test.Length; ++i)
            {
                perc = (double)test[i] / (double)test[7] * 100;
                text_2.Text += String.Format("{0,-10} {1,-15} {2, -10:00.00}\n", testString[i], test[i], perc);
            }
        }
    }

    public class Testing
    {
        public static string pickles;


        [DllImport("Matchmaking Player Generator.dll")]
        public static extern void CallbackTest(IntPtr ptr);

        public delegate void TestDelegate();

        public static void WriteTest()
        {
            pickles = "Callback success";
        }

        static TestDelegate ff;

        public static void TestCall()
        {
            ff = new TestDelegate(WriteTest);
            CallbackTest(Marshal.GetFunctionPointerForDelegate(ff));
        }
    }
}
