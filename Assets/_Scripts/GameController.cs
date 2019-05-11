using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public string currentPlayer = "X";
    public GameObject[] subBoardArrayTemp;
    private GameObject[,] subBoardArray = new GameObject[3, 3];
    private int moveNum = 0;
    public Text currentPlayerText;

    // Start is called before the first frame update
    void Start() {
        formatSubBoardArray();
        startInteractive();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void newGame() {
        Debug.Log("New Game Cleaning");
        currentPlayer = "X";
        ToggleAllInteractability(true);
        RemoveTextSubBoard();
        startInteractive();
    }

    private void RemoveTextSubBoard() {
        Debug.Log("removing text");
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                Debug.Log("Text In SubBoard: " + subBoardArray[row, col].GetComponentInChildren<Text>().text);
                subBoardArray[row, col].GetComponent<SubBoardController>().subBoard.text = "";
                subBoardArray[row, col].GetComponent<SubBoardController>().RemoveTextFromButtonArray();
            }
        }
    }

    public void EndMove(int row, int col) {
        ToggleAllInteractability(false);
        setNextMoveInteractable(row, col);
        moveNum++;
        setCurrentPlayer();
        SetCurrentPlayerText();
    }

    //TODO: Make this a method that both subboard and game controller can use
    private void formatSubBoardArray() {
        int i = 0;
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                subBoardArray[row, col] = subBoardArrayTemp[i]; 
                i++;
            }
        }
    }

    private void setNextMoveInteractable(int row, int col) {
        subBoardArray[row, col].GetComponent<SubBoardController>().toggleSubBoardInteractble(true);
    }

    private void startInteractive() {
        Debug.Log("start Interactive");
        subBoardArray[1, 1].GetComponent<SubBoardController>().toggleSubBoardInteractble(false);
    }

    private void ToggleAllInteractability(bool toggle) {
        for(int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                subBoardArray[row, col].GetComponent<SubBoardController>().toggleSubBoardInteractble(toggle);
            }
        }
    }

    private void SetCurrentPlayerText() {
        currentPlayerText.text = "Current Player: " + currentPlayer;
    }

    public bool checkGameWin() {
        string[,] textArray = makeTextArray();

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

    public void gameOver() {
        Debug.Log("Game Over");
        ToggleAllInteractability(false);
    }

    private string[,] makeTextArray() {
        string[,] textArray = new string[3, 3];
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                textArray[row, col] = subBoardArray[row, col].GetComponentInChildren<Text>().text;
            }
        }
        return textArray;
    }

    private void setCurrentPlayer() {
        if (currentPlayer == "X") {
            currentPlayer = "O";
        }
        else
            currentPlayer = "X";
    }
}
