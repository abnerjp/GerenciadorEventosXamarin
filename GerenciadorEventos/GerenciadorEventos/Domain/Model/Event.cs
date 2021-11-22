using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorEventos.Domain.Model
{
    public class Event
    {
        public string EventId { get; set; } = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
        public DateTime DateTime { get; set; }
        public string EventName { get; set; }
        public string Local { get; set; }
        public string Organization { get; set; }
        public string Contact { get; set; }
        public string Link { get; set; }
    }
}
