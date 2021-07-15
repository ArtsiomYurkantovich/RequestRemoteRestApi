using System.Threading.Tasks;

namespace TestTaskRemoteRestApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
           MenuOptions menu = new MenuOptions();
           await menu.RunApplicationAsync();
          
        }
    }
}
