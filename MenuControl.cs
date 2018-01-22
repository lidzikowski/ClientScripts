using UnityEngine;
using UnityEngine.UI;
using UnitySocketIO.Events;
using LitJson;

public class MenuControl : MonoBehaviour {

    private ClientSocket socket;
    private Language LANG;
    private GameObject infoMessage;

    private float timer = 0.5f;
    private Text serverStatus;
    private Button buttonSignIn;
    private Button buttonRegister;

    void Start () {
        socket = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<ClientSocket>();
        infoMessage = Resources.Load<GameObject>("Prefabs/infoMessage");

        serverStatus = GameObject.Find("serverStatus").GetComponent<Text>();
        buttonSignIn = GameObject.Find("button_signin").GetComponent<Button>();
        buttonRegister = GameObject.Find("button_register").GetComponent<Button>();

        socket.io.On("buildGame", (SocketIOEvent e) => {
            ServerData serverData = JsonMapper.ToObject<ServerData>(e.data);

            socket.gameResources = new GameResources();
            socket.gameData = new GameData();

            foreach (Item item in serverData.items)
                socket.gameData.items.Add(item.item_id, item);
            foreach (Map map in serverData.maps)
                socket.gameData.maps.Add(map.map_id, map);
            foreach (PlayerShip ship in serverData.playerShips)
                socket.gameData.playerShips.Add(ship.ship_name, ship);
            foreach (EnemieShip ship in serverData.enemieShips)
                socket.gameData.enemieShips.Add(ship.ship_name, ship);

            socket.localPlayer = new LocalPlayer(serverData.playerData, socket);
            if(socket.localPlayer != null)
                socket.changeScene("game");
        });

        setLanguage();
    }

    void Update()
    {
        if(timer > 1)
        {
            if (socket.socket_connected)
            {
                serverStatus.text = "ONLINE";
                serverStatus.color = Color.green;
                buttonSignIn.interactable = true;
                buttonRegister.interactable = true;
            }
            else
            {
                serverStatus.text = "OFFLINE";
                serverStatus.color = Color.red;
                buttonSignIn.interactable = false;
                buttonRegister.interactable = false;
            }
        }
        else
            timer += Time.deltaTime;
    }

    private void showError(string message)
    {
        GameObject go = Instantiate(infoMessage, GameObject.Find("Canvas").transform);
        go.transform.GetChild(0).GetComponent<Text>().text = LANG.INFO_WARNING;
        go.transform.GetChild(1).GetComponent<Text>().text = message;
    }

    public void signIn()
    {
        if (socket.socket_connected)
        {
            string username = GameObject.Find("panel-signin").transform.GetChild(1).GetComponent<InputField>().text;
            string password = GameObject.Find("panel-signin").transform.GetChild(2).GetComponent<InputField>().text;
            if(username != "")
            {
                if (password != "")
                {
                    if (username.Length >= 4 && username.Length <= 20)
                    {
                        if (password.Length >= 4 && password.Length <= 30)
                        {
                            Settings setting = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<Settings>();
                            if (setting.checkString(username))
                            {
                                if (setting.checkString(password))
                                {
                                    byte[] saltUsername = setting.saltString(username);
                                    byte[] saltPassword = setting.saltString(password);

                                    string newUsername = "";
                                    foreach (byte x in saltUsername)
                                        newUsername += x + "/01/";
                                    string newPassword = "";
                                    foreach (byte x in saltPassword)
                                        newPassword += x + "/01/";
                                    
                                    string array = "{\"username\":\"" + newUsername + "\",\"password\":\"" + newPassword + "\"}";
                                    socket.io.Emit("userSignIn", array, (string data) => {
                                        showError(data);
                                    });
                                }
                                else
                                    showError(LANG.PASSWORD_IS_INCORRECT);
                            }
                            else
                                showError(LANG.USERNAME_IS_INCORRECT);
                        }
                        else
                            showError(LANG.PASSWORD_LENGTH);
                    }
                    else
                        showError(LANG.USERNAME_LENGTH);
                }
                else
                    showError(LANG.PASSWORD_IS_EMPTY);
            }
            else
                showError(LANG.USERNAME_IS_EMPTY);
        }
        else
            showError(LANG.SOCKET_NOT_CONNECTED);
}

