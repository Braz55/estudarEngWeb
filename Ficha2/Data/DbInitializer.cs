using Ficha2.Models;

namespace Ficha2.Data
{
    public class DbInitializer
    {
        private Ficha2Context _context;

        public DbInitializer(Ficha2Context context)
        {
            _context = context;
        }
        public void run()
        {
            _context.Database.EnsureCreated();
            if(_context.Category.Any())
            {
                return;
            }

            var categorias = new Category[]
            {
                new Category {name= "Programing", description="algoritmos e programacao area courses",date=DateTime.Now},
                new Category {name="administracao", description="administracao publica e gerenciamento de negocio",date=DateTime.Now},
                new Category {name="comunicacao", description=" negocios e comunicaocao instutucional cursos",date=DateTime.Now }
            };
            _context.Category.AddRange(categorias);

            _context.SaveChanges();
        }
    }
}
