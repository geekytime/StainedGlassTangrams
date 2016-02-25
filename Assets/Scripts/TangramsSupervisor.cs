using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(DragController))]
public class TangramsSupervisor : MonoBehaviour {

    public List<GameObject> puzzlePrefabs;
    public Fader star;
    public List<GameObject> BackdropPrefabs;
    DragController dragController;

    static TangramsSupervisor instance;
    int currentPuzzleIndex = 0;
    GameObject currentPuzzle;
    Backdrop currentBackdrop;

    void Awake(){
        instance = this;
        PlayerPrefs.DeleteAll();
    }

    public static TangramsSupervisor GetInstance(){
        return instance;
    }

    public DragController DragController {
        get {
            return dragController;
        }
    }

	void Start () {
        dragController = GetComponent<DragController>();
        LoadPuzzle(0);
	}               
		
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(worldPoint);
            if (hit != null)
            {                              
                if (hit.name == "next-arrow")
                {
                    if (currentPuzzleIndex + 1 < puzzlePrefabs.Count)
                    {
                        currentPuzzleIndex++;
                        LoadPuzzle(currentPuzzleIndex);
                    }
                } 
                else if (hit.name == "back-arrow")
                {
                    if (currentPuzzleIndex > 0)
                    {
                        currentPuzzleIndex--;
                        LoadPuzzle(currentPuzzleIndex);
                    }
                }

            }
        }
	}

    void LoadPuzzle(int index){                
        Destroy(currentPuzzle);
        currentPuzzle = null;
        var prefab = puzzlePrefabs [index];
        currentPuzzle = Instantiate(prefab);
        var puzzle = currentPuzzle.GetComponentInChildren<Puzzle>();
        SetBackdrop(index);
        SetSolved(puzzle);
    }       

    public void SetSolved(Puzzle puzzle){
        var data = Data.GetInstance();
        var wasEverSolved = data.GetSolved(puzzle.PuzzleName);
        star.IsVisible = wasEverSolved;
        currentBackdrop.IsSolved = puzzle.IsSolved;
    }

    void SetBackdrop(int index){
        if (currentBackdrop != null)
        {
            Destroy(currentBackdrop.gameObject);
        }
        currentBackdrop = Instantiate(BackdropPrefabs [index]).GetComponent<Backdrop>();
    }

//    void ResetPieces(){
//        foreach (var piece in pieces)
//        {
//            piece.Reset();
//        }
//    }
}
