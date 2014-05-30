using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Google.Apis.Books.v1.Data;
using Google.Apis.Services;

namespace BookApiAccess
{
    public class BookDA
    {

        public Book GetBookInfo(string isbn)
        {
            Book book = null;
            var service = new Google.Apis.Books.v1.BooksService();
            var list = service.Volumes.List(String.Format("ISBN={0}",isbn)).Execute();
            if (list.Items != null && list.Items.Count > 0)
            {
                book = LoadBook(list.Items[0], isbn);

                return book;
            }
            else
            {
                return null;
            }
        }

        private Book LoadBook(Volume volume, string isbn)
        {
            var salida = new Book();
            if (volume.VolumeInfo != null)
            {
                if(volume.VolumeInfo.Authors != null)
                    salida.Authors = volume.VolumeInfo.Authors.Select(a => new Author() { Name = a });
                if(volume.VolumeInfo.Categories != null)
                    salida.Categories = volume.VolumeInfo.Categories.Select(c => new Category() { Name = c });
                salida.Description = volume.VolumeInfo.Description;
                salida.PageCount = volume.VolumeInfo.PageCount;
                salida.PreviewLink = volume.VolumeInfo.PreviewLink;
                salida.PrintedPageCount = volume.VolumeInfo.PrintedPageCount;
                salida.PrintType = volume.VolumeInfo.PrintType;
                salida.PublishedDate = volume.VolumeInfo.PublishedDate;
                salida.Subtitle = volume.VolumeInfo.Subtitle;
                salida.Title = volume.VolumeInfo.Title;
                salida.Publisher = volume.VolumeInfo.Publisher;
                salida.ISBN = isbn;

                if (volume.VolumeInfo.ImageLinks != null && !String.IsNullOrEmpty(volume.VolumeInfo.ImageLinks.Thumbnail))
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(volume.VolumeInfo.ImageLinks.Thumbnail, String.Format("{0}-T.gif", isbn));
                }

            }


            return salida;
        }

    }
}
