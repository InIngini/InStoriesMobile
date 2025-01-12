using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Core.DB.Interfaces
{
    public interface IDatabaseSyncService
    {
        Task SyncDatabasesAsync(int userId);
    }
}
