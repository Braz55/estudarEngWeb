using ficha3.Models;

namespace ficha3.Data
{
    public class DbInicializer
    {
        private ficha3Context _context;

        public DbInicializer(ficha3Context context)
        {
            _context = context;
        }

        public void Run()
        {
            _context.Database.EnsureCreated();  //cria a base ded dados se ela n existir

            if (_context.Category.Any())
            {
                return;
            }

            var categorias = new Category[]
            {
                new Category { name = "programacao", description="programacao e cenas de nerd" },
                new Category { name = "administaçao", description="dinheiro e cenas de beto" },
                new Category { name = "comunicacao", description="falar e cenas de " }

            };

            _context.Category.AddRange(categorias); //É uma forma mais rápida e limpa de adicionar vários registos de uma vez.

            _context.SaveChanges();
        }
    }
}
