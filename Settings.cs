using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Language_Menu LANG;

    public void Start()
    {
        changeLanguage("polish");
    }

    public bool checkString(string message, bool at = false)
    {
        int countAt = 0;
        int countDot = 0;
        for (int i = 0; i < message.Length; i++)
        {
            switch (message[i])
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case 'q':
                case 'w':
                case 'e':
                case 'r':
                case 't':
                case 'y':
                case 'u':
                case 'i':
                case 'o':
                case 'p':
                case 'a':
                case 's':
                case 'd':
                case 'f':
                case 'g':
                case 'h':
                case 'j':
                case 'k':
                case 'l':
                case 'z':
                case 'x':
                case 'c':
                case 'v':
                case 'b':
                case 'n':
                case 'm':
                case 'Q':
                case 'W':
                case 'E':
                case 'R':
                case 'T':
                case 'Y':
                case 'U':
                case 'I':
                case 'O':
                case 'P':
                case 'A':
                case 'S':
                case 'D':
                case 'F':
                case 'G':
                case 'H':
                case 'J':
                case 'K':
                case 'L':
                case 'Z':
                case 'X':
                case 'C':
                case 'V':
                case 'B':
                case 'N':
                case 'M':
                    {
                        continue;
                    }
                case '@':
                    {
                        if (at)
                        {
                            countAt++;
                            continue;
                        }
                        else
                            return false;
                    }
                case '.':
                    {
                        if (at)
                        {
                            countDot++;
                            continue;
                        }
                        else
                            return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        if(at)
        {
            if (countAt == 1 && countDot > 0)
                return true;
            else
                return false;
        }
        else
        {
            return true;
        }
    }

    public void changeLanguage () {
        Image image = GameObject.FindGameObjectWithTag("Language").GetComponent<Image>();
        if(LANG is Polish)
        {
            image.sprite = Resources.Load<Sprite>("Language/english");
            LANG = new English();
        }
        else
        {
            image.sprite = Resources.Load<Sprite>("Language/polish");
            LANG = new Polish();
        }
    }

    public void changeLanguage(string lang)
    {
        Image image = GameObject.FindGameObjectWithTag("Language").GetComponent<Image>();
        if (lang == "english")
        {
            image.sprite = Resources.Load<Sprite>("Language/english");
            LANG = new English();
        }
        else
        {
            image.sprite = Resources.Load<Sprite>("Language/polish");
            LANG = new Polish();
        }
    }

    public byte[] saltString(string content)
    {
        byte[] hash = System.Text.Encoding.ASCII.GetBytes(content);
        for (int i = 0; i < hash.Length; i++)
        {
            byte count = 0;
            if (i <= 2)
                count = 2;
            else if (i <= 4)
                count = 6;
            else if (i <= 7)
                count = 3;
            else if (i <= 11)
                count = 2;
            else if (i >= 15 && i <= 19)
                count = 4;
            else
            {
                if (i % 2 == 0)
                    count = 1;
                else
                    count = 2;
            }

            if (i % 2 == 0)
                hash[i] += count;
            else if (i % 2 == 1)
                hash[i] += count;
        }
        return hash;
    }
}