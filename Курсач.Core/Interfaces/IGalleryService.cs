using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Курсач.Core.Data.DTO;
using Курсач.Core.Data.Entities;

namespace Курсач.Core.Interfaces
{
    public interface IGalleryService
    {
        Task<BelongToGallery> CreateGallery(GalleryData galleryData);
        Task<BelongToGallery> GetGallery(int id);
        Task DeletePictureFromGallery(int idPicture);
    }
}