    public void createAccount()
    {
        if (socket.socket_connected)
        {
            string username = GameObject.Find("panel-register").transform.GetChild(1).GetComponent<InputField>().text;
            string password = GameObject.Find("panel-register").transform.GetChild(2).GetComponent<InputField>().text;
            string email = GameObject.Find("panel-register").transform.GetChild(3).GetComponent<InputField>().text;

            if (username != "")
            {
                if (password != "")
                {
                    if (email != "")
                    {
                        bool rules = GameObject.Find("panel-register").transform.GetChild(4).GetComponent<Toggle>().isOn;
                        if(rules)
                        {
                            if (username.Length >= 4 && username.Length <= 30)
                            {
                                if (password.Length >= 4 && password.Length <= 30)
                                {
                                    if (email.Length >= 4 && email.Length <= 50)
                                    {
                                        Settings setting = GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<Settings>();
                                        if (setting.checkString(username))
                                        {
                                            if (setting.checkString(password))
                                            {
                                                if (setting.checkString(email, true))
                                                {
                                                    byte[] saltUsername = setting.saltString(username);
                                                    byte[] saltPassword = setting.saltString(password);
                                                    byte[] saltEmail = setting.saltString(email);

                                                    string newUsername = "";
                                                    foreach (byte x in saltUsername)
                                                        newUsername += x + "/01/";
                                                    string newPassword = "";
                                                    foreach (byte x in saltPassword)
                                                        newPassword += x + "/01/";
                                                    string newEmail = "";
                                                    foreach (byte x in saltEmail)
                                                        newEmail += x + "/01/";

                                                    string array = "{\"username\":\"" + newUsername + "\",\"password\":\"" + newPassword + "\",\"email\":\"" + newEmail + "\",\"rules\":\"" + rules + "\"}";
                                                    socket.io.Emit("userRegister", array, (string data) => {
                                                        showError(data);
                                                    });
                                                }
                                                else
                                                    showError(LANG.EMAIL_IS_INCORRECT);
                                            }
                                            else
                                                showError(LANG.PASSWORD_IS_INCORRECT);
                                        }
                                        else
                                            showError(LANG.USERNAME_IS_INCORRECT);
                                    }
                                    else
                                        showError(LANG.EMAIL_LENGTH);
                                }
                                else
                                    showError(LANG.PASSWORD_LENGTH);
                            }
                            else
                                showError(LANG.USERNAME_LENGTH);
                        }
                        else
                            showError(LANG.RULES_NOT_ACCEPTED);
                    }
                    else
                        showError(LANG.PASSWORD_IS_EMPTY);
                }
                else
                    showError(LANG.PASSWORD_IS_EMPTY);
            }
            else
                showError(LANG.USERNAME_IS_EMPTY);
        }
        else
            showError(LANG.SOCKET_NOT_CONNECTED);
    }

    public void openHelp()
    {

    }

    public void openWebsite()
    {
        Application.OpenURL("https://cosmicspace.pl/");
    }

    public void gameExit()
    {
        Application.Quit();
    }

    public void changeLang()
    {
        GameObject.FindGameObjectWithTag("SocketIOController").GetComponent<Settings>().changeLanguage();
        setLanguage();
    }

    private void setLanguage()
    {
        LANG = socket.GetComponent<Settings>().LANG;

        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos)
        {
            switch (go.name)
            {
                case "button_forum":
                    go.transform.GetChild(0).GetComponent<Text>().text = LANG.FORUM;
                    break;
                case "button_help":
                    go.transform.GetChild(0).GetComponent<Text>().text = LANG.HELP;
                    break;
                case "button_settings":
                    go.transform.GetChild(0).GetComponent<Text>().text = LANG.SETTINGS;
                    break;
                case "button_exit":
                    go.transform.GetChild(0).GetComponent<Text>().text = LANG.EXIT;
                    break;
                case "button_keepme":
                    go.transform.GetChild(1).GetComponent<Text>().text = LANG.KEEP_ME;
                    break;
                case "button_signin":
                    go.transform.GetChild(0).GetComponent<Text>().text = LANG.SIGN_IN;
                    break;
                case "button_register":
                    go.transform.GetChild(0).GetComponent<Text>().text = LANG.HEADER_REGISTER;
                    break;
                case "input_username":
                    foreach (Transform t in go.transform)
                    {
                        if(t.name == "Placeholder")
                            t.GetComponent<Text>().text = LANG.USERNAME;
                    }
                    break;
                case "input_password":
                    foreach (Transform t in go.transform)
                    {
                        if (t.name == "Placeholder")
                            t.GetComponent<Text>().text = LANG.PASSWORD;
                    }
                    break;
                case "input_email":
                    foreach (Transform t in go.transform)
                    {
                        if (t.name == "Placeholder")
                            t.GetComponent<Text>().text = LANG.EMAIL;
                    }
                    break;
                case "input_rules":
                    go.transform.GetChild(1).GetComponent<Text>().text = LANG.ACCEPT_RULES;
                    break;
                case "header-signin":
                    go.GetComponent<Text>().text = LANG.HEADER_SIGN_IN;
                    break;
                case "header-register":
                    go.GetComponent<Text>().text = LANG.HEADER_REGISTER;
                    break;
                case "header-content":
                    go.GetComponent<Text>().text = LANG.HEADER_CONTENT;
                    break;
                default:
                    break;
            }
        }
    }
}