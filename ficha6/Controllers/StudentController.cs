using ficha6.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ficha6.Controllers
{
    public class StudentController : Controller
    {
        private readonly ficha6Context _context;        //contexto da base de dados
        public StudentController(ficha6Context context)  //injeção de dependência do contexto da base de dados,  faz com que o asp.net crie uma instância do contexto e passe para o construtor, para que possamos aceder à base de dados
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var allStudents = _context.Students.Include(s => s.Class);  //obtem todos os estudantes da base de dados, incluindo a informação da turma associada
            return View(allStudents);
        }
        public async Task<IActionResult> Index2(string letter)      //ação assíncrona que recebe um parâmetro opcional "letter"
        {
            ViewBag.Letter = letter;  //armazena o valor do parâmetro "letter" na ViewBag para que possa ser acedido na view
            if (!string.IsNullOrEmpty(letter))  //verifica se o parâmetro "letter" não é nulo ou vazio
            {
                return View(await _context.Students.Where(x => x.Name.StartsWith(letter)).Include(s => s.Class).ToListAsync());     //se for fornecida uma letra, filtra os estudantes cujo nome começa com essa letra, inclui a informação da turma e retorna a lista para a view
            }
            else
            {
                return View(await _context.Students.Include(s => s.Class).ToListAsync());       //se não for fornecida nenhuma letra, retorna todos os estudantes com a informação da turma para a view
            }
        }

        public async Task<IActionResult> Index3(string order)
        {
            ViewBag.Order = order;  //armazena o valor do parâmetro "order" na ViewBag para que possa ser acedido na view
            if (string.IsNullOrEmpty(order))
            {
                return View(await _context.Students.Include(s => s.Class).ToListAsync());
            }
            else
            {
                if(order == "ascendente")
                {
                    return View(await _context.Students.Include(s => s.Class).OrderBy(s => s.Name).ToListAsync());
                }
                else
                {
                    return View(await _context.Students.Include(s => s.Class).OrderByDescending(s => s.Name).ToListAsync());
                }
            }
        }
    }
}
