using Ficha2.Models;
using Ficha2.Data;

namespace Ficha2
{
    public class TempClean
    {
        public static void Run(Ficha2Context context)
        {
            // Apaga todos os registros da tabela Category
            context.Category.RemoveRange(context.Category);
            context.SaveChanges();
        }
    }
}
