using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubBoardController : MonoBehaviour {


    public Button[] buttonArraytemp = new Button[9]; //Allows for the array to show up in Unity eddior
    Button[,] buttonArray; //Makes it so I can format what is given from the editor to a 2d array
    public GameController gameController; //Place holder for the gamecontroller object
    public Text subBoard; //the text value of the Subboard

    // Start is called before the first frame update
    void Start() {
        buttonArray = new Button[3, 3]; // Sets it so that button array is a 3x3 2D Array
        FormatButtonArray();
        ToggleSubBoardInteractble(true);
    }

    //This is called When a button is clicked in the game
    public void AddMove(Button button) {
            if (CheckEmpty(button.GetComponentInChildren<Text>().text)) { //Makes sure that the button is empty so they dont get over written
                SetButtonText(button); //Sets the text value of the button
                if (CheckEmpty(subBoard.text)) { //Checks if the SubBoard Has been won or not so not to be over written
                    if (CheckSubBoardWin()) { //Calls a method that checks all possible win cases on the 2D Array
                        SetSubBoardText(); // Sets the text of the subboard 
                        if (gameController.CheckGameWin()) { //checks for a game win
                            gameController.GameOver(); // Ends the game if true
                        }
                    }
                }
                // If it gets here the game is not over and is able to end move. 
                //This also gets the x and y of the button and tells the game controller where it is so it can force the next move
                gameController.EndMove(button.GetComponentInChildren<ButtonController>().buttonX, button.GetComponentInChildren<ButtonController>().buttonY);
            }
    }

    //Checks to see if the passed location is empty.
    private bool CheckEmpty(string text) {
        if (text == "") {
            return true;
        }
        return false;
    }

    //Formats buttonArrayTemp from a 1D array to A 2D array 
    private void FormatButtonArray() {
        int i = 0;
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                buttonArray[row, col] = buttonArraytemp[i];
                i++;
            }
        }
    }

    //Iterates over the buttonArray and grabs all of the strings on the text object and makes an 2D array of Strings
    private string[,] MakeTextArray() {
        string[,] textArray = new string[3, 3];
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                textArray[row, col] = buttonArray[row, col].GetComponentInChildren<Text>().text;
            }
        }
        return textArray;
    }

    //Checks if the subboard was won on the previous move
    private bool CheckSubBoardWin() {
        string[,] textArray = MakeTextArray();

        //Across
        if (textArray[0, 0] == gameController.currentPlayer && textArray[0, 1] == gameController.currentPlayer && textArray[0, 2] == gameController.currentPlayer) return true;
        if (textArray[1, 0] == gameController.currentPlayer && textArray[1, 1] == gameController.currentPlayer && textArray[1, 2] == gameController.currentPlayer) return true;
        if (textArray[2, 0] == gameController.currentPlayer && textArray[2, 1] == gameController.currentPlayer && textArray[2, 2] == gameController.currentPlayer) return true;

        //Verticle
        if (textArray[0, 0] == gameController.currentPlayer && textArray[1, 0] == gameController.currentPlayer && textArray[2, 0] == gameController.currentPlayer) return true;
        if (textArray[0, 1] == gameController.currentPlayer && textArray[1, 1] == gameController.currentPlayer && textArray[2, 1] == gameController.currentPlayer) return true;
        if (textArray[0, 2] == gameController.currentPlayer && textArray[1, 2] == gameController.currentPlayer && textArray[2, 2] == gameController.currentPlayer) return true;

        //Diagonal
        if (textArray[0, 0] == gameController.currentPlayer && textArray[1, 1] == gameController.currentPlayer && textArray[2, 2] == gameController.currentPlayer) return true;
        if (textArray[2, 0] == gameController.currentPlayer && textArray[1, 1] == gameController.currentPlayer && textArray[0, 2] == gameController.currentPlayer) return true;

        return false;
    }


    //Sets the text of the button that was clicked
    private void SetButtonText(Button button) {
        button.GetComponentInChildren<Text>().text = gameController.currentPlayer;
    }


    //Sets the text of this SubBoard
    private void SetSubBoardText() {
        subBoard.GetComponentInChildren<Text>().text = gameController.currentPlayer;
    }


    // toggles if the user can interact with the buttons on the subboard
    public void ToggleSubBoardInteractble(bool toggle) {
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                buttonArray[row, col].interactable = toggle;
            }
        }
    }

    public void RemoveTextFromButtonArray() {
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                buttonArray[row, col].GetComponentInChildren<Text>().text = ""; //Sets the text to an empty string
            }
        }
    }


}
