using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PhotoStudio;

public class GetHash
{
    public  string GetHash1(string password)
    {
        using var hash = SHA1.Create();
        return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
    }
}