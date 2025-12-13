using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ficha5.Data;
using ficha5.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace ficha5.Controllers
{
    public class BooksController : Controller
    {
        private readonly ficha5Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(ficha5Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _webHostEnvironment = environment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Title,CoverPhoto,Document")] BoolViewModel book)
        {
            var PhotoExtensiosns = new[] { ".jpg", ".jpeg", ".png", ".gif" };       //listas com os formatos permitidos
            var DocumentExtensions = new[] { ".pdf", ".doc", ".docx", ".txt" };

            var extension = Path.GetExtension(book.CoverPhoto.FileName).ToLower();      //verifica a extensao do ficheiro inserido com a foto (ver o tipo de foto)
            if (!PhotoExtensiosns.Contains(extension))      //se a extensao nao estiver na lista de permitidas entao adiciona um erro ao ModelState
            {
                ModelState.AddModelError("CoverPhoto", "Invalid photo file type. Allowed types are: " + string.Join(", ", PhotoExtensiosns));
            }
            extension = Path.GetExtension(book.Document.FileName).ToLower();        //verifica a extensao do ficheiro inserido com o documento
            if (!DocumentExtensions.Contains(extension))
            {
                ModelState.AddModelError("Document", "Invalid document file type. Allowed types are: " + string.Join(", ", DocumentExtensions));
            }
            //se o modelo estiver valido entao prossegue com a criacao do livro na base de dados
            if (ModelState.IsValid)     
            {
                //cria um novo objeto do tipo Book
                var newBook = new Book();

                //atribui os valores do modelo de viewmodel ao modelo de Book
                newBook.Title = book.Title;     
                newBook.CoverPhoto = Path.GetFileName(book.CoverPhoto.FileName);
                newBook.Document = Path.GetFileName(book.Document.FileName);

                
                string coverFileName = Path.GetFileName(book.CoverPhoto.FileName);      //obter o nome do ficheiro da foto, ou seja é o nome com que vai ser guardado no servidor
                string coverFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "cover", coverFileName);  //criar o caminho completo onde a foto vai ser guardada no servidor, na pasta cover


                using (var stream = new FileStream(coverFullPath, FileMode.Create))     //cria um novo ficheiro no caminho especificado para gravar a foto
                {
                    await book.CoverPhoto.CopyToAsync(stream);
                }

                string docFileName = Path.GetFileName(book.Document.FileName);      //obter o nome do ficheiro do documento para ser guardado, ou seja é o nome com que vai ser guardado no servidor
                string docFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents", docFileName);  //criar o caminho completo onde o documento vai ser guardado no servidor, na pasta documents


                using (var stream = new FileStream(docFullPath, FileMode.Create))
                {
                    await book.Document.CopyToAsync(stream);   //cria um novo ficheiro no caminho especificado para gravar o documento
                }

                //gurdar na base de dados a nova entrada do livro
                _context.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  //redirecionar para a lista de livros
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CoverPhoto,Document")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                if(!string.IsNullOrEmpty(book.CoverPhoto))    //eliminar os ficheiros associados ao livro
                {
                    var coverPath = Path.Combine(_webHostEnvironment.WebRootPath, "cover", book.CoverPhoto);
                    if (System.IO.File.Exists(coverPath))
                    {
                        System.IO.File.Delete(coverPath);
                    }
                }
                if (!string.IsNullOrEmpty(book.Document))
                {
                    var documentPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents", book.Document);
                    if (System.IO.File.Exists(documentPath))
                    {
                        System.IO.File.Delete(documentPath);
                    }
                }
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        public IActionResult Download(string? id)       //o id corresponde ao nome do ficheiro a transferir
        {
            string pathFile= Path.Combine(_webHostEnvironment.WebRootPath, "documents", id);        //caminho completo do ficheiro a transferir, localizado na pasta documents da wwwroot, usando o nome do ficheiro recebido no id e "documents" refere se a pasta onde o ficheiro esta guardado
            byte[] fileBytes = System.IO.File.ReadAllBytes(pathFile);                               //ler os bytes do ficheiro para um array de bytes

            string mineType;        //obter o tipo de ficheiro para o cabeçalho da resposta, através da extensão do ficheiro

            if (new FileExtensionContentTypeProvider().TryGetContentType(id, out mineType) == false)    //se nao conseguir obter o tipo de ficheiro
            {
                mineType = "application/force-download"; //correcao tipo de arquivo
            }
            return File(fileBytes, mineType);    //restitui o ficheiro com o tipo de conteudo correcto
        }
    }


}
