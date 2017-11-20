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

using System.Windows.Controls.Primitives;

namespace TimeTestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime dtToday = DateTime.Today;

        public MainWindow()
        {
            InitializeComponent();

            tbxDate.Text = dtToday.ToShortDateString();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PrintDaysOfWeek();

            List<int> list = GetCalendar(dtToday);
            PrintCalendar(list, dtToday);
        }

        List<int> GetCalendar(DateTime date)
        {
            List<int> list = new List<int>();

            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            DateTime dt1 = new DateTime(date.Year, date.Month, 1);
            int firstDayOfWeek = (int)dt1.DayOfWeek;

            for (int p = 1; p <= firstDayOfWeek; p++)
            {
                list.Add(0);
            }

            for (int i = 1; i <= daysInMonth; i++)
            {
                list.Add(i);
            }

            return list;
        }

        void PrintDaysOfWeek()
        {
            foreach (var day in Enum.GetValues(typeof(DayOfWeek)))
            {
                TextBlock tb = new TextBlock();
                tb.Text = String.Format(" {0} ", day.ToString().Substring(0, 2));
                tb.Width = 30;
                tb.FontWeight = FontWeights.DemiBold;
                tb.FontStyle = FontStyles.Italic;

                ugCalendar.Children.Add(tb);
            }
        }

        void PrintCalendar(List<int> calendar, DateTime date)
        {
            string aDay = "";
            StringBuilder sb = new StringBuilder();

            tblMonthYear.Text = String.Format("{0} {1}", date.ToString("MMMM"), date.ToString("yyyy"));
           
            for (int i = 0; i < calendar.Count; i++)
            {
                sb.AppendFormat("{0}", calendar[i].ToString("D2"));

                sb.Replace("00", "  ");

                sb.Replace("0", " ", 0, 1);

                TextBlock tb = new TextBlock();

                if (calendar[i] == date.Day)
                {
                    aDay = String.Format("[{0}]", sb);
                    tb.FontWeight = FontWeights.Bold;
                }
                else
                {
                    aDay = String.Format(" {0} ", sb);
                }
                
                tb.Text = aDay;
                tb.Width = 30;

                ugCalendar.Children.Add(tb);
                
                sb.Clear();
            }          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt;

            ugCalendar.Children.Clear();

            if (DateTime.TryParse(tbxDate.Text, out dt))
            {               
                PrintDaysOfWeek();
                List<int> list = GetCalendar(dt);
                PrintCalendar(list, dt);
            }
            else
            {
                tbxDate.Text = DateTime.Today.ToShortDateString();
            }
        }
    }
}
