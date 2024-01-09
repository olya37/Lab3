using Calc_LAB2_Dolganova;
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        //объявление необходимых переменных и экземпляров класса, а также констант
        Calculator calculator = new Calculator();
        calculator.ShowUsage();
        Saver saver = new Saver();
        string userFileChoice;
        Loader loader = new Loader();  
        Steps steps = new Steps([]);
        string pattern = @"^#\d+$";
        string[] types = new string[] { "xml", "json", "sql" };

        double operand = calculator.EnterOperand();
        if (steps.Results.Count == 0)
        {
            steps.Results.Add(operand);
            Console.WriteLine("[#" + steps.Results.Count() + "]=" + operand);
        }

        while (true)
        {
            string useraction = calculator.EnterAction();
            if (Regex.IsMatch(useraction, pattern))
            {
                string action = useraction.Substring(1);
                int stepNumber = Convert.ToInt32(action);
                double result = steps.Results[stepNumber - 1];
                steps.Results.Add(result);
                Console.WriteLine("[#" + steps.Results.Count() + "]=" + result);
            }
            else
            {
                int previousResultId;
                double previousResult;
                double result;
                switch (useraction)
                {
                    case "+":
                        previousResultId = steps.Results.Count() - 1;
                        previousResult = steps.Results[previousResultId];
                        operand = calculator.EnterOperand();              
                        result = calculator.PlusAction(previousResult, operand);
                        steps.Results.Add(result);
                        Console.WriteLine("[#" + steps.Results.Count() + "]=" + result);
                        break;
                    case "-":
                        previousResultId = steps.Results.Count() - 1;
                        previousResult = steps.Results[previousResultId];
                        operand = calculator.EnterOperand();
                        result = calculator.MinusAction(previousResult, operand);
                        steps.Results.Add(result);
                        Console.WriteLine("[#" + steps.Results.Count() + "]=" + result);
                        break;
                    case "*":
                        previousResultId = steps.Results.Count() - 1;
                        previousResult = steps.Results[previousResultId];
                        operand = calculator.EnterOperand();
                        result = calculator.MultiplyAction(previousResult, operand);
                        steps.Results.Add(result);
                        Console.WriteLine("[#" + steps.Results.Count() + "]=" + result);
                        break;
                    case "/":
                        previousResultId = steps.Results.Count() - 1;
                        previousResult = steps.Results[previousResultId];
                        operand = calculator.EnterOperand();
                        result = calculator.DivisionAction(previousResult, operand);
                        steps.Results.Add(result);
                        Console.WriteLine("[#" + steps.Results.Count() + "]=" + result);
                        break;
                    case "q":
                        calculator.ExitAction();
                        break;
                    case "s":
                        Console.WriteLine("Выберите тип файла, в который сохранятся ваши результаты: 'xml' для xml-файла, 'json' для json-файла, sql - для сохранения в базу данных");
                        userFileChoice = Console.ReadLine();

                        while (!(types.Contains(userFileChoice)))
                        {
                            Console.WriteLine("Некорректное значение. Введите один из доступных форматов.");
                            userFileChoice = Console.ReadLine();
                        }

                        switch (userFileChoice)
                        {
                            case "xml":
                                saver.SaveListToXml(steps);
                                break;
                            case "json":
                                saver.SaveListToJson(steps.Results,  "userFile.json");
                                break;
                            case "sql":
                                saver.SaveListToSql(steps);
                                break;


                        }
                        break;
                    case "l":
                        Console.WriteLine("Выберите тип файла, из которого загрузятся ваши результаты: 'xml' для xml-файла, 'json' для json-файла, sql - для сохранения в базу данных");
                        userFileChoice = Console.ReadLine();

                        while (!(types.Contains(userFileChoice)))
                        {
                            Console.WriteLine("Некорректное значение. Введите один из доступных форматов.");
                            userFileChoice = Console.ReadLine();
                        }

                        switch (userFileChoice)
                        {
                            case "xml":
                                steps.Results = loader.LoadListFromXml("userFile.xml");
                                foreach (var item in steps.Results)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case "json":
                                steps.Results = loader.LoadListFromJson("userFile.json");
                                foreach (var item in steps.Results)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case "sql":
                                steps.Results = loader.LoadListFromSql();
                                foreach (var item in steps.Results)
                                {
                                    Console.WriteLine(item);
                                }
                                break;

                        }
                        break;

                }
            }


        }


        
    }
    





}





