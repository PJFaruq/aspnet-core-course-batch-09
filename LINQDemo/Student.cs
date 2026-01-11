namespace LINQDemo
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public double Marks { get; set; }
        public List<string> Subjects { get; set; }

        public static List<Student> GetStudentList()
        {
            return new List<Student>
            {
                new Student { Id =1, Name ="Rahim", Age =20, Department ="CSE", Marks =85 },
                new Student { Id =2, Name ="Karim", Age =17, Department ="EEE", Marks =70 },
                new Student { Id =3, Name ="Tuhin", Age =22, Department ="CSE", Marks =90 },
                new Student { Id =4, Name ="Tushar", Age =19, Department ="BBA", Marks =65},
                new Student { Id =5, Name ="Shakib", Age =16, Department ="BBA", Marks =60},
                new Student { Id =6, Name ="Hasan", Age =26, Department ="CSE", Marks =55 },
                new Student { Id =7, Name ="Nasir", Age =18, Department ="EEE", Marks =100 },

            };
        }
    }
}
