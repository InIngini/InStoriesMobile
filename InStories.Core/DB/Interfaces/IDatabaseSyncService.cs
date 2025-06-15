using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InStories.Core.DB.Interfaces
{
    public interface IDatabaseSyncService
    {
        Task SyncDatabasesAsync(int userId);
    }
}
