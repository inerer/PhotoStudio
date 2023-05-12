namespace PhotoStudio;

public class PhoneNumberValidate
{
    public bool CheckFirstSymbol(string phoneNumber)
    {
        switch (phoneNumber[0])
        {
            case '8' when phoneNumber.Length==11:
            case '+' when phoneNumber[1] == '7' && phoneNumber.Length==12:
                return true;
            default:
                return false;
        }
    }
}