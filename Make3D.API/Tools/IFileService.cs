using Make3D.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Make3D.API.Tools
{
    public interface IFileService
    {
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);
        string SizeConverter(long bytes);
        void UploadFile(List<IFormFile> files, string subDirectory, int articleId, IFichierService fichierService);
    }
}