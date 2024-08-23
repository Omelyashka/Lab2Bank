using System;
using System.Windows;
using System.IO;



namespace Lab_2
{
    class BankAccount
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public BankAccount(string accountNumber, double initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }
        public void Withdraw(double amount)
        {

            var file = @"D:\Lab2Bank\" + AccountNumber + "_balance.txt"; 
            var fileH = @"D:\Lab2Bank\" + AccountNumber + "_history.txt";
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;

                using (StreamWriter sw = new StreamWriter(file)) { sw.WriteLine(Balance); }
                using (StreamWriter sw = new StreamWriter(fileH, append: true)) { sw.WriteLine(DateTime.Now + "  було знято " + amount + " гривень. " + "Залишок " + Balance); }
            }
            else if (amount <= 0)
            {
                MessageBox.Show("Сума зняття має бути більше нуля.");
            }
            else
            {
                MessageBox.Show("Недостатньо коштів на рахунку.");
            }

        }
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                var file = @"D:\Lab2Bank\" + AccountNumber + "_balance.txt";
                var fileH = @"D:\Lab2Bank\" + AccountNumber + "_history.txt";
                Balance += amount;

                using (StreamWriter sw = new StreamWriter(file)) { sw.WriteLine(Balance); }
                using (StreamWriter sw = new StreamWriter(fileH, append: true))
                {
                    sw.WriteLine(DateTime.Now + "  було поповнено на " + amount + "  гривень. " + "Залишок  " + Balance);
                }
            }
            else
            {
                MessageBox.Show("Сума поповнення має бути більше нуля.");
            }
        }
        public static void NewAccount(string number)
        {
            using (FileStream fs = File.Create(@"D:\Lab2Bank\" + number + "_history.txt")) { }
            using (FileStream fs = File.Create(@"D:\Lab2Bank\" + number + "_balance.txt")) { }
            using (StreamWriter sw = new StreamWriter(@"D:\Lab2Bank\" + number + "_balance.txt")) { sw.WriteLine(0); }
        }


    }

    public partial class MainWindow : Window
    {
        string NameOfAccount { get; set; }
        private void ShowAll(string file)
        {
            using (StreamReader rd = new StreamReader(file))
            {
                string[] lines = File.ReadAllLines(file);

                ListBox1.ItemsSource = lines;
            }


        }
        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            var file = @"D:\Lab2Bank\" + NameOfAccount + "_history.txt";
            var file1 = @"D:\Lab2Bank\" + NameOfAccount + "_balance.txt";
            double value = Convert.ToDouble(TBChangeValue.Text);
            double balance = 0;
            using (StreamReader sr = new StreamReader(file1)) { balance = Convert.ToDouble(File.ReadAllText(file1)); }
            BankAccount bank = new BankAccount(NameOfAccount, balance);
            bank.Deposit(value);

            ShowAll(file);
            using (StreamReader sr = new StreamReader(file1)) { Labelvalue.Content = File.ReadAllText(file1); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string NameOfAccount = TB.Text;
            var file = @"D:\Lab2Bank\" + NameOfAccount + "_history.txt";
            var file1 = @"D:\Lab2Bank\" + NameOfAccount + "_balance.txt";
            double value = Convert.ToDouble(TBChangeValue.Text);
            double balance = 0;
            using (StreamReader sr = new StreamReader(file1)) { balance = Convert.ToDouble(File.ReadAllText(file1)); }
            BankAccount bank = new BankAccount(NameOfAccount, balance);
            bank.Withdraw(value);
            ShowAll(file);
            using (StreamReader sr = new StreamReader(file1)) { Labelvalue.Content = File.ReadAllText(file1); }

        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NameOfAccount = TB.Text;


            var file = @"D:\Lab2Bank\" + NameOfAccount + "_history.txt";
            var file1 = @"D:\Lab2Bank\" + NameOfAccount + "_balance.txt";
            if (File.Exists(file1))
            {
                ShowAll(file);
                LabelNumber.Content = NameOfAccount;

                using (StreamReader sr = new StreamReader(file1)) { Labelvalue.Content = File.ReadAllText(file1); }

            }
            else
            {
                MessageBox.Show("Рахунок не знайдено. Перевірте номер або створіть новий рахунок");

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NameOfAccount = TB.Text;
            var file = @"D:\Lab2Bank\" + NameOfAccount + "_history.txt";
            var file1 = @"D:\Lab2Bank\" + NameOfAccount + "_balance.txt";
            double balance = 0;

            BankAccount.NewAccount(NameOfAccount);
            using (StreamReader sr = new StreamReader(file1)) { balance = Convert.ToDouble(File.ReadAllText(file1)); }
            ShowAll(file);
            LabelNumber.Content = NameOfAccount;

            using (StreamReader sr = new StreamReader(file1))
            {
                Labelvalue.Content = File.ReadAllText(file1);

            }
        }
    }
}