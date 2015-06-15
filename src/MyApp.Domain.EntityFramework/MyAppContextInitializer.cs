using System.Data.Entity;

namespace MyApp.Domain.EntityFramework
{
    public class MyAppContextInitializer : DropCreateDatabaseIfModelChanges<MyAppContext>
    {
        protected override void Seed(MyAppContext context)
        {
            //var tasks = new List<Task>
            //{
            //};

            //context.TaskSet.AddRange(tasks);
            //context.SaveChanges();
        }
    }
}