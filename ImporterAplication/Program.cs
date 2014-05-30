using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookDA;
using Google.Apis.Books.v1.Data;

namespace ImporterAplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BookContext();
            var da = new BookApiAccess.BookDA();
            string ISBN = string.Empty;
            ISBN = Console.ReadLine();
            while (ISBN != "END")
            {

                if (context.Books.Where(b => b.ISBN == ISBN).Count() > 0)
                {
                    Console.WriteLine("El libro ya se encuentra cargado");
                }
                else
                {
                    var volume = da.GetBookInfo(ISBN);
                    if (volume != null)
                    {
                        Console.WriteLine(volume.Title);
                        Console.WriteLine(volume.Publisher);
                        Console.WriteLine(volume.Subtitle);
                        Console.WriteLine(volume.Description);
                        context.Books.Add(volume);
                        if (volume.Authors != null)
                        {
                            foreach (var au in volume.Authors)
                            {
                                if (context.Authors.Where(a => a.Name == au.Name).Count() == 0)
                                {
                                    context.Authors.Add(au);
                                }
                            }
                        }
                        if (volume.Categories != null)
                        {
                            foreach (var ct in volume.Categories)
                            {
                                if (context.Categories.Where(c => c.Name == ct.Name).Count() == 0)
                                {
                                    context.Categories.Add(ct);
                                }
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("No se encontro el ISBN");
                    }
                    Console.WriteLine("----------------------------------------------------------");
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                ISBN = Console.ReadLine();
            }


        }
    }
}
