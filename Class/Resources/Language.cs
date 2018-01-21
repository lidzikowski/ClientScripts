
public abstract class Language
{
    public string FORUM = "Forum";
    public string HELP = "Help";
    public string SETTINGS = "Settings";
    public string EXIT = "Exit";
    public string HEADER_SIGN_IN = "Sign in";
    public string USERNAME = "Username";
    public string PASSWORD = "Password";
    public string KEEP_ME = "Keep me";
    public string SIGN_IN = "Sign In";
    public string HEADER_REGISTER = "Register";
    public string EMAIL = "Email";
    public string ACCEPT_RULES = "Im accept rules.";
    public string CREATE_ACCOUNT = "Create account";
    public string HEADER_CONTENT = "Information and News";
    

    public string INFO_WARNING = "Warning!";
    public string SOCKET_NOT_CONNECTED = "Server is not connected.";
    public string USERNAME_IS_EMPTY = "Field username is empty.";
    public string USERNAME_IS_INCORRECT = "Username has invalid characters.";
    public string USERNAME_LENGTH = "Username has incorrect length (4-30).";
    public string PASSWORD_IS_EMPTY = "Field password is empty.";
    public string PASSWORD_LENGTH = "Password has incorrect length (4-30).";
    public string PASSWORD_IS_INCORRECT = "Password has invalid characters.";
    public string EMAIL_IS_EMPTY = "Field email is empty";
    public string EMAIL_LENGTH = "Email has incorrect length (4-50).";
    public string EMAIL_IS_INCORRECT = "Email has invalid characters.";
    public string RULES_NOT_ACCEPTED = "Rules is not accepted.";


}

public class English : Language { }

public class Polish : Language
{
    public Polish()
    {
        HELP = "Pomoc";
        SETTINGS = "Ustawienia";
        EXIT = "Wyjście";
        HEADER_SIGN_IN = "Logowanie";
        USERNAME = "Użytkownik";
        PASSWORD = "Hasło";
        KEEP_ME = "Zapamiętaj";
        SIGN_IN = "Zaloguj";
        HEADER_REGISTER = "Rejestracja";
        ACCEPT_RULES = "Akceptuję regulamin.";
        CREATE_ACCOUNT = "Utwórz konto";
        HEADER_CONTENT = "Informacje i Nowości";

        INFO_WARNING = "Ostrzeżenie!";
        SOCKET_NOT_CONNECTED = "Brak połączenia z serwerem.";
        USERNAME_IS_EMPTY = "Pole użytkownika jest puste.";
        USERNAME_LENGTH = "Pole użytkownika posiada nieprawidłową długość (4-30).";
        USERNAME_IS_INCORRECT = "Pole użytkownika posiada nieprawidłowe znaki.";
        PASSWORD_IS_EMPTY = "Pole hasła jest puste.";
        PASSWORD_LENGTH = "Pole hasła posiada nieprawidłową długość (4-30).";
        PASSWORD_IS_INCORRECT = "Pole hasła posiada nieprawidłowe znaki.";
        EMAIL_IS_EMPTY = "Pole emaila jest puste.";
        EMAIL_LENGTH = "Pole emaila posiada nieprawidłową długość (4-50).";
        EMAIL_IS_INCORRECT = "Pole emaila posiada nieprawidłowe znaki.";
        RULES_NOT_ACCEPTED = "Regulamin nie został zaakceptowany.";
    }
}