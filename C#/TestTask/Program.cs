using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestTask.Abstract;
using TestTask.Domain;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<BaseEmployee> employees = new List<BaseEmployee>();
            employees.Add(new FixWageEmployee
            {
                Id = 1,
                Name = "Olexandr",
                FixWage = 2000
            });
            employees.Add(new HourWageEmployee
            {
                Id = 2,
                Name = "Ivan",
                HourWage = 15
            });
            employees.Add(new FixWageEmployee
            {
                Id = 3,
                Name = "Oleg",
                FixWage = 2500
            });
            employees.Add(new HourWageEmployee
            {
                Id = 4,
                Name = "Taras",
                HourWage = 24
            });
            employees.Add(new HourWageEmployee
            {
                Id = 5,
                Name = "Sergiy",
                HourWage = 18
            });
            employees.Add(new FixWageEmployee
            {
                Id = 6,
                Name = "Mykola",
                FixWage = 3000
            });
            employees.Add(new HourWageEmployee
            {
                Id = 7,
                Name = "Andrij",
                HourWage = 13
            });
            //Пункт а)
            employees = employees.OrderByDescending(e => e.GetAverageMonthWage()).ToList();
            PrintEmployees(employees);
            PrintDelimiter();
            //Пункт b)
            if (employees.Count >= 5)
            {
                for (int i = 0; i < employees.Count; ++i)
                {
                    Console.WriteLine(employees[i].ToString());
                    if (i == 4)
                    {
                        break;
                    }
                }
            }
            else
            {
                PrintEmployees(employees);
            }
            PrintDelimiter();
            //Пункт c)
            if (employees.Count >= 3)
            {
                for (int i = employees.Count - 3; i < employees.Count; ++i)
                {
                    Console.WriteLine(employees[i].ToString());
                }
            }
            else
            {
                PrintEmployees(employees);
            }
            PrintDelimiter();
            //Пункт d)
            try
            {
                string fileName = "Employees.emp";
                WriteToFile(employees, fileName);
                employees.Clear();
                employees = ReadFromFile(fileName);
                PrintEmployees(employees);
                PrintDelimiter();
                //Пункт e)
                employees = ReadFromFile("InvalidEmployees.emp");
                PrintEmployees(employees);
                PrintDelimiter();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
        static void PrintEmployees(IList<BaseEmployee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }
        static void PrintDelimiter()
        {
            Console.WriteLine("------------------------------------------------------------------------------");
        }

        static void WriteToFile(IList<BaseEmployee> employees, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine($"Count:{employees.Count}");
                Console.WriteLine($"Writitng {employees.Count} objects to {fileName}...");
                foreach (var employee in employees)
                {
                    if (employee.GetType() == typeof(FixWageEmployee))
                    {
                        writer.WriteLine($"Id:{employee.Id};Name:{employee.Name};FixWage:{((FixWageEmployee)employee).FixWage}");
                    }
                    else if(employee.GetType() == typeof(HourWageEmployee))
                    {
                        writer.WriteLine($"Id:{employee.Id};Name:{employee.Name};HourWage:{((HourWageEmployee)employee).HourWage}");
                    }
                }
                Console.WriteLine($"Done");
                PrintDelimiter();
            }
        }
        static IList<BaseEmployee> ReadFromFile(string fileName)
        {
            IList<BaseEmployee> employees = new List<BaseEmployee>();
            if(File.Exists(fileName))
            {
                var strEmployees = File.ReadAllLines(fileName);
                var countData = strEmployees[0].Split(':');
                if (countData[0] != "Count" || countData.Length != 2)
                {
                    throw new Exception("Invalid file format: line 1: Missing parameter 'Count'");
                }
                else
                {
                    int size = 0;
                    if(!int.TryParse(countData[1], out size))
                    {
                        throw new Exception("Invalid file format: line 1: Invalid 'Count' value");
                    }
                    if(strEmployees.Length != size + 1)
                    {
                        throw new Exception("Invalid file format: line 1: Invalid 'Count' value");
                    }
                    Console.WriteLine($"Reading file {fileName} with {size} employee(s)");
                    for(int i = 1; i <= size; ++i)
                    {
                        var employeeFields = strEmployees[i].Split(';');
                        if(employeeFields.Length != 3)
                        {
                            throw new Exception($"Invalid file format: line {i+1}: Invalid employee line");
                        }
                        //Id
                        var idfields = employeeFields[0].Split(':');
                        if(idfields.Length != 2 || idfields[0] != "Id")
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Missing 'id' parameter");
                        }
                        int id = 0;
                        if(!int.TryParse(idfields[1], out id))
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Invalid 'id' parameter");
                        }
                        //Name
                        var nameFields = employeeFields[1].Split(':');
                        if (nameFields.Length != 2 || nameFields[0] != "Name")
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Missing 'Name' parameter");
                        }
                        if (nameFields[1].Length == 0)
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Empty 'Name' parameter");
                        }
                        string name = nameFields[1];
                        //Wage and Type
                        var typeFields = employeeFields[2].Split(':');
                        if (typeFields.Length != 2)
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Missing 'Wage' parameter");
                        }
                        int wage = 0;
                        if(!int.TryParse(typeFields[1], out wage))
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Invalid {typeFields[0]} parameter");
                        }
                        if(typeFields[0] == "HourWage")
                        {
                            employees.Add(new HourWageEmployee
                            {
                                Id = id,
                                Name = name,
                                HourWage = wage
                            });
                        }
                        else if(typeFields[0] == "FixWage")
                        {
                            employees.Add(new FixWageEmployee
                            {
                                Id = id,
                                Name = name,
                                FixWage = wage
                            });
                        }
                        else
                        {
                            throw new Exception($"Invalid file format: line {i + 1}: Unknown 'Wage' parameter");
                        }
                    }
                    Console.WriteLine("Done");
                    PrintDelimiter();
                    return employees;
                }
            }
            else
            {
                throw new Exception($"Unable to find file {AppContext.BaseDirectory + fileName}");
            }
        }
    }
}
