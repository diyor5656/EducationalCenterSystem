using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Yangi.Services
{
    public partial class Services
    {
        public static string GetTeacherPAth()
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath += @"\teachers.json";
            return currentPath;
        }
        public void ClearTFile()
        {
            File.WriteAllText(GetTeacherPAth(), string.Empty);
            Console.WriteLine("Malumotlar tozalandi");
        }
        public void SaveTeachers(List<Teachers> teachers)
        {
            string serialized = JsonSerializer.Serialize(teachers);

            File.WriteAllText(GetTeacherPAth(), serialized);
        }
        public void AddTeacher()
        {

            List<Teachers> teachers = GetTeachers();

            Console.Write("Mentor Ismini kiriting: ");
            string teacherName = Console.ReadLine();
            Console.Write("Mentorning Mutaxassisligini kiriting: ");
            string teacherSpc = Console.ReadLine();
            Console.Write("Mentor yoshini kiriting: ");
            int teacherAge = int.Parse(Console.ReadLine());

            int newId = teachers.Count > 0 ? teachers.Max(t => t.Id) + 1 : 1;

            Teachers newTeacher = new Teachers
            {
                Id = newId,
                Name = teacherName,
                Spc = teacherSpc,
                Age = teacherAge
            };

            teachers.Add(newTeacher);
            SaveTeachers(teachers);

            Console.WriteLine("Mentor Muvoffaqiyatli qushildi!");
        }
        public List<Teachers> GetTeachers()
        {
            if (!File.Exists(GetTeacherPAth()))
            {
                Console.WriteLine("Hozircha Mentorlar yo'q");
            }
            if (!File.Exists(GetTeacherPAth()))
            {
                return new List<Teachers>();
            }

            string jsonFromFile = File.ReadAllText(GetTeacherPAth());
            var teachers = string.IsNullOrEmpty(jsonFromFile) ? new List<Teachers>() : JsonSerializer.Deserialize<List<Teachers>>(jsonFromFile);

            Console.WriteLine("Mentorlar Ro'yxati:");
            foreach (var teacher in teachers)
            {
                Console.WriteLine($"Id: {teacher.Id}, Ismi: {teacher.Name}, Mutaxassisligi: {teacher.Spc}, Yoshi: {teacher.Age}");
            }

            return teachers;
        }
        public void DeleteTeacher()
        {
            List<Teachers> teachers = GetTeachers();

            Console.Write("O'chirish uchun Mentor ismini kiriting: ");
            int teacherId;
            while (!int.TryParse(Console.ReadLine(), out teacherId))
            {
                Console.Write("Iltimos Id ni kiriting : ");
            }

            Teachers teacherToDelete = teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacherToDelete == null)
            {
                Console.WriteLine("Mentor topilmadi");
                return;
            }

            teachers.Remove(teacherToDelete);
            SaveTeachers(teachers);

            Console.WriteLine("Mentor o'chirib tashlandi.");
        }
        public void UpdateTeacher()
        {
            List<Teachers> teachers = GetTeachers();

            Console.Write("Yangilash ucun Mentor Id sini kiriting : ");
            int teacherId;
            while (!int.TryParse(Console.ReadLine(), out teacherId))
            {
                Console.Write("Iltimos Id kiriting: ");
            }

            Teachers teacherToUpdate = teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacherToUpdate == null)
            {
                Console.WriteLine("O'qituvchi kiriting.");
                return;
            }

            Console.Write("Yangi Ism kiriting : ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                teacherToUpdate.Name = newName;
            }
            Console.Write("Mutaxassisligini kiriting : ");
            string newspc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newspc))
            {
                teacherToUpdate.Spc = newspc;
            }
            Console.Write("Yoshini kiriting: ");
            int newag = int.Parse(Console.ReadLine());

            teacherToUpdate.Age = newag;


            SaveTeachers(teachers);
            Console.WriteLine("Mentor muvaffaqiyatli saqlandi! ");
        }
    }
}
