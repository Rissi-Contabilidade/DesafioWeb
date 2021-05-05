namespace DesafioWeb.Controllers
{
    public class RissiAPIDatabaseSettings : IRissiAPIDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IRissiAPIDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}