using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class BankInfo
{
    public string Bank { get; set; }
    public double BuyRate { get; set; }
    public double SellRate { get; set; }
    public string Address { get; set; }
    public double RateDifference => SellRate - BuyRate;
}

class Program
{
    static void Main(string[] args)
    {
        
        string filePath = "C:/Users/LEO/source/repos/Lab1Git/Data.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден. Проверьте путь.");
            return;
        }

        // Чтение и обработка данных
        List<BankInfo> bankInfos = new List<BankInfo>();
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(' ', 4); // Разделение строки
                bankInfos.Add(new BankInfo
                {
                    Bank = parts[0],
                    BuyRate = double.Parse(parts[1]),
                    SellRate = double.Parse(parts[2]),
                    Address = parts[3]
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке файла: {ex.Message}");
            return;
        }

        // Меню
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Вывести курсы валюты во всех отделениях банка «Беларусбанк».");
            Console.WriteLine("2. Вывести курсы валюты и адреса отделений банков, в которых продажа меньше 2.5.");
            Console.WriteLine("3. Сортировать данные по убыванию разницы между стоимостью продажи и покупки (Bubble sort).");
            Console.WriteLine("4. Сортировать данные по возрастанию названия банка и адреса отделения (Merge sort).");
            Console.WriteLine("5. Выйти.");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    FilterByBank(bankInfos, "Беларусбанк");
                    break;
                case "2":
                    FilterBySellRate(bankInfos, 2.5);
                    break;
                case "3":
                    SortByRateDifferenceBubble(bankInfos);
                    break;
                case "4":
                    SortByBankAndAddressMerge(bankInfos);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void FilterByBank(List<BankInfo> banks, string targetBank)
    {
        var filtered = banks.Where(b => b.Bank.Equals(targetBank, StringComparison.OrdinalIgnoreCase)).ToList();
        Console.WriteLine($"\nКурсы валют в отделениях банка «{targetBank}»:");
        foreach (var bank in filtered)
        {
            Console.WriteLine($"Купить: {bank.BuyRate}, Продать: {bank.SellRate}, Адрес: {bank.Address}");
        }
        if (!filtered.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
        }
    }

    static void FilterBySellRate(List<BankInfo> banks, double maxSellRate)
    {
        var filtered = banks.Where(b => b.SellRate < maxSellRate).ToList();
        Console.WriteLine($"\nОтделения банков, где продажа меньше {maxSellRate}:");
        foreach (var bank in filtered)
        {
            Console.WriteLine($"Банк: {bank.Bank}, Купить: {bank.BuyRate}, Продать: {bank.SellRate}, Адрес: {bank.Address}");
        }
        if (!filtered.Any())
        {
            Console.WriteLine("Данные отсутствуют.");
        }
    }

    static void SortByRateDifferenceBubble(List<BankInfo> banks)
    {
        for (int i = 0; i < banks.Count - 1; i++)
        {
            for (int j = 0; j < banks.Count - i - 1; j++)
            {
                if (banks[j].RateDifference < banks[j + 1].RateDifference)
                {
                    var temp = banks[j];
                    banks[j] = banks[j + 1];
                    banks[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("\nДанные отсортированы по убыванию разницы между продажей и покупкой:");
        foreach (var bank in banks)
        {
            Console.WriteLine($"Банк: {bank.Bank}, Разница: {bank.RateDifference:F2}, Адрес: {bank.Address}");
        }
    }

    static void SortByBankAndAddressMerge(List<BankInfo> banks)
    {
        banks = MergeSort(banks);

        Console.WriteLine("\nДанные отсортированы по возрастанию названия банка и адреса:");
        foreach (var bank in banks)
        {
            Console.WriteLine($"Банк: {bank.Bank}, Адрес: {bank.Address}, Купить: {bank.BuyRate}, Продать: {bank.SellRate}");
        }
    }

    static List<BankInfo> MergeSort(List<BankInfo> banks)
    {
        if (banks.Count <= 1)
            return banks;

        int mid = banks.Count / 2;
        var left = MergeSort(banks.GetRange(0, mid));
        var right = MergeSort(banks.GetRange(mid, banks.Count - mid));

        return Merge(left, right);
    }

    static List<BankInfo> Merge(List<BankInfo> left, List<BankInfo> right)
    {
        var result = new List<BankInfo>();
        int i = 0, j = 0;

        while (i < left.Count && j < right.Count)
        {
            if (string.Compare(left[i].Bank, right[j].Bank) < 0 ||
                (left[i].Bank == right[j].Bank && string.Compare(left[i].Address, right[j].Address) < 0))
            {
                result.Add(left[i]);
                i++;
            }
            else
            {
                result.Add(right[j]);
                j++;
            }
        }

        while (i < left.Count)
        {
            result.Add(left[i]);
            i++;
        }

        while (j < right.Count)
        {
            result.Add(right[j]);
            j++;
        }

        return result;
    }
}
