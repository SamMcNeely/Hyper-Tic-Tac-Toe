using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubBoardController : MonoBehaviour {
    // Start is called before the first frame update


    public Button[] buttonArraytemp = new Button[9]; //Allows for the array to show up in eddior
    Button[,] buttonArray; //Makes it so I can format what is given from the editor to a 2d array
    public GameController gameController; //Place holder for the gamecontroller object
    public Text subBoard; //the text value of the button \
    
    //The location of the subboard on the screen in relation to the "Array of subboards"
    public int subBoardX; 
    public int subBoardY;

    void Start() {
        formatButtonArray();
        toggleSubBoardInteractble(true);
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void addMove(Button button) {
        //if(gameController.checkCorrectSubBoard(this)) { //Tells if the move can be made in the SubBoard that the button is in
            if (checkEmpty(button.GetComponentInChildren<Text>().text)) { //Makes sure that the button is empty so they dont get over written
                setButtonText(button);
                if (checkEmpty(subBoard.text)) {
                    Debug.Log("The subboard is empty");
                    if (checkSubBoardWin()) {
                        Debug.Log("The Subboard has been won");
                        setSubBoardText();
                        if (gameController.checkGameWin()) {
                            gameController.gameOver();
                        }
                    }
                }
                gameController.EndMove(button.GetComponentInChildren<ButtonController>().buttonX, button.GetComponentInChildren<ButtonController>().buttonY);
            }
            //else gameController.SpaceFilled();//TODO make class to show error
        //}
    }

    //Checks to see if the passed location is empty.
    private bool checkEmpty(string text) {
        if (text == "") {
            return true;
        }
        return false;
    }

    //Iterates over the temp array that is shown in the editor to add them to the 2d array for use in the class
    private void formatButtonArray() {
        buttonArray = new Button[3, 3];
        int i = 0;
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                buttonArray[row, col] = buttonArraytemp[i];
                i++;
            }
        }
    }

    //Iterates over the buttonArray and grabs all of the strings on the text object
    private string[,] makeTextArray() {
        string[,] textArray = new string[3, 3];
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                textArray[row, col] = buttonArray[row, col].GetComponentInChildren<Text>().text;
            }
        }
        return textArray;
    }

    //Checks if the subboard was won on the previous move
    private bool checkSubBoardWin() {
        string[,] textArray = makeTextArray();

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


    //Sets the text of the button
    private void setButtonText(Button button) {
        Debug.Log("setting player move");
        button.GetComponentInChildren<Text>().text = gameController.currentPlayer;
    }


    //Sets the text of the SubBoard
    private void setSubBoardText() {
        subBoard.GetComponentInChildren<Text>().text = gameController.currentPlayer;
    }

    public void toggleSubBoardInteractble(bool toggle) {
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                buttonArray[row, col].interactable = toggle;
            }
        }
    }

    public void clearBoard() {
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {

            }
        }
    }

    public Button[,] getButtonArray() {
        return buttonArray;
    }

    public void RemoveTextFromButtonArray() {
        for (int row = 0; row < 3; row++) {
            for (int col = 0; col < 3; col++) {
                buttonArray[row, col].GetComponentInChildren<Text>().text = "";
            }
        }
    }


}
