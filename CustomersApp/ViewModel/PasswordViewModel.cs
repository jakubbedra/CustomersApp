using System.ComponentModel;
using System.Windows.Input;
using CustomersApp.View;
using CustomersApp.ViewModel.Commands;

namespace CustomersApp.ViewModel;

public class PasswordViewModel : INotifyPropertyChanged
{
    private string _validationError;

    public string ValidationError
    {
        get
        {
            return _validationError;
        }
        set
        {
            _validationError = value;
            OnPropertyChanged(nameof(ValidationError));
        }
    }

    public bool PasswordValid { get; set; }

    private string _password;
    
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public PasswordWindow Window { get; set; }

    private YamlPropertiesProvider _provider;

    public ICommand ValidatePasswordCommand {get; set;}

    public PasswordViewModel()
    {
        ValidationError = "";
        Password = "";
        PasswordValid = false;
        _provider = ServiceProvider.YamlPropertiesProviderInstance();
        ValidatePasswordCommand = new ValidatePasswordCommand(this);
    }

    public void ValidatePassword()
    {
        //todo
        PasswordValid = _provider.GetAppPassword() == Password;
        if (!PasswordValid)
        {
            ValidationError = "Niepoprawne hasło";
        }
        else
        {
            MainWindow window = new MainWindow();
            window.Show();
            Window.Close();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}