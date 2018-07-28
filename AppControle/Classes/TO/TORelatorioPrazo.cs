using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppControle.Classes.TO
{
    public class TORelatorioPrazo 
    {
        private const string Pendente = "Pendente";

        public string ID_DEMANDA { get; set; }
        public string DE_SISTEMA { get; set; }
        public string REQUISITO { get; set; }
        public string ANALISTA { get; set; }
        public string TIPO { get; set; }
        public string DE_TIPO_DEMANDA { get; set; }
        public string ENVIO_PROPOSTA { get; set; }
        public string ACEITE_PROPOSTA { get; set; }
        public string PARECER_SEF_PROPOSTA { get; set; }
        public string DE_ASSUNTO_SOLICITACAO { get; set; }
        public string PRAZO_PROPOSTA { get; set; }
        public string ENVIO_HOMOL { get; set; }
        public string ACEITE_HOMOL { get; set; }
        public string ENVIO_PROD { get; set; }
        public string ACEITE_PROD { get; set; }
        public string PRAZO_PARCELA { get; set; }
        public string PARCELA_FECHADA { get; set; }
        public decimal VL_CONTAGEM_ESTIMADA { get; set; }
        public decimal VL_CONTAGEM_DETALHADA { get; set; }
        public string DESC_PROVIDENCIA { get; set; }
        public string DE_PARECER_PARCELA { get; set; }
        public bool TemProposta
        {
            get
            {
                if (TIPO == "Sustentação" && DE_TIPO_DEMANDA.ToUpper().StartsWith("CORRETIVA"))
                    return false;

                return true;
            }   
        }
        public string Sustentacao
        {
            get
            {
                if (TIPO == "Sustentação")
                    return "Sim";

                return "Não";
            }
        }
        public bool TemPropostaEnviada
        {
            get
            {
                if (ENVIO_PROPOSTA == Pendente)
                    return false;

                return true;
            }
        }

        public bool TemAceiteProposta
        {
            get
            {
                if (TemPropostaEnviada)
                {
                    if (ACEITE_PROPOSTA != Pendente && PARECER_SEF_PROPOSTA != Pendente)
                        return true;
                }

                if (!TemProposta)
                    return true;

                return false;
            }
        }

        public bool TemAceitesExecucao
        {
            get
            {
                if (ACEITE_PROD != "Pendente")
                    return true;

                return false;
            }
        }

        public bool TemPendenciaFecharParcela
        {
            get
            {
                if (TemAceiteProposta && (PARCELA_FECHADA == Pendente || DE_PARECER_PARCELA == Pendente))
                    return true;

                return false;
            }
        }

     
    }
}