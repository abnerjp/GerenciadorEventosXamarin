using GerenciadorEventos.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorEventos.Domain.Validator
{
    public class EventValidator
    {
        public Event EventToValidate { get; set; }

        public List<string> Erros { get; } = new List<string>();


        private bool IsNullObject()
        {
            return EventToValidate == null;
        }

        public bool IsValid()
        {

            if(IsNullObject()) 
                Erros.Add("O objeto está nulo");

            if (EventToValidate.EventName == null || EventToValidate.EventName.Equals(""))
                Erros.Add("O nome do evento não pode ser vazio");

            if (EventToValidate.DateTime== null || EventToValidate.DateTime < DateTime.Now)
                Erros.Add("A data/hora deve ser maior do que a atual");

            if (EventToValidate.Local == null || EventToValidate.Local.Equals(""))
                Erros.Add("O local não pode ser vazio");

            if (EventToValidate.Organization == null || EventToValidate.Organization.Equals(""))
                Erros.Add("A organização não pode ser vazia");

            if (EventToValidate.Contact == null || EventToValidate.Contact.Equals(""))
                Erros.Add("O contato não pode ser vazio");

            return Erros.Count == 0;
        }
    }
}
