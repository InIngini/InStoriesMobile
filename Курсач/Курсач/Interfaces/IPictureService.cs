using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Data.Entities;

namespace Курсач.Core.Interfaces
{
    public interface IPictureService
    {
        Task<Picture> CreatePicture(Picture picture);
        Task<Picture> GetPicture(int id);
        Task DeletePicture(int id);
    }
}
