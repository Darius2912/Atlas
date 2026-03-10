using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCore
{
    public class PrestamoManager : BaseManager
    {
        private readonly EmailManager _emailManager;
        private readonly LibroCrudFactory _libroCrud;
        private readonly UsuarioCrudFactory _usuarioCrud;

        public PrestamoManager(EmailManager emailManager)
        {
            _emailManager = emailManager;
            _libroCrud = new LibroCrudFactory();
            _usuarioCrud = new UsuarioCrudFactory();
        }

        public void Create(Prestamo p)
        {
            try
            {
                var libro = _libroCrud.RetrieveAll<Libro>().FirstOrDefault(x => x.Isbn == p.Isbn);
                if (libro == null)
                    throw new Exception("El libro no existe.");

                if (libro.Disponibles <= 0)
                    throw new Exception("No hay copias disponibles.");

                if (p.FechaPrestamo > p.FechaLimite)
                    throw new Exception("La fecha de préstamo no puede ser mayor a la fecha límite.");

                p.Estado = "Activo";
                p.Created = DateTime.Now;

                var pCrud = new PrestamoCrudFactory();
                pCrud.Create(p);

                libro.Disponibles -= 1;
                _libroCrud.Update(libro);

                var usuario = _usuarioCrud.RetrieveById<Usuario>(p.UsuarioId);
                if (usuario != null)
                {
                    _emailManager.SendPrestamoConfirmacion(p, usuario);
                }
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Update(Prestamo p)
        {
            try
            {
                var pCrud = new PrestamoCrudFactory();
                pCrud.Update(p);

                if (p.Estado == "Devuelto")
                {
                    var libro = _libroCrud.RetrieveAll<Libro>().FirstOrDefault(x => x.Isbn == p.Isbn);
                    if (libro != null)
                    {
                        libro.Disponibles += 1;
                        _libroCrud.Update(libro);
                    }

                    var usuario = _usuarioCrud.RetrieveById<Usuario>(p.UsuarioId);
                    if (usuario != null)
                    {
                        _emailManager.SendConfirmacionDevolucion(p, usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Delete(Prestamo p)
        {
            var pCrud = new PrestamoCrudFactory();
            pCrud.Delete(p);
        }

        public List<Prestamo> RetrieveAll()
        {
            var pCrud = new PrestamoCrudFactory();
            return pCrud.RetrieveAll<Prestamo>();
        }

        public Prestamo RetrieveById(int id)
        {
            var pCrud = new PrestamoCrudFactory();
            return pCrud.RetrieveById<Prestamo>(id);
        }
    }
}
