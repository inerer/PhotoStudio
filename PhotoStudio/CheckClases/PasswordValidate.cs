using System.Linq;

namespace PhotoStudio;

public class PasswordValidate
{
    public bool PasswordResult(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;
        if (!password.Any(char.IsDigit))
            return false;
        if (!password.Any(char.IsUpper))
            return false;
        if (password.Length < 8)
            return false;
        if (!password.Any(char.IsPunctuation))
            return false;

        return true;
    }
}