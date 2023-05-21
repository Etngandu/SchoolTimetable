using ENB.SchoolTimetables.EF;
using ENB.SchoolTimetables.Entities.Repositories;
using ENB.SchoolTimetables.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.SchoolTimetables.EF.Repositories
{

    /// <summary>
    /// A concrete repository to work with case in the system.
    /// </summary>
    public class AsyncSubjectRepository : AsyncRepository<Subject>, IAsyncSubjectRepository
    {
        /// <summary>
        /// Gets a list of all guests whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        /// 

        private readonly SchoolTimetablesContext _schoolTimetablesContext;
        public AsyncSubjectRepository(SchoolTimetablesContext schoolTimetablesContext) : base(schoolTimetablesContext)
        {
            _schoolTimetablesContext = schoolTimetablesContext;
        }
        public IEnumerable<Subject> FindByName(string name)
        {
            return _schoolTimetablesContext.Set<Subject>().Where(x => x.SubjectName == name);
        }
    }
}
