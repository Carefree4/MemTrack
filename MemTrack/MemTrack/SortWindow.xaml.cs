using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MemTrack
{
    /// <summary>
    /// Interaction logic for SortWindow.xaml
    /// </summary>
    public partial class SortWindow
    {
        private readonly List<Member> _members;
        public List<Member> Sorted { get; set; }
        public SortWindow(List<Member> members)
        {
            InitializeComponent();
            _members = members;
        }

        private void cb_Feilds_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Feilds.ItemsSource = Enum.GetValues(typeof(Member.Feilds)).Cast<Member.Feilds>();
        }

        private void btn_Sort_Click(object sender, RoutedEventArgs e)
        {
            // Years is a list of all the desired class years
            var years = new List<int>();

            if (mi_YearFreshmen.IsChecked == true)
            {
                years.Add(ClassYear.FreshmenYear);
            }
            if (mi_YearSoftmore.IsChecked == true)
            {
                years.Add(ClassYear.SophomoreYear);
            }
            if (mi_YearJunior.IsChecked == true)
            {
                years.Add(ClassYear.JuniorYear);
            }
            if (mi_YearSenior.IsChecked == true)
            {
                years.Add(ClassYear.SeniorYear);
            }

            var sort = new Sort(_members);
            Sorted = sort.ByGraduationYear(years);

            Close();
        }

    }
}
