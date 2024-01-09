using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Calc_LAB2_Dolganova
{
    public class Calculator
    {

        public void ShowUsage() //Метод для вывода приветственного сообщения и правил пользования
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("when a first symbol on line is ‘>’ – enter operand (number)");
            Console.WriteLine("when a first symbol on line is ‘@’ – enter operation");
            Console.WriteLine("operation is one of ‘+’, ‘-‘, ‘/’, ‘*’, ‘#’ followed with number of evaluation step, ‘q’ to exit, ‘s’ to save file and ‘l’ to load file");
        }


        public double EnterOperand()
        {
            Console.Write(">");

            double operand;

            bool isNumber = double.TryParse(Console.ReadLine(), out operand);

            // если это не удалось, то выводим сообщение об ошибке и повторяем запрос
            while (!isNumber)
            {
                Console.WriteLine("Некорректное значение. Введите число.");
                Console.Write(">");
                isNumber = double.TryParse(Console.ReadLine(), out operand);
            }

            return operand;
        }

        public string EnterAction()
        {

            string[] actions = ["+", "-", "*", "/", "q","s","l"];
            string pattern = @"^#\d+$";
            Console.Write("@");
            string userAction = Console.ReadLine();
            
            while (!(actions.Contains(userAction) | (Regex.IsMatch(userAction, pattern))))
            {
                Console.WriteLine("Некорректное значение. одно из достпуных действий");
                Console.Write("@");
                userAction = Console.ReadLine();
            }

            return userAction;
        }

        public double PlusAction(double previousResult, double operand)
        {
            double result = previousResult + operand;
            return result;
        }

        public double MinusAction(double previousResult, double operand)
        {
            double result = previousResult - operand;
            return result;
        }

        public double MultiplyAction(double previousResult, double operand)
        {
            double result = previousResult * operand;
            return result;
        }

        public double DivisionAction(double previousResult, double operand)
        {
            double result = previousResult / operand;
            return result;
        }

        public void ExitAction()
        {
            Console.WriteLine("Выход...");
            Environment.Exit(0);
        }
   
    }
}
