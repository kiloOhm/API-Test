using NHibernate;
using NHibernate_implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Auftrag
    {
        public virtual Guid ID { get; set; }
        public virtual string Kunde { get; set; }
        public virtual string Position { get; set; }
        public virtual int Volumen { get; set; }
        public virtual DateTime Deadline { get; set; }

        public Auftrag() { }

        public override string ToString()
        {
            return $"{ID} {Kunde} {Position} {Volumen} {Deadline}";
        }

        public static List<Auftrag> getAuftrag(Dictionary<string,string> parameters)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            StringBuilder sb = new StringBuilder("from Auftrag");
            if(parameters != null && parameters.Count > 0)
            {
                sb.Append(" where");
                int i = 0;
                foreach(string key in parameters.Keys)
                {
                    if (i != 0) sb.Append(" and ");
                    sb.Append($" {key} = '{parameters[key]}'");
                    i++;
                }
            }
            IQuery query = session.CreateQuery(sb.ToString());
            IList<Auftrag> auftraege = query.List<Auftrag>();
            session.Close();
            return auftraege.ToList();
        }

        public static bool addAuftrag(Auftrag auftrag)
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
                Console.WriteLine("Auftrag mit dieser ID existiert bereits");
            }
            session.Close();
            return true;
        }

        public static bool updateAuftrag(Auftrag auftrag)
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
                Console.WriteLine("Auftrag mit dieser ID existiert nicht");
            }
            session.Close();
            return true;
        }

        public static bool deleteAuftrag(Auftrag auftrag)
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
                Console.WriteLine("keine Aufträge gefunden");
            }
            session.Close();
            return true;
        }
    }
}
