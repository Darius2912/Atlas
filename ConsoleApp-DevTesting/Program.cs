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
        // Aquí va tu CRUD de Usuario (ya lo tienes implementado)
    }

    private static void MenuLibro(LibroCrudFactory lc)
    {
        // Aquí va tu CRUD de Libro (ya lo tienes implementado)
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
