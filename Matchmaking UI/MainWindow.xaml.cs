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

        #region OnLoad
        //things that need to be declared and can't go out of scope (if anything isn't initialized it should be in the OnLoad event
        Counter newCounter = new Counter();
        public delegate void GetPlayerDistro();
        GetPlayerDistro getPlayers;
        public delegate void SynchronizeCounters();
        SynchronizeCounters syncCounters;


        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //pass the callback functions
            var newPlayerDistro = PlayerDistribution.GetInstance();

            getPlayers = new GetPlayerDistro(newPlayerDistro.UpdateDistro);
            syncCounters = new SynchronizeCounters(newCounter.SyncCount);

            ManagedObject.cw_AssignCallbacks(getPlayers, syncCounters);

            //Binding test = new Binding("test");
            //test.Source = newPlayerDistro.PlayerDistro;
            //test.Mode = BindingMode.OneWay;
            //test.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //text_Output_PlayerDistro.SetBinding(TextBox.TextProperty, test);

            //Binding pickle = new Binding("pickle");
            //pickle.Source = newPlayerDistro.PlayerDistro;
            //pickle.Mode = BindingMode.OneWay;
            //pickle.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            //test_pickle.DataContext = PlayerDistribution.GetDistro();
            

            //misc 
            Binding counterBinding = new Binding("Count");
            counterBinding.Source = newCounter;
            counterBinding.Mode = BindingMode.OneWay;
            counterBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            text_Output_Counter.SetBinding(TextBlock.TextProperty, counterBinding);

            //create c++ objects
            ManagedObject.cw_CreateCounter(1000, 10);
        }
        #endregion
        #region Column 0
        async private void Button_MakePlayers_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            button.IsEnabled = false;
            button.Content = "Calculating";

            if (int.TryParse(text_Input_MakePlayers.Text, out int input))
            {
                if (input > 0)
                {
                    var result = await Task.Run(() => MakePlayers(input));
                }
            }
            else
            {
                text_Output_Error.Text = "Input must be numeric"; //TO DO this should be a variable
            }

            button.IsEnabled = true;
            button.Content = "Make Players";
        }

        private int MakePlayers(int input)
        {
            var makePlayers = new System.Threading.Thread(() => ManagedObject.cw_GetMakePlayer(input));
            makePlayers.Start();

            makePlayers.Join();

            return 0;
        }
        #endregion
        #region Column 1
        
        #endregion
    }

    public class ViewModel
    {
        public PlayerDistribution View_PlayerDistribution { get; set; }

        //constructor
        public ViewModel()
        {
            View_PlayerDistribution = PlayerDistribution.GetInstance();
        }
    }
}
