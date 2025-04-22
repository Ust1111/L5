using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace L5
{
    public partial class MainWindow : Window
    {
        private string _currentFilePath;

        public MainWindow()
        {
            InitializeComponent();
            Title = "Простой блокнот - Новый файл";
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextArea.Clear();
            _currentFilePath = null;
            Title = "Простой блокнот - Новый файл";
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    TextArea.Text = File.ReadAllText(openFileDialog.FileName);
                    _currentFilePath = openFileDialog.FileName;
                    Title = $"Простой блокнот - {Path.GetFileName(_currentFilePath)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии файла:\n{ex.Message}",
                                  "Ошибка",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error);
                }
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                SaveAs();
            }
            else
            {
                SaveToFile(_currentFilePath);
            }
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _currentFilePath = saveFileDialog.FileName;
                SaveToFile(_currentFilePath);
            }
        }

        private void SaveToFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, TextArea.Text);
                Title = $"Простой блокнот - {Path.GetFileName(filePath)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла:\n{ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
