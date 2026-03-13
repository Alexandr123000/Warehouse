namespace Base.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _text;
        public string text 
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(text));
            }
        }
    }
}