using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDotNetCore.Models
{
    public class Usuario
    {
        string Nome { set; get; }
        int Idade { set; get; }
        char Sexo { set; get; }
        string Email { set; get; }
    }
}
