using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppControle.Classes.TO
{
    public class TOProvidencia
    {
        public int ID_PROVIDENCIA { get; set; }
        public string ID_DEMANDA { get; set; }
        public string DE_OBSERVACOES { get; set; }
        public DateTime DT_INCLUSAO { get; set; }
        public bool ST_RESOLVIDO { get; set; }
        public DateTime? DT_LIMITE { get; set; }
        public int? ID_PESSOA_RESPONSAVEL { get; set; }
        public bool ST_CPD { get; set; }

    }
}