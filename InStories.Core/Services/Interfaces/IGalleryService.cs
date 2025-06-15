using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InStories.Core.Data.DTO;
using InStories.Core.Data.Entities;

namespace InStories.Core.Services.Interfaces
{
    public interface IGalleryService
    {
        Task<BelongToGallery> CreateGallery(GalleryData galleryData);
        Task<BelongToGallery> GetGallery(int id);
        Task DeletePictureFromGallery(int idPicture);
    }
}
