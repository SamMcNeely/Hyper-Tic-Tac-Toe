using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public string currentPlayer;
    public GameObject[] subBoardArrayTemp; //Is what is visiable in the Unity Editor so SubBoard refferences could be added.
    private GameObject[,] subBoardArray; // 2D array for use in c
    public Text currentPlayerText; // Tells the users who turn it is
    private bool devToolsOn;

    // Start is called before the first frame update
    void Start() {
        subBoardArray = new GameObject[3, 3];
        devToolsOn = false;
        FormatSubBoardArray();
        StartInteractive();
    }

    void Update() {
        if(Input.GetKeyDown("f3")) {
            devToolsOn = !devToolsOn;
            Debug.Log("DevToolsToggled to: " + devToolsOn);
        }
        if(devToolsOn) {
            DevToolSubBoardControl();
            DevToolCurrentPlayer();
        }
    }

    // Called When a user clicks the "NEW GAME" Button
    public void NewGame() {
        currentPlayer = "X";
        ToggleAllInteractability(true);
        RemoveTextSubBoard();
        StartInteractive();
    }

    // Itterates over the array of subboards and removes texts from the objects
    private void RemoveTextSubBoard() {
        Debug.Log("removing text");
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                subBoardArray[row, col].GetComponent<SubBoardController>().subBoard.text = ""; // Sets text to an empty string
                subBoardArray[row, col].GetComponent<SubBoardController>().RemoveTextFromButtonArray();
            }
        }
    }

    public void EndMove(int row, int col) {
        ToggleAllInteractability(false); //Makes whole board not interactable
        SetNextMoveInteractable(row, col); // Makes subboard that is next interableable
        SetCurrentPlayer(); // Changes current player
        SetCurrentPlayerText(); // Sets the 
    }

    // Formats the 1D array to a 2D array
    private void FormatSubBoardArray() {
        int i = 0;
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                subBoardArray[row, col] = subBoardArrayTemp[i]; 
                i++;
            }
        }
    }

    private void SetNextMoveInteractable(int row, int col) {
        subBoardArray[row, col].GetComponent<SubBoardController>().ToggleSubBoardInteractble(true);
    }

    private void StartInteractive() {
        Debug.Log("start Interactive");
        subBoardArray[1, 1].GetComponent<SubBoardController>().ToggleSubBoardInteractble(false);
    }

    private void ToggleAllInteractability(bool toggle) {
        for(int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                subBoardArray[row, col].GetComponent<SubBoardController>().ToggleSubBoardInteractble(toggle);
            }
        }
    }

    private void SetCurrentPlayerText() {
        currentPlayerText.text = "Current Player: " + currentPlayer;
    }

    public bool CheckGameWin() {
        string[,] textArray = MakeTextArray();

        //Across
        if (textArray[0, 0] == currentPlayer && textArray[0, 1] == currentPlayer && textArray[0, 2] == currentPlayer) return true;
        if (textArray[1, 0] == currentPlayer && textArray[1, 1] == currentPlayer && textArray[1, 2] == currentPlayer) return true;
        if (textArray[2, 0] == currentPlayer && textArray[2, 1] == currentPlayer && textArray[2, 2] == currentPlayer) return true;

        //Verticle
        if (textArray[0, 0] == currentPlayer && textArray[1, 0] == currentPlayer && textArray[2, 0] == currentPlayer) return true;
        if (textArray[0, 1] == currentPlayer && textArray[1, 1] == currentPlayer && textArray[2, 1] == currentPlayer) return true;
        if (textArray[0, 2] == currentPlayer && textArray[1, 2] == currentPlayer && textArray[2, 2] == currentPlayer) return true;

        //Diagonal
        if (textArray[0, 0] == currentPlayer && textArray[1, 1] == currentPlayer && textArray[2, 2] == currentPlayer) return true;
        if (textArray[2, 0] == currentPlayer && textArray[1, 1] == currentPlayer && textArray[0, 2] == currentPlayer) return true;

        return false;
    }

    public void GameOver() {
        ToggleAllInteractability(false);
    }

    private string[,] MakeTextArray() {
        string[,] textArray = new string[3, 3];
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                textArray[row, col] = subBoardArray[row, col].GetComponentInChildren<SubBoardController>().GetSubBoardText();
            }
        }
        return textArray;
    }

    private void DevToolSubBoardControl() {
        if(Input.GetKeyDown("[7]")) {
            subBoardArray[0, 0].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[8]")) {
            subBoardArray[0, 1].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[9]")) {
            subBoardArray[0, 2].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[4]")) {
            subBoardArray[1, 0].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[5]")) {
            subBoardArray[1, 1].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[6]")) {
            subBoardArray[1, 2].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[1]")) {
            subBoardArray[2, 0].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[2]")) {
            subBoardArray[2, 1].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }
        if (Input.GetKeyDown("[3]")) {
            subBoardArray[2, 2].GetComponentInChildren<SubBoardController>().SetSubBoardText();
            if (CheckGameWin()) GameOver();
            SetCurrentPlayer();
        }

    }

    private void DevToolCurrentPlayer() {
        if(Input.GetKeyDown("x")) {
            currentPlayer = "X";
        }
        if(Input.GetKeyDown("o")) {
            currentPlayer = "O";
        }
    }

    private void SetCurrentPlayer() {
        if (currentPlayer == "X") {
            currentPlayer = "O";
        }
        else
            currentPlayer = "X";
    }
}
