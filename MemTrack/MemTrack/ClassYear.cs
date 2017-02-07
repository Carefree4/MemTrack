using System;

namespace MemTrack
{
    public static class ClassYear
    {
        public static int FreshmenYear
        {
            get { return CalcClassYear(3); }
        }

        public static int SophomoreYear
        {
            get { return CalcClassYear(2); }
        }

        public static int JuniorYear
        {
            get { return CalcClassYear(1); }
        }

        public static int SeniorYear
        {
            get { return CalcClassYear(0); }
        }

        // yearsUntilSenor for freshmen will be 3, seniors it will be 0 
        private static int CalcClassYear(int yearsUntilSenior)
        {
            int seniorYear = DateTime.Now.Year;
            seniorYear += yearsUntilSenior;
            if (IsFirstSemister())
            {
                seniorYear++;
            }
            return seniorYear;
        }

        // Checks if it is the beginning of the this year
        // Or the end of the last one
        private static bool IsFirstSemister()
        {
            return DateTime.Now.Month > 7;
        }
    }
}