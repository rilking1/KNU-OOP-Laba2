using System;
using Microsoft.Maui.Controls;

namespace Laba2
{
    class Program1
    {
        static void vfv()
        {
            // Задайте шлях до вашого файлу
            string filePath = "Resources/Raw/kex.txt";

            // Задайте слово, яке ви шукаєте
            string searchWord = "Вірш";

            // Виклик функції пошуку
            SearchWordInFile(filePath, searchWord);
        }

        static void SearchWordInFile(string filePath, string searchWord)
        {
            try
            {
                // Використовуйте StreamReader для зчитування файлу
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;

                    // Зчитуйте рядок за рядком
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Перевіряйте, чи містить рядок шукане слово
                        if (line.Contains(searchWord))
                        {
                            Console.WriteLine($"Знайдено слово '{searchWord}' в рядку: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoadDataButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Button Clicked", "Load Data button clicked", "OK");
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Button Clicked", "Search button clicked", "OK");
        }

        async void ClearButton_Clicked(object sender, EventArgs e)
        {
            await label.RelRotateTo(360, 1000);
        }

        //стибрив нижній код з книги омельчук с.257
        private async void ConfirmExitButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?", "Так", "Ні");
            if (answer)
            {
                System.Environment.Exit(0);
            }
        }
    }
}