using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fontComboBox.SelectedItem = fontComboBox.Items[0];
            FontComboBox_SelectionChanged(null, null);


        }

        // Створення нового файлу
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Document = new FlowDocument();
        }

        // Відкриття файлу
        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    string text = await reader.ReadToEndAsync();
                    txtEditor.Document = new FlowDocument(new Paragraph(new Run(text)));
                }
            }
        }

        // Вибір шрифту
        private void FontComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string fontFamily = ((ComboBoxItem)fontComboBox.SelectedItem).Content.ToString();
            TextRange selection = txtEditor.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new System.Windows.Media.FontFamily(fontFamily));
            }
        }

        //Вибір кольору
        private void FontColorRed_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.Text.Length > 0)
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            }
        }

        private void FontColorGreen_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.Text.Length > 0)
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green);
            }
        }

        private void FontColorBlue_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.Text.Length > 0)
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
            }
        }

        private void FontColorBlack_Click(object sender, RoutedEventArgs e)
        {
            if (txtEditor.Selection.Text.Length > 0)
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            }
        }


        //Жирний шрифт
        private void ToggleBold_Click(object sender, RoutedEventArgs e)
        {
            if (btnBold.IsChecked == true)
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                txtEditor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        // Вирівнювання абзаців
        private void ParagraphLeft_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Left);
        }

        private void ParagraphCenter_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Center);
        }

        private void ParagraphRight_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }

        private void ParagraphJustify_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, TextAlignment.Justify);
        }


        // Збереження файлу
        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    string text = new TextRange(txtEditor.Document.ContentStart, txtEditor.Document.ContentEnd).Text;
                    await writer.WriteAsync(text);
                }
            }
        }
    }
}