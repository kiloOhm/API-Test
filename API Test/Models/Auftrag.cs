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
    public class Auftrag
    {
        /// <summary>
        /// Eindeutige Identifikation
        /// </summary>
        public virtual Guid ID { get; set; }
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
        /// Zeitpunkt zu dem der Auftrag vollendet sein muss
        /// </summary>
        public virtual DateTime Deadline { get; set; }

        public Auftrag() { }

        public override string ToString()
        {
            return $"{ID} {Kunde} {Position} {Volumen} {Deadline}";
        }

        public static List<Auftrag> getAuftrag(Auftrag auftrag)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            StringBuilder sb = new StringBuilder("from Auftrag");
            int i = 0;
            foreach(PropertyInfo property in typeof(Auftrag).GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;

                object value = property.GetValue(auftrag, null);
                if (value is int) if ((int)value == 0) value = null;
                if (value is Guid) if (((Guid)value) == Guid.Empty) value = null;
                if (value is DateTime) if ((DateTime)value == DateTime.MinValue) value = null;
                if (value == null)
                    continue;

                if (i == 0) sb.Append(" where ");
                else sb.Append(" and ");
                sb.Append($"{property.Name} = '{value}'");
                i++;
            }
            IQuery query = session.CreateQuery(sb.ToString());
            IList<Auftrag> auftraege = query.List<Auftrag>();
            session.Close();
            return auftraege.ToList();
        }

        public static string addAuftrag(Auftrag auftrag)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(auftrag);
                    transaction.Commit();
                }
            }
            catch(Exception e)
            {
                return "Auftrag mit dieser ID existiert bereits\n" + e.Message;
            }
            session.Close();
            return "";
        }

        public static string updateAuftrag(Auftrag auftrag)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(auftrag);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                return "Auftrag mit dieser ID existiert nicht\n" + e.Message;
            }
            session.Close();
            return "";
        }

        public static string deleteAuftrag(Auftrag auftrag)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(auftrag);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                return "keine Aufträge gefunden\n" + e.Message;
            }
            session.Close();
            return "";
        }
    }
}
