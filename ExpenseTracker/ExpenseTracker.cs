using System;
class ExpenseTracker
{
    static void Main()
    {
        loadExpenseJSON();

        bool quitting = false;
        while (!quitting)
        {
            printMainMenu();
            int blabla = takeInput();   
            switch (blabla)
            {
                case 1:
                    // Show all expenses
                case 2:
                    // Show statistics
                case 3:
                    // Add expense
                case 4:
                    // Remove expense
                case 5:
                    // Quit
                default:
                    // Unrecognized input
            }    
        }

        printQuittingScreen();
        saveExpenseJSON();
    }

    
}