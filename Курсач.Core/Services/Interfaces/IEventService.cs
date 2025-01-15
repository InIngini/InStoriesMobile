using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;

namespace Курсач.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateEvent(EventData eventData);
        Task<Event> GetEvent(int id);
        Task UpdateEvent(int id, EventData eventData);
        Task DeleteEvent(int id);
    }
}
