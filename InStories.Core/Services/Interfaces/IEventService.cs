using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateEvent(EventData eventData);
        Task<Event> GetEvent(int id);
        Task UpdateEvent(int id, EventData eventData);
        Task DeleteEvent(int id);
    }
}
