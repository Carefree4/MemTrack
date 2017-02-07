using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;

namespace MemTrack
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static string _filePath = @"c:\Data\MemTrack\Data.xml";

        private List<Member> _allMembers;
        private List<Member> _sortedMembers;


        public MainWindow()
        {
            InitializeComponent();

            _allMembers = new List<Member>();
        }

        private static string GetFilePathFromUser(string filter)
        {
            var fd = new OpenFileDialog {Filter = filter};

            bool? result = fd.ShowDialog();

            return result == true ? fd.FileName : string.Empty;
        }

        private static string GetSaveFilePathFromUser(string filter)
        {
            var sfd = new SaveFileDialog {Filter = filter};

            bool? result = sfd.ShowDialog();

            return result == true ? sfd.FileName : string.Empty;
        }

        private void Refresh(List<Member> members)
        {
            DgMembers.ItemsSource = null;

            DgMembers.ItemsSource = members;
        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            var access = new AccessMemberXml(_filePath);
            access.OverwriteXml(_allMembers);
        }

        private void MiLoad_Click(object sender, RoutedEventArgs e)
        {
            _filePath = GetFilePathFromUser("XML File (*.xml) | *.xml");

            var access = new AccessMemberXml(_filePath);
            _allMembers = access.ReadXml();
            _sortedMembers = _allMembers;
            Refresh(_allMembers);
        }

        private void MiRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh(_allMembers);
        }

        private void MiCSV_Click(object sender, RoutedEventArgs e)
        {
            string path = GetSaveFilePathFromUser("CSV File (*.csv) | *.csv | TXT File (*.txt) | *.txt");
            var report = new CreateReport(path, _sortedMembers);
            report.ExportCsv();
        }

        private void MiSort_Click(object sender, RoutedEventArgs e)
        {
            var sw = new SortWindow(_allMembers);
            sw.ShowDialog();
            _sortedMembers = sw.Sorted;
            Refresh(_sortedMembers);
        }

        private void MiExcel_Click(object sender, RoutedEventArgs e)
        {
            string path = GetSaveFilePathFromUser("XLSX File (*.xlsx) | *.xlsx | XLS File (*.xls) | *.xls");
            var report = new CreateReport(path, _sortedMembers);
            report.ExportExcel();
        }

        private void MiShowAll_Click(object sender, RoutedEventArgs e)
        {
            _sortedMembers = _allMembers;
            Refresh(_allMembers);
        }

        private void MiEmail_Click(object sender, RoutedEventArgs e)
        {
            var report = new CreateReport(_sortedMembers);

            // TODO: email input from user
            report.BuildEmail("max@bluewatercode.com", "Member Report");
        }

        private void MiNew_Click(object sender, RoutedEventArgs e)
        {
            _filePath = GetSaveFilePathFromUser("XML File (*.xml) | *.xml");
            var xml = new AccessMemberXml(_filePath);
            xml.OverwriteXml(new List<Member>());
            Refresh(_allMembers);
        }

    }
}