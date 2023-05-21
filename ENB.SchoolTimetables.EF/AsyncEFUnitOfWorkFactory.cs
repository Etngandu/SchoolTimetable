using ENB.SchoolTimetables.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ENB.SchoolTimetables.EF
{
  public  class AsyncEFUnitOfWorkFactory :IAsyncUnitOfWorkFactory
    {
        private readonly SchoolTimetablesContext _schoolTimetablesContext;

      

        public AsyncEFUnitOfWorkFactory(SchoolTimetablesContext schoolTimetablesContext)
        {
            _schoolTimetablesContext = schoolTimetablesContext;

        }
        public AsyncEFUnitOfWorkFactory(bool forcenew, SchoolTimetablesContext schoolTimetablesContext)
        {
                _schoolTimetablesContext = schoolTimetablesContext;

        }
        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        public async Task<IAsyncUnitOfWork> Create()
        {
            return await Create(false);
        }

        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        /// <param name="forceNew">When true, clears out any existing data context from the storage container.</param>
        public async Task<IAsyncUnitOfWork> Create(bool forceNew)
        {
            var asyncEFUnitOfWork = await Task.FromResult(new AsyncEFUnitOfWork(forceNew,_schoolTimetablesContext));


            return asyncEFUnitOfWork!;

        }


    }
}
