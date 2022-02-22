using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Tasssssk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            using (ApplicationContext db = new ApplicationContext())
            {
                // вывод задач
                var tasks = db.Tasks.Include(u => u.Projects).ToList();
                foreach (Tasks task in tasks)
                    Console.WriteLine($"{task.TaskName} - {task.Projects?.TaskName}");

                // вывод проектов
                var projects = db.Projects.Include(c => c.Tasks).ToList();
                foreach (Projects project in projects)
                {
                    Console.WriteLine($"\n Проекты: {project.ProjectName}");
                    foreach (Tasks task in project.Tasks)
                    {
                        Console.WriteLine($"{task.TaskName}");
                    }
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                // пересоздадим базу данных
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // создание и добавление моделей
                Projects project1 = new Projects { ProjectName = "История 20-х веков" };
                Projects project2 = new Projects { ProjectName = "Митохондрии" };
                db.Projects.AddRange(project1, project2);

                Tasks task1 = new Tasks { TaskName = "Изучение клетки", Projects = project2 };
                Tasks task2 = new Tasks { TaskName = "Свержение царской власти", Projects = project1 };
                Tasks task3 = new Tasks { TaskName = "Революция 1917-х годов", Projects = project1 };
                db.Tasks.AddRange(task1, task2, task3);
                db.SaveChanges();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                // изменение имени задачи
                Tasks task1 = db.Tasks.FirstOrDefault(p => p.TaskName == "Изучение клетки");
                if (task1 != null)
                {
                    task1.TaskName = "Изучение тела";
                    db.SaveChanges();
                }
                // изменение названия проекта
                Projects project = db.Projects.FirstOrDefault(p => p.ProjectName == "Митохондрии");
                if (project != null)
                {
                    project.ProjectName = "Цитоплазма";
                    db.SaveChanges();
                }

                // смена задачей проекта
                Tasks task2 = db.Tasks.FirstOrDefault(p => p.TaskName == "Изучение тела");
                if (task2 != null && project != null)
                {
                    task2.Projects = project;
                    db.SaveChanges();
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                Tasks task1 = db.Tasks.FirstOrDefault(p => p.TaskName == "Изучение тела");
                if (task1 != null)
                {
                    db.Tasks.Remove(task1);
                    db.SaveChanges();
                }

                Projects project = db.Projects.FirstOrDefault();
                if (project != null)
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
