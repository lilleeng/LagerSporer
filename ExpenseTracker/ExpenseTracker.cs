using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text.Json;

public class Expense
{
    public string name { get; set; }
    public double amount { get; set; }
    public DateTime paymentDueDate { get; set; }
}

class ExpenseTracker
{
    static void Main()
    {


        // var iso8601String = "20080501T08:30:52Z";
        // DateTime dateISO8602 = DateTime.ParseExact(iso8601String, "yyyyMMddTHH:mm:ssZ",
        //                                 System.Globalization.CultureInfo.InvariantCulture);


        // string paymentDueDateString = "20261010";
        // paymentDueDateString = paymentDueDateString + "T23:59:59Z";

        // Console.WriteLine(paymentDueDateString);

        // DateTime paymentDueDate = DateTime.ParseExact(paymentDueDateString, "yyyyMMddTHH:mm:ssZ",
        //     System.Globalization.CultureInfo.InvariantCulture);

        // return;

        List<Expense> expenses = loadExpenseJSON();

        bool quitting = false;
        while (!quitting)
        {
            printMainMenu();
            string mainMenuOption = Console.ReadLine();   
            switch (mainMenuOption)
            {
                case "1": // Show all expenses
                    printAllExpenses(expenses);
                    break;
                case "2": // Show statistics
                    printStatistics();
                    break;
                case "3": // Add expense
                    printAddExpense(expenses);
                    break;
                case "4": // Remove expense
                    printRemoveExpense(expenses);
                    break;
                case "5": // Quit
                    quitting = true;
                    break;
                default: // Unrecognized input
                    printUnrecognizedInput();
                    break;
            }    
        }

        saveExpenseJSON(expenses);
    }

    static List<Expense> loadExpenseJSON()
    {
        // Look for 'expenses.json'
        // If found, load in
        // If not found, return a empty list/array/collection


        if (File.Exists("expenses.json"))
        {
            using (StreamReader sr = new StreamReader("expenses.json"))
            {
                string json = sr.ReadToEnd();
                return JsonSerializer.Deserialize<List<Expense>>(json);
            }
        }
        else
        {
            Console.WriteLine("'expenses.json' not found. A new one will be created when exiting the program.");
            return new List<Expense> {};
        }
    }
    static void saveExpenseJSON(List<Expense> expenses)
    {
        // If 'expenses.json' already exists, overwrite
        // Else, make new
        // ^These might be the same code

        using (StreamWriter sw = new StreamWriter("expenses.json"))
        {
            sw.WriteLine(JsonSerializer.Serialize(expenses));
        }
        Console.WriteLine("Saved Expense JSON");
    }

    static void printMainMenu()
    {
        Console.WriteLine(
@"
=== EXPENSE TRACKER ===
1. Show expenses
2. Show statistics
3. Add expense
4. Remove expense
5. Quit
Input option (1-5): "
        );
    }
    static void printAllExpenses(List<Expense> expenses)
    {
        double sum = 0;
        Console.WriteLine(
@"
=== ALL EXPENSES ===
Name, Amount, Due date"
        );
        foreach (var expense in expenses)
        {
            sum += expense.amount;
            string expenseString = string.Format("{0}, {1}, {2}", expense.name, expense.amount, expense.paymentDueDate);
            Console.WriteLine(expenseString);
        }
        Console.WriteLine(string.Format("Sum of expenses: {0}", sum));
    }
    static void printStatistics()
    {
        Console.WriteLine(
            @"=== STATISTICS ==="
        );
    }
    static void printAddExpense(List<Expense> expenses)
    {
        string name = "";
        double amount = 0.0;
        DateTime paymentDueDate = new DateTime(2000, 1, 1, 0, 0, 0);
        Console.WriteLine(
@"
=== ADD EXPENSE ===
Enter '-' to interrupt
"
        );
        Console.Write("Name: ");
        name = Console.ReadLine();
        if (name == "-") {return;}

        bool tryFlag = true;
        while (tryFlag)
        {
            try
            {
                Console.Write("Amount: ");
                string amountString = Console.ReadLine();
                if (amountString == "-") {return;}
                amount = Convert.ToDouble(amountString);
                tryFlag = false;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Incorrect format, try again.");
            }
        }

        tryFlag = true;
        while (tryFlag)
        {
            try
            {
                Console.WriteLine("Format: yyyyMMdd");
                Console.Write("Due date: ");
                string paymentDueDateString = Console.ReadLine();
                if (paymentDueDateString == "-") {return;}
                paymentDueDateString = paymentDueDateString + "T23:59:59Z";
                paymentDueDate = DateTime.ParseExact(paymentDueDateString, "yyyyMMddTHH:mm:ssZ",
                    System.Globalization.CultureInfo.InvariantCulture);
                tryFlag = false;
            }
            catch (System.Exception)
            {
                Console.WriteLine("Incorrect format, try again.");
            }
        }

        expenses.Add(new Expense {
            name = name,
            amount = amount,
            paymentDueDate = paymentDueDate
        });
        

    }
    static void printRemoveExpense(List<Expense> expenses)
    {
        Console.WriteLine(
@"
=== REMOVE EXPENSE ===
Index, Name, Amount, Due date"
        );
        for (int i = 0; i < expenses.Count; i++)
        {
            Expense expense = expenses[i];
            string expenseString = string.Format("{0}, {1}, {2}, {3}", i, expense.name, expense.amount, expense.paymentDueDate);
            Console.WriteLine(expenseString);
        }
        Console.Write("Index to remove: ");
        string indexToRemoveString = Console.ReadLine();
        if (indexToRemoveString == "-") {return;}
        int indexToRemove = Convert.ToInt32(indexToRemoveString);
        expenses.RemoveAt(indexToRemove);
    }
    static void printUnrecognizedInput()
    {
        Console.WriteLine(
            @"Unrecognized input, try again."
        );
    }
}

