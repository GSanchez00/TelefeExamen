using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TelefeWebApi.Models;
using System.Web.Mvc;

namespace TelefeWebApi.Controllers
{
    public class HistorialController : ApiController
    {
        public IEnumerable<Historial> Get()
        {
            using (HistorialDBEntities db = new HistorialDBEntities())
            {
               return db.Historial.ToList();
            }
        }

        public void Insert (string record)
        {
            Historial _historial= new Historial();
            _historial.Valor=record;
            using (HistorialDBEntities db = new HistorialDBEntities())
            {
                db.Historial.Add(_historial);
                db.SaveChanges();
            }
        }

    }
}
