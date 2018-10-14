using System.Data;

namespace SSO.Interfaces
{
    public interface IDataContext
    {
        IDbCommand CreateCommand();
        IDbTransaction BeginTransaction();
        void Commit(IDbTransaction transaction);
        void Rollback(IDbTransaction transaction);

    }
}
