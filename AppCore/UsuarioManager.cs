using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AppCore
{
    public class UsuarioManager : BaseManager
    {
        private readonly EmailManager _emailManager;

        public UsuarioManager(EmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        public void Create(Usuario u)
        {
            try
            {
                if (!IsOver18(u))
                    throw new Exception("Solo mayores de edad pueden registrarse.");

                if (!IsValidEmail(u.Email))
                    throw new Exception("Formato de correo inválido.");

                var uCrud = new UsuarioCrudFactory();
                var existing = uCrud.RetrieveAll<Usuario>().FirstOrDefault(x => x.Email == u.Email);
                if (existing != null)
                    throw new Exception("Ya existe un usuario con ese correo.");

                u.Created = DateTime.Now;
                uCrud.Create(u);

                _emailManager.SendWelcomeEmail(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Update(Usuario u)
        {
            try
            {
                if (!IsOver18(u))
                    throw new Exception("Solo mayores de edad pueden registrarse.");

                if (!IsValidEmail(u.Email))
                    throw new Exception("Formato de correo inválido.");

                var uCrud = new UsuarioCrudFactory();
                uCrud.Update(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public void Delete(Usuario u)
        {
            try
            {
                var uCrud = new UsuarioCrudFactory();
                uCrud.Delete(u);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
            }
        }

        public List<Usuario> RetrieveAll()
        {
            var uCrud = new UsuarioCrudFactory();
            return uCrud.RetrieveAll<Usuario>();
        }

        public Usuario RetrieveById(int id)
        {
            var uCrud = new UsuarioCrudFactory();
            return uCrud.RetrieveById<Usuario>(id);
        }

        private bool IsOver18(Usuario u)
        {
            if (u.BirthDate == DateTime.MinValue)
                throw new Exception("La fecha de nacimiento es requerida.");

            var today = DateTime.Today;
            var age = today.Year - u.BirthDate.Year;
            if (u.BirthDate.Date > today.AddYears(-age)) age--;

            return age >= 18;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
