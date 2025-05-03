using System.Text;

namespace TarefasBackEnd.Config;

public class AppSettings
{
    public string JWT_SECRET_KEY { get; set; }


    public string getSecretKey()
    {
        return Encoding.UTF8.GetString(getSecretKeyBytes());
    }
    
    public byte[] getSecretKeyBytes()
    {
        return Encoding.UTF8.GetBytes(JWT_SECRET_KEY);
    }
}