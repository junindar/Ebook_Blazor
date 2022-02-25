using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.IService;
using Microsoft.AspNetCore.Hosting;
namespace DataService.Service
{
    public class FileUpload : IFileUpload
    {

       
        private IHostingEnvironment _environment;
        public FileUpload(IHostingEnvironment environment)
        {
            _environment = environment;
        }




        public async Task UploadAsync(MemoryStream file, string fileName)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images", fileName);

            await using FileStream fs = new FileStream(uploads, FileMode.Create, FileAccess.Write);
            file.WriteTo(fs);
        }

     
    }
}
