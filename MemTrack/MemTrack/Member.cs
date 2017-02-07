namespace MemTrack
{
    public class Member
    {
        public enum Feilds
        {
            MembershipNumber,
            FirstName,
            LastName,
            School,
            State,
            Email,
            YearJoined,
            Active,
            AmountOwed,
            ExpectedGraduationYear
        }

        // Membership Number is 7 digits
        public int MembershipNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public int YearJoined { get; set; }
        public bool Active { get; set; }
        public double AmountOwed { get; set; }
        public int ExpectedGraduationYear { get; set; }
    }
}