using Microsoft.AspNetCore.Http;

namespace ConectElo.Application.Areas.Social.DTOs.Perfil
{
    public class UploadFotoDto
    {
        public IFormFile Foto { get; set; }
    }
}
