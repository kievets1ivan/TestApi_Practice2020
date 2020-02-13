using System;
using System.Collections.Generic;
using System.Text;

namespace TestApi.BL.Services.Interfaces
{
    public interface IDbTransactionService
    {
        void BeginTransaction();
        void Commit();
        void Dispose();
        void RollBack();
    }
}
