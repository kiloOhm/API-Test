using NHibernate;
using NHibernate_implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Models
{
    /// <summary>
    /// Repräsentiert einen Kundenauftrag
    /// </summary>
    public class AuftragBP
    {
        /// <summary>
        /// Name des Kunden
        /// </summary>
        public virtual string Kunde { get; set; }
        /// <summary>
        /// Position des abzuholenden Objekts
        /// </summary>
        public virtual string Position { get; set; }
        /// <summary>
        /// Volumen des abzuholenden Objekts
        /// </summary>
        public virtual int Volumen { get; set; }
        /// <summary>
        /// Zeitpunkt zu dem der Auftrag vollendet sein muss.
        /// </summary>
        public virtual DateTime Deadline { get; set; }
    }
}
