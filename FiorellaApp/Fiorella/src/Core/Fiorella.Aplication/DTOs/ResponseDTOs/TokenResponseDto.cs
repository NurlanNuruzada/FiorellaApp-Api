using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.DTOs.ResponseDTOs;
public record TokenResponseDto(string token ,DateTime expireDate);

