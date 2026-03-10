using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        var uc = new UsuarioCrudFactory();
        var lc = new LibroCrudFactory();
        var pc = new PrestamoCrudFactory();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\n MENU PRINCIPAL ");
            Console.WriteLine("1. CRUD Usuario");
            Console.WriteLine("2. CRUD Libro");
            Console.WriteLine("3. CRUD Prestamo");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuUsuario(uc);
                    break;

                case "2":
                    MenuLibro(lc);
                    break;

                case "3":
                    MenuPrestamo(pc);
                    break;

                case "4":
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }

    // Métodos de menú
    private static void MenuUsuario(UsuarioCrudFactory uc)
    {
        bool salirUsuario = false;
        while (!salirUsuario)
        {
            Console.WriteLine("\n MENU CRUD USUARIO ");
            Console.WriteLine("1. Crear usuario");
            Console.WriteLine("2. Actualizar usuario");
            Console.WriteLine("3. Eliminar usuario");
            Console.WriteLine("4. Listar todos los usuarios");
            Console.WriteLine("5. Consultar usuario por ID");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    var usuario = new Usuario();
                    Console.WriteLine("Ingrese: Nombre, Apellido, Password, Email, FechaNacimiento (yyyy-MM-dd), Estado, Rol - separados por coma:");
                    var uText = Console.ReadLine();
                    var uVals = uText.Split(",");

                    usuario.Name = uVals[0];
                    usuario.LastName = uVals[1];
                    usuario.Password = uVals[2];
                    usuario.Email = uVals[3];
                    usuario.BirthDate = DateTime.Parse(uVals[4]);
                    usuario.Status = uVals[5];
                    usuario.Rol = uVals[6];
                    usuario.Created = DateTime.Now;

                    uc.Create(usuario);
                    Console.WriteLine("Usuario creado correctamente.");
                    break;

                case "2":
                    Console.WriteLine("Ingrese: Id, Nombre, Apellido, Password, Email, FechaNacimiento, Estado, Rol - separados por coma:");
                    var updText = Console.ReadLine();
                    var updVals = updText.Split(",");

                    var updUsuario = new Usuario
                    {
                        Id = int.Parse(updVals[0]),
                        Name = updVals[1],
                        LastName = updVals[2],
                        Password = updVals[3],
                        Email = updVals[4],
                        BirthDate = DateTime.Parse(updVals[5]),
                        Status = updVals[6],
                        Rol = updVals[7],
                        Updated = DateTime.Now
                    };

                    uc.Update(updUsuario);
                    Console.WriteLine("Usuario actualizado correctamente.");
                    break;

                case "3":
                    Console.Write("Ingrese el ID del usuario a eliminar: ");
                    int idDel = int.Parse(Console.ReadLine());
                    var usuarioToDelete = uc.RetrieveById<Usuario>(idDel);

                    if (usuarioToDelete == null)
                    {
                        Console.WriteLine($"No se encontró un usuario con el ID {idDel}.");
                    }
                    else
                    {
                        uc.Delete(usuarioToDelete);
                        Console.WriteLine("Usuario eliminado correctamente.");
                    }
                    break;

                case "4":
                    var usuarios = uc.RetrieveAll<Usuario>();
                    Console.WriteLine("\n--- Lista de Usuarios ---");
                    foreach (var us in usuarios)
                    {
                        Console.WriteLine($"ID: {us.Id}, Nombre: {us.Name} {us.LastName}, Email: {us.Email}, Estado: {us.Status}, Rol: {us.Rol}");
                    }
                    break;

                case "5":
                    Console.Write("Ingrese el ID del usuario a consultar: ");
                    int idUsuario = int.Parse(Console.ReadLine());
                    var usuarioConsultado = uc.RetrieveById<Usuario>(idUsuario);
                    if (usuarioConsultado != null)
                    {
                        Console.WriteLine($"ID: {usuarioConsultado.Id}, Nombre: {usuarioConsultado.Name} {usuarioConsultado.LastName}, Email: {usuarioConsultado.Email}, Estado: {usuarioConsultado.Status}, Rol: {usuarioConsultado.Rol}");
                    }
                    else
                    {
                        Console.WriteLine("Usuario no encontrado.");
                    }
                    break;

                case "6":
                    salirUsuario = true;
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }


    private static void MenuLibro(LibroCrudFactory lc)
    {
        bool salirLibro = false;
        while (!salirLibro)
        {
            Console.WriteLine("\n MENU CRUD LIBRO ");
            Console.WriteLine("1. Crear libro");
            Console.WriteLine("2. Actualizar libro");
            Console.WriteLine("3. Eliminar libro");
            Console.WriteLine("4. Listar todos los libros");
            Console.WriteLine("5. Consultar libro por ID");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    var libro = new Libro();
                    Console.WriteLine("Ingrese: ISBN, Titulo, Autor, Categoria, Copias, Disponibles - separados por coma:");
                    var lText = Console.ReadLine();
                    var lVals = lText.Split(",");

                    libro.Isbn = lVals[0];
                    libro.Titulo = lVals[1];
                    libro.Autor = lVals[2];
                    libro.Categoria = lVals[3];
                    libro.Copias = int.Parse(lVals[4]);
                    libro.Disponibles = int.Parse(lVals[5]);
                    libro.Created = DateTime.Now;

                    lc.Create(libro);
                    Console.WriteLine("Libro creado correctamente.");
                    break;

                case "2":
                    Console.WriteLine("Ingrese: Id, ISBN, Titulo, Autor, Categoria, Copias, Disponibles - separados por coma:");
                    var updText = Console.ReadLine();
                    var updVals = updText.Split(",");

                    var updLibro = new Libro
                    {
                        Id = int.Parse(updVals[0]),
                        Isbn = updVals[1],
                        Titulo = updVals[2],
                        Autor = updVals[3],
                        Categoria = updVals[4],
                        Copias = int.Parse(updVals[5]),
                        Disponibles = int.Parse(updVals[6]),
                        Updated = DateTime.Now
                    };

                    lc.Update(updLibro);
                    Console.WriteLine("Libro actualizado correctamente.");
                    break;

                case "3":
                    Console.Write("Ingrese el ID del libro a eliminar: ");
                    int idDel = int.Parse(Console.ReadLine());
                    var libroToDelete = lc.RetrieveById<Libro>(idDel);

                    if (libroToDelete == null)
                    {
                        Console.WriteLine($"No se encontró un libro con el ID {idDel}.");
                    }
                    else
                    {
                        lc.Delete(libroToDelete);
                        Console.WriteLine("Libro eliminado correctamente.");
                    }
                    break;

                case "4":
                    var libros = lc.RetrieveAll<Libro>();
                    Console.WriteLine("\n--- Lista de Libros ---");
                    foreach (var lb in libros)
                    {
                        Console.WriteLine($"ID: {lb.Id}, ISBN: {lb.Isbn}, Titulo: {lb.Titulo}, Autor: {lb.Autor}, Categoria: {lb.Categoria}, Copias: {lb.Copias}, Disponibles: {lb.Disponibles}");
                    }
                    break;

                case "5":
                    Console.Write("Ingrese el ID del libro a consultar: ");
                    int idLibro = int.Parse(Console.ReadLine());
                    var libroConsultado = lc.RetrieveById<Libro>(idLibro);
                    if (libroConsultado != null)
                    {
                        Console.WriteLine($"ID: {libroConsultado.Id}, ISBN: {libroConsultado.Isbn}, Titulo: {libroConsultado.Titulo}, Autor: {libroConsultado.Autor}, Categoria: {libroConsultado.Categoria}, Copias: {libroConsultado.Copias}, Disponibles: {libroConsultado.Disponibles}");
                    }
                    else
                    {
                        Console.WriteLine("Libro no encontrado.");
                    }
                    break;

                case "6":
                    salirLibro = true;
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }



    private static void MenuPrestamo(PrestamoCrudFactory pc)
    {
        bool salirPrestamo = false;
        while (!salirPrestamo)
        {
            Console.WriteLine("\n MENU CRUD PRESTAMO ");
            Console.WriteLine("1. Crear préstamo");
            Console.WriteLine("2. Actualizar préstamo");
            Console.WriteLine("3. Eliminar préstamo");
            Console.WriteLine("4. Listar todos los préstamos");
            Console.WriteLine("5. Consultar préstamo por ID");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    var prestamo = new Prestamo();
                    Console.WriteLine("Ingrese: ISBN, UsuarioId, FechaPrestamo (yyyy-MM-dd), FechaLimite (yyyy-MM-dd), Estado - separados por coma:");
                    var pText = Console.ReadLine();
                    var pVals = pText.Split(",");

                    prestamo.Isbn = pVals[0];
                    prestamo.UsuarioId = int.Parse(pVals[1]);
                    prestamo.FechaPrestamo = DateTime.Parse(pVals[2]);
                    prestamo.FechaLimite = DateTime.Parse(pVals[3]);
                    prestamo.Estado = pVals[4];
                    prestamo.Created = DateTime.Now;

                    pc.Create(prestamo);
                    Console.WriteLine("Préstamo creado correctamente.");
                    break;

                case "2":
                    Console.WriteLine("Ingrese: id, ISBN, UsuarioId, FechaPrestamo, FechaLimite, FechaDevolucion (opcional), Estado - separados por coma:");
                    var updText = Console.ReadLine();
                    var updVals = updText.Split(",");

                    var updPrestamo = new Prestamo
                    {
                        Id = int.Parse(updVals[0]),
                        Isbn = updVals[1],
                        UsuarioId = int.Parse(updVals[2]),
                        FechaPrestamo = DateTime.Parse(updVals[3]),
                        FechaLimite = DateTime.Parse(updVals[4]),
                        FechaDevolucion = string.IsNullOrWhiteSpace(updVals[5]) ? (DateTime?)null : DateTime.Parse(updVals[5]),
                        Estado = updVals[6],
                        Updated = DateTime.Now
                    };

                    pc.Update(updPrestamo);
                    Console.WriteLine("Préstamo actualizado correctamente.");
                    break;

                case "3":
                    Console.Write("Ingrese el ID del préstamo a eliminar: ");
                    int idDel = int.Parse(Console.ReadLine());
                    var prestamoToDelete = pc.RetrieveById<Prestamo>(idDel);

                    if (prestamoToDelete == null)
                    {
                        Console.WriteLine($"No se encontró un préstamo con el ID {idDel}.");
                    }
                    else
                    {
                        pc.Delete(prestamoToDelete);
                        Console.WriteLine("Préstamo eliminado correctamente.");
                    }
                    break;

                case "4":
                    var prestamos = pc.RetrieveAll<Prestamo>();
                    Console.WriteLine("\n--- Lista de Préstamos ---");
                    foreach (var pr in prestamos)
                    {
                        Console.WriteLine($"ID: {pr.Id}, ISBN: {pr.Isbn}, UsuarioId: {pr.UsuarioId}, Estado: {pr.Estado}, FechaPrestamo: {pr.FechaPrestamo.ToShortDateString()}, FechaLimite: {pr.FechaLimite.ToShortDateString()}, FechaDevolucion: {(pr.FechaDevolucion.HasValue ? pr.FechaDevolucion.Value.ToShortDateString() : "Pendiente")}");
                    }
                    break;

                case "5":
                    Console.Write("Ingrese el ID del préstamo a consultar: ");
                    int idPrestamo = int.Parse(Console.ReadLine());
                    var prestamoConsultado = pc.RetrieveById<Prestamo>(idPrestamo);
                    if (prestamoConsultado != null)
                    {
                        Console.WriteLine($"ID: {prestamoConsultado.Id}, ISBN: {prestamoConsultado.Isbn}, UsuarioId: {prestamoConsultado.UsuarioId}, Estado: {prestamoConsultado.Estado}, FechaPrestamo: {prestamoConsultado.FechaPrestamo.ToShortDateString()}, FechaLimite: {prestamoConsultado.FechaLimite.ToShortDateString()}, FechaDevolucion: {(prestamoConsultado.FechaDevolucion.HasValue ? prestamoConsultado.FechaDevolucion.Value.ToShortDateString() : "Pendiente")}");
                    }
                    else
                    {
                        Console.WriteLine("Préstamo no encontrado.");
                    }
                    break;

                case "6":
                    salirPrestamo = true;
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
