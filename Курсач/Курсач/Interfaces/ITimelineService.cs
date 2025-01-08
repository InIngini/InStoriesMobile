using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.DTO;
using Курсач.Data.Entities;

namespace Курсач.Core.Interfaces
{
    public interface ITimelineService
    {
        Task<Timeline> CreateTimeline(TimelineData timelineData);
        Task<Timeline> GetTimeline(int id);
        Task UpdateTimeline(int id, int eventId);
        Task DeleteTimeline(int id);
    }
}
