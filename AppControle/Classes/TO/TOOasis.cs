using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppControle.Classes.TO
{
    public class TOOasis
    {

        public string ID_DEMANDA { get; set; }
        public string DE_DEMANDA { get; set; }
        public string NU_PARCELA { get; set; }
        public string DE_SISTEMA { get; set; }
        public string DE_TIPO_DEMANDA { get; set; }
        public int ST_SUSTENTACAO { get; set; }
        public string DE_PRIORIDADE { get; set; }
        public string DE_ASSUNTO_SOLICITACAO { get; set; }
        public string DE_GESTOR_TECNICO { get; set; }
        public string DE_GESTOR_OPERACIONAl { get; set; }
        public decimal VL_TOTAL_PF_PROPOSTA { get; set; }
        public decimal VL_CONTAGEM_ESTIMADA { get; set; }
        public decimal VL_CONTAGEM_DETALHADA { get; set; }
        public decimal VL_HORAS { get; set; }
        public decimal VL_PF { get; set; }
        public string DE_INM { get; set; }
        public string DE_CONTRATO { get; set; }
        public string DE_SITUACAO_DEMANDA { get; set; }
        public string DE_SITUACAO_PARCELA { get; set; }
        public DateTime? DT_ULTIMA_ATUALIZACAO { get; set; }
        public DateTime? DT_AUTORIZACAO { get; set; }
        public DateTime? DT_PRAZO_LIMITE_PROPOSTA { get; set; }
        public string DE_FECHAMENTO_PROPOSTA { get; set; }
        public string DE_ACEITE_PROPOSTA { get; set; }
        public string DE_PARECER_PROPOSTA { get; set; }
        public DateTime? DT_AUTORIZACAO_PARCELA { get; set; }
        public DateTime? DT_PRAZO_LIMITE_PARCELA { get; set; }
        public string DE_FECHAMENTO_PARCELA { get; set; }
        public string DE_PARECER_PARCELA { get; set; }
        public string DE_FECHAMENTO_HOMOLOGACAO { get; set; }
        public string DE_ACEITE_HOMOLOGACAO { get; set; }
        public string DE_FECHAMENTO_PRODUCAO { get; set; }
        public string DE_ACEITE_PRODUCAO { get; set; }
        public string DE_NUMERO_DA_FATURA { get; set; }
        public string ANO_DA_FATURA { get; set; }
        public int ST_GARANTIA { get; set; }
        public string DE_PROFISSIONAL { get; set; }
        public int ID_ANALISTA { get; set; }
        public int ID_REQUISITOS { get; set; }

        public bool aux_fechamento_proposta
        {
            get
            {
                if (ConverterData(DE_FECHAMENTO_PROPOSTA))
                    return true;
                else
                    return false;
            }
        }

        public bool aux_aceite_homol
        {
            get {
               if(ConverterData(DE_ACEITE_HOMOLOGACAO))
                return true;
               else
                return false;
            }
        }

        public bool aux_fechamento_parcela
        {
            get
            {
                if (ConverterData(DE_FECHAMENTO_PARCELA))
                    return true;
                else
                    return false;
            }
        }

        public bool aux_parecer_parcela
        {
            get
            {
                if (ConverterData(DE_PARECER_PARCELA))
                    return true;
                else
                    return false;
            }
        }

        public bool aux_aceite_proposta
        {
            get
            {
                if (ConverterData(DE_ACEITE_PROPOSTA))
                    return true;
                else
                    return false;
            }
        }

        public bool aux_aceite_prod
        {
            get
            {
                if (ConverterData(DE_ACEITE_PRODUCAO))
                    return true;
                else
                    return false;
            }
        }

        private bool ConverterData(string data)
        {
            DateTime dateValue;
            if (DateTime.TryParse(data, out dateValue))
                return true;
            else
                return false;
        }

        public string aux_situacao_parcela
        {
            get
            {
                if (DE_SITUACAO_DEMANDA == "Proposta Rejeitada")
                    return "Proposta Rejeitada";

                if (!aux_fechamento_proposta && !aux_aceite_proposta)
                    return "Entregar Proposta";

                if (aux_fechamento_proposta && !aux_aceite_proposta)
                    return "Proposta com a SEFAZ";

                if (aux_aceite_proposta && aux_aceite_prod && VL_CONTAGEM_DETALHADA == 0)
                    return "Contar métrica Detalhada";

                if (aux_aceite_proposta && !aux_aceite_homol)
                    return "Em construção";

                if (aux_aceite_homol && !aux_aceite_prod)
                    return "Disponibilizar em Produção";

                if (aux_aceite_proposta && aux_aceite_prod && VL_CONTAGEM_DETALHADA > 0 && !aux_fechamento_parcela)
                    return "Fechar Parcela";

                if (aux_aceite_proposta && aux_aceite_prod && VL_CONTAGEM_DETALHADA > 0 && aux_fechamento_parcela)
                    return "Verificar SEFAZ Gerar Fatura";

                    return "Verificar";
            }
        }


    }
}