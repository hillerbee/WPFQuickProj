using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            AddName();
        }

        private void ButtonDeleteName_Click(object sender, RoutedEventArgs e)
        {
            lstNames.Items.Remove(lstNames.SelectedItem);
        }

        /// <summary>
        /// Listens for arrow key inputs, as well as the ctrl modifier and the enter key
        /// Pressing ctrl + up or down will move the current selected item in array
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyEventListener(object sender, KeyEventArgs e)
        {
            int currentIndex = lstNames.SelectedIndex;
            switch (e.Key)
            {
                case Key.Up:
                    if (currentIndex > 0) { currentIndex--; }
                    else { currentIndex = 0; } // special case, if there is no selected item in list set it to the first item

                    if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        MoveName(lstNames.SelectedIndex, lstNames.SelectedIndex - 1);
                        break;
                    }
                    break;
                case Key.Down:
                    if (currentIndex < lstNames.Items.Count) { currentIndex++; }

                    if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        MoveName(lstNames.SelectedIndex, lstNames.SelectedIndex + 1);
                        break;
                    }
                    break;
                case Key.Enter:
                    AddName();
                    break;
                default:
                    txtName.Focus(); // allows for immediate typing in names field
                    break;
            }
            lstNames.SelectedIndex = currentIndex;
        }

        /// <summary>
        /// If text box is not empty or a repeated name
        /// take entered string, capitalize it, then add it to list
        /// </summary>
        private void AddName()
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
            {
                // The code will always convert names to Uppercase
                char[] c = txtName.Text.ToCharArray();
                c[0] = char.ToUpper(c[0]);
                lstNames.Items.Add(new String(c));
                txtName.Clear();
            }
        }

        /// <summary>
        /// Rearranges lstNames
        /// </summary>
        /// <param name="curr"></param>
        /// <param name="next"></param>
        private void MoveName(int curr, int next)
        {
            var item1 = lstNames.Items.GetItemAt(curr);
            // if the item is moving up the list
            if (curr > next && next >= 0)
            {
                lstNames.Items.RemoveAt(curr);
                lstNames.Items.Insert(next, item1);
            }
            // if it is moving down
            else if (curr < next && next < lstNames.Items.Count)
            {
                lstNames.Items.RemoveAt(curr);
                lstNames.Items.Insert(next, item1);
            }
        }
    }
}
