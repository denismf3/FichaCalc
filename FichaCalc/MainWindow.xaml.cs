using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FichaCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex RegexSomenteNumeros =
            new Regex("[^0-9,.]+");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaterial.Text))
            {
                MessageBox.Show(
                    "Informe o nome do material.",
                    "FichaCalc",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                txtMaterial.Focus();
                return;
            }

            if (!decimal.TryParse(txtQuantidade.Text, out decimal quantidade) ||
                !decimal.TryParse(txtValor.Text, out decimal valor) ||
                !decimal.TryParse(txtProducao.Text, out decimal producao))
            {
                MessageBox.Show(
                    "Preencha todos os valores corretamente.",
                    "FichaCalc",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (quantidade <= 0)
            {
                MessageBox.Show(
                    "A quantidade da embalagem deve ser maior que zero.",
                    "FichaCalc",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                txtQuantidade.Focus();
                return;
            }

            if (producao <= 0)
            {
                MessageBox.Show(
                    "A quantidade produzida deve ser maior que zero.",
                    "FichaCalc",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                txtProducao.Focus();
                return;
            }

            if (valor < 0)
            {
                MessageBox.Show(
                    "O valor da embalagem não pode ser negativo.",
                    "FichaCalc",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                txtValor.Focus();
                return;
            }

            string unidade = ObterUnidadeSelecionada();

            if (string.IsNullOrWhiteSpace(unidade))
            {
                MessageBox.Show(
                    "Selecione uma unidade de medida.",
                    "FichaCalc",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                cmbUnidade.Focus();
                return;
            }

            decimal consumoPorPar = quantidade / producao;
            decimal custoPorPar = valor / producao;
            decimal rendimento = producao / quantidade;

            txtResultadoConsumo.Text =
                $"Consumo por par: {consumoPorPar:F4} {unidade}";

            txtResultadoCusto.Text =
                $"Custo por par: R$ {custoPorPar:F4}";

            txtResultadoRendimento.Text =
                $"Rendimento: {rendimento:F4} pares/{unidade}";
        }

        private string ObterUnidadeSelecionada()
        {
            if (cmbUnidade.SelectedItem is ComboBoxItem item)
            {
                return item.Content?.ToString() ?? string.Empty;
            }

            return string.Empty;
        }

        private void SomenteNumeros_PreviewTextInput(
            object sender,
            TextCompositionEventArgs e)
        {
            e.Handled = RegexSomenteNumeros.IsMatch(e.Text);
        }

        private void BtnAdicionarUnidade_Click(
            object sender,
            RoutedEventArgs e)
        {
            MessageBox.Show(
                "Cadastro de novas unidades será implementado em seguida.",
                "FichaCalc",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void TxtValor_TextChanged(
            object sender,
            TextChangedEventArgs e)
        {
            placeholderValor.Visibility =
                string.IsNullOrWhiteSpace(txtValor.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }

        private void TxtQuantidade_TextChanged(
            object sender,
            TextChangedEventArgs e)
        {
            placeholderQuantidade.Visibility =
                string.IsNullOrWhiteSpace(txtQuantidade.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }

        private void TxtProducao_TextChanged(
            object sender,
            TextChangedEventArgs e)
        {
            placeholderProducao.Visibility =
                string.IsNullOrWhiteSpace(txtProducao.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
        }
    }
}