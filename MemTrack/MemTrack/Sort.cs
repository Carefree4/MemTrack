using System.Collections.Generic;
using System.Linq;

namespace MemTrack
{
    public class Sort
    {
        private readonly List<Member> _members;

        public Sort(List<Member> members)
        {
            _members = members;
        }

        public List<Member> ByGraduationYear(List<int> years)
        {
            return _members.Where(m => years.Any(y => y == m.ExpectedGraduationYear)).ToList();
        }
    }
}