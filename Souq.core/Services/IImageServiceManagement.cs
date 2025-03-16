using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.core.Services
{
    public interface IImageServiceManagement
    {
        Task<List<string>> AddImagesAsync(IFormFileCollection files , string src);
        void DeleteImage(string src);
    }
}
