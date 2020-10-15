using System.Threading.Tasks;

namespace CallNodeJsFromCore
{
    public interface IJavaScriptService
    {
        Task<string> Hello(string name);
    }
}