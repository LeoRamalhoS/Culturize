namespace Culturize.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepo { get; }
        IApplicationUserRepository ApplicationUserRepo { get; }

        void Save();
    }
}
