using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore
{
    public class LibroManager : BaseManager
    {
        public void Create(Libro l)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(l.Isbn) || string.IsNullOrWhiteSpace(l.Titulo))
                    throw new Exception("ISBN y Título son obligatorios.");

                if (l.Copias < 0 || l.Disponibles < 0)
                    throw new Exception("Copias y Disponibles no pueden ser negativos.");

                if (l.Disponibles > l.Copias)
                    throw new Exception("Disponibles no puede ser mayor que Copias.");

                var lCrud = new LibroCrudFactory();
                var existing = lCrud.RetrieveAll<Libro>().FirstOrDefault(x => x.Isbn == l.Isbn);
                if (existing != null)
                    throw new Exception("Ya existe un libro con ese ISBN.");

                l.Created = DateTime.Now;
                lCrud.Create(l);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Update(Libro l)
        {
            try
            {
                if (l.Disponibles > l.Copias)
                    throw new Exception("Disponibles no puede ser mayor que Copias.");

                var lCrud = new LibroCrudFactory();
                lCrud.Update(l);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Delete(Libro l)
        {
            var lCrud = new LibroCrudFactory();
            lCrud.Delete(l);
        }

        public List<Libro> RetrieveAll()
        {
            var lCrud = new LibroCrudFactory();
            return lCrud.RetrieveAll<Libro>();
        }

        public Libro RetrieveById(int id)
        {
            var lCrud = new LibroCrudFactory();
            return lCrud.RetrieveById<Libro>(id);
        }
    }
}
