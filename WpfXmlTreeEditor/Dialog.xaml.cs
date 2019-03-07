using System.Windows;
using System.Windows.Controls;

namespace XMLViewer
{
    public partial class Dialog : Window
    {
        public object[] Results;

        public Dialog(string tile, params (string, object)[] args)
        {
            InitializeComponent();
            Results = new object[args.Length];
            int Index = 0;
            foreach (var item in args)
            {
                MainContent.RowDefinitions.Add(new RowDefinition());
                var Question = new TextBlock
                {
                    Text = item.Item1,
                    Margin = new Thickness(3),
                };
                Grid.SetRow(Question, Index);
                MainContent.Children.Add(Question);
                var Answere = new TextBox
                {
                    Text = item.Item2.ToString(),
                    Margin = new Thickness(3),
                };
                if (Index == 0)
                {
                    Answere.SelectAll();
                    Answere.Focus();
                }
                Answere.Tag = Index;
                Answere.MinWidth = 50;
                Answere.TextChanged += (s, e) => Results[(int)(s as FrameworkElement).Tag] = Answere.Text;
                Grid.SetRow(Answere, Index);
                Grid.SetColumn(Answere, 1);
                Index++;
                MainContent.Children.Add(Answere);
            }
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
