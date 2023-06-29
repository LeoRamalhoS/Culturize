using Culturize.DataAccess.Repository.IRepository;

namespace Culturize.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICompanyRepository CompanyRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CompanyRepo = new CompanyRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
