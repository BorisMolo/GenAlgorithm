using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;


namespace GenAlgorithm
{
    /*
     * Создаем популяцию random чисел от -1000 до 1000
     * Делаем оценку для этой популяции и убиваем самые плохие
     * Скрещиваем хорошие виды
     * заново все
     * 
     * 
     */

    class points
    {

        public double x;
        public double y;
        public double evaluation;

        public points(double x, double y)
        {
            this.x = x;
            this.y = y;
            evaluation = 0;
        }

        public points()
        {
            this.x = 0;
            this.y = 0;
            evaluation = 0;
        }
    }

    class Genetic
    {
        public Genetic(int Population,int breed,int chose) {
            this.Population = new List<points>();
            population_count = Population;
            generation = 0;
            chose_func = chose;
            breed_count = breed;
        }

        public double Function(double x, double y)
        {
            switch (chose_func)
            {
                case 1: //Himmelblau
                    return Math.Pow((x * x + y - 11), 2) + Math.Pow((x + y * y - 7), 2);    
                    break;
                case 2: //Rosenbrock
                    return Math.Pow((1 - x), 2) + 100 * Math.Pow((y - x * x), 2);
                    break;
                case 3: // Растригина 
                    return 20 + x * x + y * y - 10 * (Math.Cos(2 * 3.14 * x) + Math.Cos(2 * 3.14 * y));
                    break;
                case 4: // по заданию
                    return Math.Pow((x + 9 * y), 2) + Math.Pow((y - 1), 2);
                    break;
                default: return 0;
            }
        }


        const int MAX_RND = 100;
        const int MIN_RND = -100;

        public int chose_func;
        public int generation;
        public int breed_count;            // число поколений


        private int population_count;
        private List<points> Population;
       

        // Основной метод, возвращает минимум функции
        public points Gen()
        {
            points p = new points();

            int breed = 0;
            FirstPopulation();
            while(true)
            {
                
                Console.Write("Поколение: "); Console.Write(breed);
                Console.Write(" Популяция:"); Console.WriteLine(Population.Count);
                Population = CreateFitnesses();
                Population = CrossOver();
                Mutation();
                if (breed >= breed_count) break;
                breed++;
                
            }
            // сортируем лучшие на вершине
            for (int i = 0; i < Population.Count; i++)
            {
                for (int j = 0; j < Population.Count - i - 1; j++)
                {
                    if (Population[j].evaluation > Population[j + 1].evaluation)
                    {
                        points tmp = new points();
                        tmp = Population[j];
                        Population[j] = Population[j + 1];
                        Population[j + 1] = tmp;
                    }
                }
            }

            p.x = Population[0].x;
            p.y = Population[0].y;
            return p;
        }
        
        // Создание первоначальной популяции
        void FirstPopulation() {
            Random ranGen = new Random();

            for (int i = 0; i < population_count; ++i) {
                double x = ranGen.NextDouble() * ranGen.Next(MIN_RND, MAX_RND);
                double y = ranGen.NextDouble() * ranGen.Next(MIN_RND, MAX_RND);

                points p = new points(x, y);
                Population.Add(p);
            }
        }

        // Делаем оценку
        public List<points> CreateFitnesses()
        {
            List<points> NewPopulation = new List<points>();

            //  у каждой особи есть 2 храмомы X и Y, а так же их оценка evalution
            for (int i = 0; i < Population.Count; i++ )
            {
                Population[i].evaluation = Function(Population[i].x, Population[i].y);
            }

            // сортируем лучшие на вершине
            Population = Population.OrderBy(x => x.evaluation).ToList();

            // убиваем 35%  
            for (int i = 0; i < (int)(Population.Count - Population.Count * 35 / 100); ++i)
            {
                NewPopulation.Add(Population[i]);
            }

            return NewPopulation;
        }

        // Скрешиваем особей
        public List<points> CrossOver() {
            List<points> NewPopulation = new List<points>();

            for (int i = 2; i < Population.Count; i = i + 2) {

                if (i > Population.Count) break;
                
                // Добавляем родителей
                NewPopulation.Add(Population[i - 1]);   
                NewPopulation.Add(Population[i - 2]);

                // добавляем предка
                points p = new points((Population[i - 1].x + Population[i - 2].x) / 2, (Population[i - 1].y + Population[i - 2].y) / 2);
                p.evaluation = Function(p.x, p.y);
                NewPopulation.Add(p);
            }
            return NewPopulation;
        }

        // делаем мутацию
        void Mutation() {
            for (int i = 0; i < (int)(Population.Count * 1 / 100); ++i) {
                Random ranGen = new Random();
                int j = ranGen.Next(0,Population.Count - 2);
                Population[j].x += ranGen.NextDouble() * ranGen.Next(MIN_RND, MAX_RND);
                Population[j].y += ranGen.NextDouble() * ranGen.Next(MIN_RND, MAX_RND);
                Population[j].evaluation = Function(Population[j].x, Population[j].y);
            }
        }

    }
}
