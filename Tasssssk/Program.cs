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
                // ����� �����
                var tasks = db.Tasks.Include(u => u.Projects).ToList();
                foreach (Tasks task in tasks)
                    Console.WriteLine($"{task.TaskName} - {task.Projects?.TaskName}");

                // ����� ��������
                var projects = db.Projects.Include(c => c.Tasks).ToList();
                foreach (Projects project in projects)
                {
                    Console.WriteLine($"\n �������: {project.ProjectName}");
                    foreach (Tasks task in project.Tasks)
                    {
                        Console.WriteLine($"{task.TaskName}");
                    }
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                // ������������ ���� ������
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // �������� � ���������� �������
                Projects project1 = new Projects { ProjectName = "������� 20-� �����" };
                Projects project2 = new Projects { ProjectName = "�����������" };
                db.Projects.AddRange(project1, project2);

                Tasks task1 = new Tasks { TaskName = "�������� ������", Projects = project2 };
                Tasks task2 = new Tasks { TaskName = "��������� ������� ������", Projects = project1 };
                Tasks task3 = new Tasks { TaskName = "��������� 1917-� �����", Projects = project1 };
                db.Tasks.AddRange(task1, task2, task3);
                db.SaveChanges();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                // ��������� ����� ������
                Tasks task1 = db.Tasks.FirstOrDefault(p => p.TaskName == "�������� ������");
                if (task1 != null)
                {
                    task1.TaskName = "�������� ����";
                    db.SaveChanges();
                }
                // ��������� �������� �������
                Projects project = db.Projects.FirstOrDefault(p => p.ProjectName == "�����������");
                if (project != null)
                {
                    project.ProjectName = "����������";
                    db.SaveChanges();
                }

                // ����� ������� �������
                Tasks task2 = db.Tasks.FirstOrDefault(p => p.TaskName == "�������� ����");
                if (task2 != null && project != null)
                {
                    task2.Projects = project;
                    db.SaveChanges();
                }
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                Tasks task1 = db.Tasks.FirstOrDefault(p => p.TaskName == "�������� ����");
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
