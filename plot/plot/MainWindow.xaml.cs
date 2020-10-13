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

namespace plot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void But_Copy_Click(object sender, RoutedEventArgs e)
        {
            var line1 = new InteractiveDataDisplay.WPF.LineGraph
            {
                Stroke = new SolidColorBrush(Colors.Blue),
                Description = "Line A",
                StrokeThickness = 2
            };

            var line2 = new InteractiveDataDisplay.WPF.LineGraph
            {
                Stroke = new SolidColorBrush(Colors.Red),
                Description = "Line B",
                StrokeThickness = 2
            };
            var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();
            double[] y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v).ToArray();
            List<double> signal_input= new List<double>();
            for (int i = 0; i < y.Length; i++)
            {
                signal_input.Add(y[i]);
            }
            line1.Plot(x, y);
            double max, min;
            List<double> mi = new List<double>();
            List<double> mx = new List<double>();
            min = signal_input[1];
            max = signal_input[1];
            
            int ii = 0;
            bool tum = true;
          for (int j = 1; j < signal_input.Count;j++)
            {

                if (min > signal_input[j] && !tum)
                    min = signal_input[j];
                if (max < signal_input[j] && tum)
                    max = signal_input[j];
               

                if ((max > signal_input[j] && tum) || (signal_input.Count-1 == j && tum))
                {

                    mx.Add(max);
                    lis.Items.Add(x[j-1]+"  max");
                    lis_Copy.Items.Add(mx[ii]);
                    ii++;
                    mi.Add(x[j - 1]);
                    min = signal_input[j];
                    tum = false;
                   
                }
                if (min < signal_input[j] && !tum || signal_input.Count-1 == j && !tum)
                {
                    ii--;
                    
                    //lis_Copy.Items.Add(mi[ii]);
                    lis.Items.Add(x[j - 1] + "  min   "+j);
                    ii++;
                    max = signal_input[j];
                    tum = true;
                }
                line2.Plot(mi, mx);
            }
            GridPlot.Children.Clear();
            GridPlot.Children.Add(line1);
            GridPlot.Children.Add(line2);
            
            // customize styling
            pl.BottomTitle = $"Horizontal Axis Label";
            pl.LeftTitle = $"Vertical Axis Label";
            


        }

    }

}
//<d3:LineGraph x:Name="linegraph" Description="Simple linegraph" Stroke="Blue" StrokeThickness="3"/>  