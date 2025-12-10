using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _newTitle = string.Empty;
        private Category _selectedCategory;

        public ObservableCollection<ToDoItem> Items { get; } = new ObservableCollection<ToDoItem>();

        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public string NewTitle
        {
            get => _newTitle;
            set
            {
                _newTitle = value;
                OnPropertyChanged();
                (AddCommand as Command)?.ChangeCanExecute();
            }
        }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Categories.Add(new Category { Name = "Общие", Color = Colors.YellowGreen });
            Categories.Add(new Category { Name = "Работа", Color = Colors.MediumBlue });
            Categories.Add(new Category { Name = "Личное", Color = Colors.Purple });
            Categories.Add(new Category { Name = "Покупки", Color = Colors.OrangeRed });

            SelectedCategory = Categories[0];

            AddCommand = new Command(AddItem, () => !string.IsNullOrWhiteSpace(NewTitle));
            DeleteCommand = new Command<ToDoItem>(DeleteItem);
        }

        private void AddItem()
        {
            var title = NewTitle?.Trim();

            var item = new ToDoItem
            {
                Title = title,
                Category = SelectedCategory
            };

            Items.Add(item);
            NewTitle = string.Empty;
        }

        private void DeleteItem(ToDoItem item)
        {
            Items.Remove(item);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
