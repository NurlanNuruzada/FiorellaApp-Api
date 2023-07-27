using Fiorella.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.Abstraction.Services
{
    public interface IFileService
    {
        Task Upload(FileModels file);
        Task<Stream> Get(string name);
        Task<Stream> Download(string name);
    }
}
