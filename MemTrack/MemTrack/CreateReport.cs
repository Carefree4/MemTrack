using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using OfficeOpenXml;

namespace MemTrack
{
    public class CreateReport
    {
        private readonly string _filePath;

        private readonly string[] _key;
        private readonly List<Member> _members;


        public CreateReport(string filePath, List<Member> members)
        {
            _filePath = filePath;
            _members = members;
            _key = new[]
            {
                "Membership Number", "First Name", "Last Name", "School", "State", "Email", "Year Joined", "Active",
                "Amount Owed", "Expected Graduation Year"
            };
        }

        public CreateReport(List<Member> members)
        {
            _members = members;
            _key = new[]
            {
                "Membership Number", "First Name", "Last Name", "School", "State", "Email", "Year Joined", "Active",
                "Amount Owed", "Expected Graduation Year"
            };
        }

        public void ExportCsv()
        {
            string lines = MemberListToFormattedString("\"", ",");

            try
            {
                File.WriteAllText(_filePath, lines);
            }
            catch (Exception e)
            {
                var err = new ErrorMessageWindow(e);
            }
        }

        private string MemberListToFormattedString(string envelop, string delimiter)
        {
            string keyString = string.Empty;
            for (int i = 0; i < _key.Length; i++)
            {
                keyString += envelop + _key[i] + envelop;
                keyString += i + 1 != _key.Length ? delimiter : "\n";
            }

            string lines = _members.Aggregate(keyString, (current, m) => current +
                                                                         envelop + m.MembershipNumber + envelop +
                                                                         delimiter +
                                                                         envelop + m.FirstName + envelop + delimiter +
                                                                         envelop + m.LastName + envelop + delimiter +
                                                                         envelop + m.School + envelop + delimiter +
                                                                         envelop + m.State + envelop + delimiter +
                                                                         envelop + m.Email + envelop + delimiter +
                                                                         envelop + m.YearJoined + envelop + delimiter +
                                                                         envelop + m.Active + envelop + delimiter +
                                                                         envelop + m.AmountOwed + envelop + delimiter +
                                                                         envelop + m.ExpectedGraduationYear + envelop +
                                                                         delimiter + "\n");
            return lines;
        }

        public void BuildEmail(string recepient, string subject)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("fblamemtrack@gmail.com", "123Fbla123"),
                EnableSsl = true
            };

            try
            {
                client.Send("fblamemtrack@gmail.com", recepient, subject, MemberListAlignedString());
            }
            catch (Exception e)
            {
                var err = new ErrorMessageWindow(e);
            }
        }

        private string MemberListAlignedString()
        {
            string res = _key.Aggregate(string.Empty, (current, t) => current + string.Format("{0,-20}", t));
            res += "\n";

            return _members.Aggregate(res, (current, m) => current + string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5,-20}{6,-20}{7,-20}{8,-20}{9,-20}\n", m.MembershipNumber, m.FirstName, m.LastName, m.School, m.State, m.Email, m.YearJoined, m.Active, m.AmountOwed, m.ExpectedGraduationYear));
        }

        public void ExportExcel()
        {
            try
            {
                var file = new FileInfo(_filePath);
                var xlPack = new ExcelPackage(file);
                var workSheet = xlPack.Workbook.Worksheets.Add("Members - " + DateTime.Now);

                InsertKeyToCells(workSheet);

                SetCellValues(workSheet, 2);

                FormatWorkSheet(workSheet);

                xlPack.Save();
            }
            catch (Exception e)
            {
                var err = new ErrorMessageWindow(e);
            }
        }

        private void InsertKeyToCells(ExcelWorksheet workSheet)
        {
            for (int i = 1; i <= _key.Length; i++)
            {
                workSheet.Cells[1, i].Value = _key[i - 1];
            }
        }

        private void SetCellValues(ExcelWorksheet workSheet, int row)
        {
            foreach (var m in _members)
            {
                workSheet.Cells[row, 1].Value = m.MembershipNumber;
                workSheet.Cells[row, 2].Value = m.FirstName;
                workSheet.Cells[row, 3].Value = m.LastName;
                workSheet.Cells[row, 4].Value = m.School;
                workSheet.Cells[row, 5].Value = m.State;
                workSheet.Cells[row, 6].Value = m.Email;
                workSheet.Cells[row, 7].Value = m.YearJoined;
                workSheet.Cells[row, 8].Value = m.Active;
                workSheet.Cells[row, 9].Value = m.AmountOwed;
                workSheet.Cells[row, 10].Value = m.ExpectedGraduationYear;
                row++;
            }
        }

        private void FormatWorkSheet(ExcelWorksheet workSheet)
        {
            for (int i = 1; i <= _key.Length; i++)
            {
                workSheet.Column(i).AutoFit();
            }
        }
    }
}