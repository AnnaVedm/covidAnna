using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covidAnna
{
    public static class EllipseMethods
    {
        public static List<Ellipse> _personEllipses = new List<Ellipse>();
        public static List<Ellipse> _infectionEllipses = new List<Ellipse>();

        public static int ellipseSize = 4;

        private static CovidModel _covidModel = new CovidModel();
        //этот класс будет содержать методы для работы с эллипсами

        //1. Тут будет список эллипсов каждого человека
        //2. Также будет список радиусов эллипсом для каждого человека
        //3. Методы добавления радиsусов инфицирования
        //4. Методы перемещения радиусов в зависимости от их скорости

        public static void CreateEllipsesForPeople(List<Person> people) //для каждого человека нужно создать список эллипсов и разместить их на поле
        {
            foreach (var person in people)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = ellipseSize,
                    Height = ellipseSize,
                    Tag = person.Id,
                    Fill = Brushes.LightBlue
                };

                Canvas.SetLeft(ellipse, person.Coordinates.X); // Смещение на половину ширины эллипса
                Canvas.SetTop(ellipse, person.Coordinates.Y); // Смещение на половину высоты эллипса

                _personEllipses.Add(ellipse);
            }
        }

        public static void UpdateCanvasPosition(List<Person> people)
        {
            // Перерисовка холста основной зоны
            for (int i = 0; i < people.Count; i++)
            {
                var person = people[i];
                var personEllipse = _personEllipses[i];

                Canvas.SetLeft(personEllipse, person.Coordinates.X);
                Canvas.SetTop(personEllipse, person.Coordinates.Y);
            }
        }

        public static void CreateInfectionRadiusEllipse(Person person, double infectRadius) //создаем эллипс для конкретного человека
        {
            Ellipse infectionRadiusEllipse = new Ellipse
            {
                Width = infectRadius * 2,
                Height = infectRadius * 2,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                Fill = Brushes.Transparent,
                Tag = person.Id
            };

            Canvas.SetLeft(infectionRadiusEllipse, person.Coordinates.X - infectRadius);
            Canvas.SetTop(infectionRadiusEllipse, person.Coordinates.Y - infectRadius);

            //InfectionField.Children.Add(infectionRadiusEllipse);
            _infectionEllipses.Add(infectionRadiusEllipse);
        }

        public static void UpdateInfectionRadiusEllipse(Person person, double infectRadius) //обновляем положение радиуса заражения для человека
        {
            var infectionRadiusEllipse = _infectionEllipses.FirstOrDefault(e => (int)e.Tag == person.Id);
            if (infectionRadiusEllipse != null)
            {
                Canvas.SetLeft(infectionRadiusEllipse, person.Coordinates.X - infectRadius);
                Canvas.SetTop(infectionRadiusEllipse, person.Coordinates.Y - infectRadius);
            }
        }
    }
}
