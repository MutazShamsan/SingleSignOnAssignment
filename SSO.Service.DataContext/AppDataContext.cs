using SSO.Interfaces;
using System;
using System.Data;

namespace SSO.Service.DataContext
{
    /// <summary>
    /// Data context class to handle the database connection and transactions
    /// No need to handle any exception. let it crash and handle in another layer
    /// </summary>
    public class AppDataContext : IDataContext, IDisposable
    {
        private readonly IDbConnection _dbConnection;

        public AppDataContext(IDbConnection con)
        {
            _dbConnection = con;
            _dbConnection.Open();
        }

        public IDbCommand CreateCommand()
        {
            return _dbConnection.CreateCommand();
        }

        public IDbTransaction BeginTransaction()
        {
            return _dbConnection.BeginTransaction();
        }

        public void Commit(IDbTransaction transaction)
        {
            transaction.Commit();
        }

        public void Rollback(IDbTransaction transaction)
        {
            transaction.Rollback();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_dbConnection != null)
                    {
                        _dbConnection.Close();
                        _dbConnection.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppDataContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}