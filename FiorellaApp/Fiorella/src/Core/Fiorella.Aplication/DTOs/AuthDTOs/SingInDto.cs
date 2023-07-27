using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.DTOs.AuthDTOs
{
    public record SingInDto(string UserOrEmail , string password);
}
