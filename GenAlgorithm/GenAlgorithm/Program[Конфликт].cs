using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgorithm
{
    
    
    
    class Program
    {
        static void Main(string[] args)
        {

            int population,breed;
            int func;
            Genetic Gens;

            Console.Write("Введите популяцию: "); population = Convert.ToUInt16(Console.ReadLine());
            Console.Write("Введите Максимальное число поколений: "); breed = Convert.ToInt16(Console.ReadLine());
            Console.Write("1 - Himmelblau's function;\n2 - Rosenbrock function\n3 - Растригин \n4 - another "); func = Convert.ToInt16(Console.ReadLine());
            
            Gens = new Genetic(population, breed, func);

            points P = new points();
            
            P = Gens.Gen();
            P.evaluation = Gens.Function(P.x, P.y);

            Console.Write("X: "); Console.WriteLine(P.x);
            Console.Write("Y: "); Console.WriteLine(P.y);
            Console.Write("F: "); Console.WriteLine(P.evaluation);

            Console.ReadLine();
        }
    }
}
