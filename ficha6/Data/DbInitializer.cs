using ficha6.Models;
namespace ficha6.Data
{
    public class DbInitializer
    {
        private readonly ficha6Context _context;

        public DbInitializer(ficha6Context context)
        {
            _context = context;
        }
        
        public void Run()
        {
            _context.Database.EnsureCreated();
            if(_context.Students.Any())
            {
                return;   // DB has been seeded
            }
            var classes = new List<Class>();
            var students = new List<Student>();

            using (StreamReader sr = File.OpenText("Data\\StudentList_ficha6.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    if (!classes.Any(c => c.Name == parts[2])) { 
                        classes.Add(new Class{ Name= parts[2] });
                    }
                    var student = new Student
                    {
                        Number = Int32.Parse(parts[0]),
                        Name = parts[1],
                        Class = classes.First(c => c.Name == parts[2])
                    };

                }
                _context.Classes.AddRange(classes);
                _context.Students.AddRange(students);
                _context.SaveChanges();
            }

        }
    }
}
