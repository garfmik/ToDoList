using Microsoft.Maui.Controls;
using ToDoList.ViewModels;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

    }
}
