using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppControle.Classes.TO
{
    public class TOInformacao
    {
        public int ID_INFORMACAO { get; set; }
        public string ID_DEMANDA { get; set; }
        public string ID_PESSOA_REQUISITO { get; set; }
        public string DE_LINGUAGEM { get; set; }
        public string DE_COMENTARIOS { get; set; }
        public int ST_IMPEDIMENTO { get; set; }
    }
}