namespace Culturize.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepo { get; }
        
        void Save();
    }
}
