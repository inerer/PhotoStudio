namespace PhotoStudio;

public class PhoneNumberValidate
{
    public bool CheckFirstSymbol(string phoneNumber)
    {
        if (phoneNumber[0] != '8' || (phoneNumber[0] != '+' && phoneNumber[1] != '7'))
            return false;
        return true;

    }
}