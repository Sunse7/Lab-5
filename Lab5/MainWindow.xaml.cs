using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> users { get; set; }
        private ObservableCollection<User> admin { get; set; }
        private User SelectedUser { get; set; }
        private User SelectedAdmin { get; set; }
        private bool updateUser = false;
        public MainWindow()
        {
            InitializeComponent();
            users = new ObservableCollection<User>();
            admin = new ObservableCollection<User>();
            userListBox.ItemsSource = users;
            adminListBox.ItemsSource = admin;
            userListBox.DisplayMemberPath = "Name";
            adminListBox.DisplayMemberPath = "Name";
            addUserButton.IsEnabled = false;
            removeUserButton.IsEnabled = false;
            makeAdminButton.IsEnabled = false;
            removeAdminButton.IsEnabled = false;
            changeUserButton.IsEnabled = false;

        }

        private void OnNameInput(object sender, TextChangedEventArgs e)
        {
            if (nameInputTextBox.Text == "" || emailInputTextBox.Text == "")
            {
                addUserButton.IsEnabled = false;
            }
            else
            {
                addUserButton.IsEnabled = true;
            }
        }
        private void OnAddUserButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedUser = (User)userListBox.SelectedItem;
           
            if(updateUser)
            {
                changeUserButton.IsEnabled = false;
                SelectedUser.Name = nameInputTextBox.Text;
                SelectedUser.Email = emailInputTextBox.Text;
                userListBox.UnselectAll();
                nameInputTextBox.Text = "";
                emailInputTextBox.Text = "";
            }
            else
            {
                users.Add(new User(nameInputTextBox.Text, emailInputTextBox.Text));
                nameInputTextBox.Text = "";
                emailInputTextBox.Text = "";
            }
        }

        private void OnUserListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedUser = (User)userListBox.SelectedItem;
            adminListBox.UnselectAll();
            
            if (SelectedUser == null)
            {
                UserNameInfoLabel.Content = "";
                UserEmailInfoLabel.Content = "";
                removeUserButton.IsEnabled = false;
                makeAdminButton.IsEnabled = false;
                changeUserButton.IsEnabled = false;
            }
            else
            {
                UserNameInfoLabel.Content = SelectedUser.Name;
                UserEmailInfoLabel.Content = SelectedUser.Email;
                removeUserButton.IsEnabled = true;
                makeAdminButton.IsEnabled = true;
                changeUserButton.IsEnabled = true;
            }
        }

        private void OnRemoveSelectedUserClick(object sender, RoutedEventArgs e)
        {
            users.Remove(SelectedUser);
        }

        private void OnEmailInput(object sender, TextChangedEventArgs e)
        {
            if (nameInputTextBox.Text == "" || emailInputTextBox.Text == "")
            {
                addUserButton.IsEnabled = false;
            }
            else
            {
                addUserButton.IsEnabled = true;
            }
        }

        private void OnMakeAdminButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedAdmin = (User)userListBox.SelectedItem;
            admin.Add(new User(SelectedAdmin.Name, SelectedAdmin.Email));
            users.Remove(SelectedUser);        
        }

        private void OnRemoveAdminButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedAdmin = (User)adminListBox.SelectedItem;
            users.Add(SelectedAdmin);
            admin.Remove(SelectedAdmin);
        }

        private void OnAdminLitsBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAdmin = (User)adminListBox.SelectedItem;
            userListBox.UnselectAll();
            if(SelectedAdmin == null)
            {
                UserNameInfoLabel.Content = "";
                UserEmailInfoLabel.Content = "";
                removeAdminButton.IsEnabled = false;
            }
            else
            {
                UserNameInfoLabel.Content = SelectedAdmin.Name;
                UserEmailInfoLabel.Content = SelectedAdmin.Email;
                removeAdminButton.IsEnabled = true;
            }
        }

        private void OnChangeUserButtonClick(object sender, RoutedEventArgs e)
        {
            nameInputTextBox.Text = SelectedUser.Name;
            emailInputTextBox.Text = SelectedUser.Email;
            updateUser = true;
            addUserButton.Content = "Save";
            makeAdminButton.IsEnabled = false;
            removeUserButton.IsEnabled = false;     
        }
    }
}
