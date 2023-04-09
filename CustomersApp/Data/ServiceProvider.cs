using CustomersApp.Model;

namespace CustomersApp;

public static class ServiceProvider
{
    private static CustomerDbContext? _customerDbContext;
    private static CustomerService? _customerService;
    private static PdfService? _pdfService;
    private static YamlPropertiesProvider? _propertiesProvider;
    
    public static CustomerDbContext DbContextInstance()
    {
        if (_customerDbContext == null)
        {
            _customerDbContext = new CustomerDbContext();
        }

        return _customerDbContext;
    }

    public static CustomerService CustomerServiceInstance()
    {
        if (_customerService == null)
        {
            _customerService = new CustomerService(
                _customerDbContext == null ? DbContextInstance() : _customerDbContext
            );
        }

        return _customerService;
    }
    
    public static PdfService PdfServiceInstance()
    {
        if (_pdfService == null)
        {
            _pdfService = new PdfService();
        }

        return _pdfService;
    }
    
    public static YamlPropertiesProvider YamlPropertiesProviderInstance()
    {
        if (_propertiesProvider == null)
        {
            _propertiesProvider = new YamlPropertiesProvider();
        }

        return _propertiesProvider;
    }
}