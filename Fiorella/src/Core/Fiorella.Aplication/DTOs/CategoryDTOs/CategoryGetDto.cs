using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Aplication.DTOs.CategoryDTOs;

public record CategoryGetDto(Guid Id, string name, string description);
