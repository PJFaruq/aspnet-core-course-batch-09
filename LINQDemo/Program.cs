namespace LINQDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = Student.GetStudentList();
            #region Filtering operator



            ////Give me only the students whose age is above 18

            ////Method syntax
            //var adults = students.Where(x => x.Age > 18 && x.Department == "CSE");


            ////SQL - Like
            //var adults = from s in students
            //             where s.Age > 18 && s.Department == "CSE"
            //             select s;

            //foreach (var student in adults)
            //{
            //    Console.WriteLine($"Name: {student.Name} - Age: {student.Age} - Departement: {student.Department}");
            //}


            ////OfType
            //ArrayList mixedList = new ArrayList { 1, "Hello", "World", 4 };
            //var onlyIntegers = mixedList.OfType<string>().ToList();

            //foreach (var i in onlyIntegers)
            //{
            //    Console.WriteLine(i);
            //}


            #endregion

            #region Projection operator
            //var studentNames = students.Select(x => x.Department);

            //foreach (var name in studentNames)
            //{
            //    Console.WriteLine(name);
            //}

            ////Select Opertor
            //var studentInfo = students.Where(x => x.Department == "CSE").Select(x => new { stdName = x.Name, stdAge = x.Age, stdDept = x.Department });

            //foreach (var s in studentInfo)
            //{
            //    Console.WriteLine($"{s.stdName} -{s.stdAge} -{s.stdDept}");
            //}

            //var studentList = new List<Student>
            //                {
            //                    new Student { Name ="Rahim", Subjects =new List<string>{"Math","Physics" }},
            //                    new Student { Name ="Karim", Subjects =new List<string>{"English" }},
            //                    new Student { Name ="Ayesha", Subjects =new List<string>{"Math","Biology" }}
            //                };
            //var allSubjects = studentList.SelectMany(x => x.Subjects);

            //foreach (var subject in allSubjects)
            //{
            //    Console.WriteLine(subject);
            //}

            #endregion

            #region Sorting Operator
            ////OrderBy
            //var sortedByAge = students.OrderBy(x => x.Age);

            //foreach (var student in sortedByAge)
            //{
            //    Console.WriteLine($"Name: {student.Name} - Age:{student.Age}");
            //}

            ////OrderByDescending

            //var sortedByAge = students.OrderByDescending(x => x.Age);

            //foreach (var student in sortedByAge)
            //{
            //    Console.WriteLine($"Name: {student.Name} - Age:{student.Age}");
            //}

            ////ThenBy
            //var sortedStudent = students.OrderBy(x => x.Department).ThenBy(x => x.Name);

            //foreach (var st in sortedStudent)
            //{
            //    Console.WriteLine($"Name: {st.Name} - Department {st.Department}");
            //}

            //Console.WriteLine("\n");

            ////ThenByDescending
            //var sortedStudents = students.OrderBy(x => x.Department).ThenByDescending(x => x.Name);

            //foreach (var st in sortedStudents)
            //{
            //    Console.WriteLine($"Name: {st.Name} - Department {st.Department}");
            //}

            #endregion

            #region Aggregation and Quantifiers

            ////Count
            //int totalStudents = students.Count(x => x.Age > 18);
            //Console.WriteLine(totalStudents);

            ////Sum operator
            //double totalMarks = students.Sum(x => x.Marks);
            //Console.WriteLine(totalMarks);

            ////Average operator
            //double avgMarks = students.Average(x => x.Marks);
            //Console.WriteLine(avgMarks);

            ////Min
            //double minMarks = students.Min(x => x.Marks);
            //Console.WriteLine(minMarks);

            ////Max
            //double maxMarks = students.Max(x => x.Marks);
            //Console.WriteLine(maxMarks);

            ////Any Operator
            //bool hasFailedStudent = students.Any(x => x.Marks < 40);
            //Console.WriteLine(hasFailedStudent);

            ////All Operator
            //bool allPassed = students.All(x => x.Marks >= 40);
            //Console.WriteLine(allPassed);

            #endregion

            #region Grouping
            ////GroupBy
            //var groupByDept = students.GroupBy(x => x.Department);   //list of department group,

            //foreach (var group in groupByDept)
            //{
            //    Console.WriteLine($"Department: {group.Key}");
            //    foreach (var student in group)
            //    {
            //        Console.WriteLine($"{student.Name} - {student.Marks} - {student.Age}");
            //    }
            //    Console.WriteLine("\n");
            //}


            #endregion

            #region Partitioning

            ////Take operator
            //var firstThree = students.Take(3).OrderBy(x => x.Marks);
            //foreach (var s in firstThree)
            //{
            //    Console.WriteLine($"{s.Name} - {s.Marks}");
            //}

            ////Skip
            //var firstThree = students.Skip(3);
            //foreach (var s in firstThree)
            //{
            //    Console.WriteLine($"{s.Name} - {s.Marks}");
            //}

            ////TakeWhile operator
            //var takeWhile = students.OrderByDescending(x => x.Marks).TakeWhile(x => x.Marks >= 70);
            //foreach (var s in takeWhile)
            //{
            //    Console.WriteLine($"{s.Name} - {s.Marks}");
            //}

            ////SkipWHile operator
            //var skipWhile = students.SkipWhile(x => x.Marks >= 70);
            //foreach (var s in skipWhile)
            //{
            //    Console.WriteLine($"{s.Name} - {s.Marks}");
            //}

            #endregion

            #region Element / Retrieval
            ////First operator
            //var firstStudent = students.First(x => x.Department == "Textile");
            //Console.WriteLine(firstStudent.Name);

            ////FirstOrDefault operator
            //var numbers = new List<int> { 1, 2, 3, 4 };
            //var number = numbers.FirstOrDefault(x => x == 5);
            //Console.WriteLine(number);

            ////Last operator
            //var firstStudent = students.Last();
            //Console.WriteLine(firstStudent.Name);

            ////Last operator
            //var firstStudent = students.LastOrDefault(x => x.Department == "Textile");
            //Console.WriteLine(firstStudent?.Name);

            ////Single operator
            //var student = students.Single(x => x.Id == 3);
            //Console.WriteLine(student.Name);

            ////SingleOrDefault operator
            //var student = students.SingleOrDefault(x => x.Id == 10);
            //Console.WriteLine(student?.Name);

            ////ElementAt
            //var student = students.ElementAt(2);
            //Console.WriteLine(student.Name);

            #endregion

            #region Set Operations
            ////Distinct
            //var departments = students.Select(x => x.Department).Distinct();
            //foreach (var dept in departments)
            //{
            //    Console.WriteLine(dept);
            //}

            ////Union
            //var batchA = new List<string> { "Rahim", "Karim", "Fahim" };
            //var batchB = new List<string> { "Karim", "Hasan", "Fahim" };

            //var allStudents = batchA.Union(batchB);

            //foreach (var name in allStudents)
            //{
            //    Console.WriteLine(name);
            //}

            ////Intersect
            //var batchA = new List<string> { "Rahim", "Karim", "Fahim" };
            //var batchB = new List<string> { "Karim", "Hasan", "Fahim" };

            //var allStudents = batchA.Intersect(batchB);

            //foreach (var name in allStudents)
            //{
            //    Console.WriteLine(name);
            //}

            //// Except
            //var batchA = new List<string> { "Rahim", "Karim", "Fahim" };
            //var batchB = new List<string> { "Karim", "Hasan", "Fahim" };

            //var onlyInA = batchB.Except(batchA);
            //foreach (var name in onlyInA)
            //{
            //    Console.WriteLine(name);
            //}



            #endregion

            #region Conversion
            //var adults = students.Where(x => x.Age > 18 && x.Department == "CSE").ToDictionary(x => x.Id, x => x.Name);
            //foreach (var item in adults)
            //{
            //    Console.WriteLine($"{item.Key} -{item.Value}");
            //}


            #endregion

            #region Others
            //Concat
            //var batchA = new List<string> { "Rahim", "Karim" };
            //var batchB = new List<string> { "Tuhin", "Tushar", "Rahim" };



            //var allStudents = batchA.Concat(batchB);

            //foreach (var name in allStudents)
            //{
            //    Console.WriteLine(name);
            //}
            //var student1 = Student.GetStudentList();
            //var student2 = Student.GetStudentList();

            //var allStudents = student1.Concat(student2);

            //foreach (var student in allStudents)
            //{
            //    Console.WriteLine($"Name: {student.Name} - Age: {student.Age} - Departement: {student.Department}");
            //}


            ////Contains
            //var numberList = new List<int> { 1, 2, 6, 7, 8 };
            //bool hasEight = numberList.Contains(9);
            //Console.WriteLine(hasEight);


            //DefaultIfEmpty
            var emptyList = new List<string>();
            var result = emptyList.DefaultIfEmpty("This is an empty list");

            foreach (var r in result)
            {
                Console.WriteLine(r);
            }

            #endregion

        }
    }
}
