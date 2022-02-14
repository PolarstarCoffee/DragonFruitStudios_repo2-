using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;




public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }


    //a type Row array named rows, used to accsess each row of the board, inside the Row array a Tiles array holds each tile in the row
    public Row[] rows;

    //a type Tile array named Tiles, has two parameters (x and y), and when called, will run in Tile.cs b/c it is get; private set 
    public Tile[,] Tiles { get; private set; }

    public int Width => Tiles.GetLength(dimension: 0);
    public int Height => Tiles.GetLength(dimension: 1);

    private readonly List<Tile> _Selection = new();

    private const float TweenDuration = 0.25f;
    private void Awake() => Instance = this;

    public int moveCount = 0; //keeps a count of the moves made, a move is when a swap happens AND a match is made

    public int moveLimit = 4; // test int var for ending the game when the moveLimit is reached, is checked for after every match.

    //gets the Type of Item that was matched
    public int matchType;
    //getting the amount of tiles matched for the ObjectiveCounter.cs
    public int matchAmount;

    //making a counter to count what level the player is on in order to place tiles correctly and update the objective in the Obj Counter script
    public int levelIndex = 2;


    private void Start()
    {
        //creating width and length of the board, storing it in the Tiles array from 0,0 to 4,4
        Tiles = new Tile[rows.Max(selector: row => row.tiles.Length), rows.Length];


        for (var y = 0; y < Height; y++)
        {

            for (var x = 0; x < Width; x++)
            {

                //tile at this x,y will be equal to the tile inside the row at y index and tile index of x
                var tile = rows[y].tiles[x];
                tile.x = x;
                tile.y = y;


                Tiles[x, y] = tile;

                //determining which item is in each tile by setting the tile.item to the item_database random pick of all items inside (2/12/22 Edit: Excluded obstacle tile from spawning randomly)
                tile.Item = Item_Database.Items[Random.Range(0, 4)];

                //ADD CANNOT SPAWN MATCHES AT START

                //try implemementing specific placement of tiles
                //set the bottom row to all triangles (obstacles tile.item.type = 4)
                /*if (rows[y] = rows[4])
                {
                    rows[4].tiles[x].Item.type = 4;
                }*/

            }
        }

        //This works to put the specific item into a tile
        //rows[0].tiles[0].Item = Item_Database.Items[4];
        //rows[0].tiles[1].Item = Item_Database.Items[4];
        //rows[0].tiles[2].Item = Item_Database.Items[4];
        //rows[0].tiles[3].Item = Item_Database.Items[4];

        //checking what levelIndex is to place the tiles correctly.
        /*if (levelIndex == 0)
        {
            //place tiles for level One
            //All tiles are random anyway so stays the same
        }*/
        if (levelIndex == 2)
        {
            //palce tiles for level two
            //obstacles
            rows[1].tiles[0].Item = Item_Database.Items[4];
            rows[1].tiles[4].Item = Item_Database.Items[4];
            rows[2].tiles[0].Item = Item_Database.Items[4];
            rows[2].tiles[1].Item = Item_Database.Items[4];
            rows[2].tiles[3].Item = Item_Database.Items[4];
            rows[2].tiles[4].Item = Item_Database.Items[4];
        }
        else if (levelIndex == 3)
        {
            //place tiles for level three
            //obstacles
            rows[1].tiles[1].Item = Item_Database.Items[4];
            rows[1].tiles[2].Item = Item_Database.Items[4];
            rows[1].tiles[3].Item = Item_Database.Items[4];
            rows[1].tiles[4].Item = Item_Database.Items[4];
            

            rows[4].tiles[1].Item = Item_Database.Items[4];
            rows[4].tiles[2].Item = Item_Database.Items[4];
            rows[4].tiles[3].Item = Item_Database.Items[4];
            rows[4].tiles[4].Item = Item_Database.Items[4];
            

        }
        else if (levelIndex == 4)
        {
            //place tiles for level four
            //obstacles
            rows[4].tiles[2].Item = Item_Database.Items[4];
            rows[4].tiles[3].Item = Item_Database.Items[4];

            rows[7].tiles[0].Item = Item_Database.Items[4];
            rows[7].tiles[1].Item = Item_Database.Items[4];
            rows[7].tiles[2].Item = Item_Database.Items[4];
            rows[7].tiles[3].Item = Item_Database.Items[4];
            rows[7].tiles[4].Item = Item_Database.Items[4];
            rows[7].tiles[5].Item = Item_Database.Items[4];

            rows[8].tiles[0].Item = Item_Database.Items[4];
            rows[8].tiles[1].Item = Item_Database.Items[4];
            rows[8].tiles[2].Item = Item_Database.Items[4];
            rows[8].tiles[3].Item = Item_Database.Items[4];
            rows[8].tiles[4].Item = Item_Database.Items[4];
            rows[8].tiles[5].Item = Item_Database.Items[4];

            rows[9].tiles[0].Item = Item_Database.Items[4];
            rows[9].tiles[1].Item = Item_Database.Items[4];
            rows[9].tiles[2].Item = Item_Database.Items[4];
            rows[9].tiles[3].Item = Item_Database.Items[4];
            rows[9].tiles[4].Item = Item_Database.Items[4];
            rows[9].tiles[5].Item = Item_Database.Items[4];
        }
        else if (levelIndex == 5)
        {
            //place tiles for level five

            //icons
            rows[0].tiles[0].Item = Item_Database.Items[0];
            rows[0].tiles[1].Item = Item_Database.Items[1];
            rows[0].tiles[2].Item = Item_Database.Items[1];

            rows[1].tiles[0].Item = Item_Database.Items[1];
            rows[1].tiles[1].Item = Item_Database.Items[3];
            rows[1].tiles[2].Item = Item_Database.Items[3];

            rows[2].tiles[0].Item = Item_Database.Items[3];
            rows[2].tiles[1].Item = Item_Database.Items[0];
            rows[2].tiles[2].Item = Item_Database.Items[0];

            rows[3].tiles[0].Item = Item_Database.Items[0];
            rows[3].tiles[1].Item = Item_Database.Items[1];
            rows[3].tiles[2].Item = Item_Database.Items[1];

            rows[4].tiles[0].Item = Item_Database.Items[3];
            rows[4].tiles[1].Item = Item_Database.Items[0];
            rows[4].tiles[2].Item = Item_Database.Items[3];
            rows[4].tiles[3].Item = Item_Database.Items[3];

            rows[5].tiles[0].Item = Item_Database.Items[1];
            rows[5].tiles[1].Item = Item_Database.Items[3];
            rows[5].tiles[2].Item = Item_Database.Items[0];
            rows[5].tiles[3].Item = Item_Database.Items[0];


            //obstacles
            rows[0].tiles[3].Item = Item_Database.Items[4];
            rows[1].tiles[3].Item = Item_Database.Items[4];
            rows[2].tiles[3].Item = Item_Database.Items[4];
            rows[3].tiles[3].Item = Item_Database.Items[4];
            rows[4].tiles[4].Item = Item_Database.Items[4];
            rows[5].tiles[4].Item = Item_Database.Items[4];
        }


        //Add one to the level index to determine what level layout is placed on the grid
        //levelIndex += 1;
        //EDIT just change the levelIndex in the hierarchy on the Board board script in inspector
    }


    

    private void Update()
    {
        //if (!Input.GetKeyDown(KeyCode.A)) return;

        //foreach (var connectedTile in Tiles[0, 0].GetConnectedTiles()) connectedTile.icon.transform.DOScale(1.25f, TweenDuration).Play();
    }

    //class for selecting two tiles
    public async void Select(Tile tile)
    {
        //ADD TIMER TO WAIT FOR THE TILES TO FULLY SWAP UNTILL PLAYER CAN SELECT TILES AGAIN

        //if( !_Selection.Contains(tile))_Selection.Add(tile);
        //Do not let player select a obstacle (tile.Item.type = 4)
        if( !_Selection.Contains(tile) && tile.Item.type != 4)
        {
            //only choose another tile neighboring the first chosen tile
            if (_Selection.Count > 0)
            {
                //unity does not have Array, need to use System.Array... or list.IndexOf
                //making sure the index of the _Selection[0] is not -1
                if (System.Array.IndexOf(_Selection[0].Neighbours, tile) != -1)
                {
                    _Selection.Add(tile);
                }
                else
                {
                    _Selection.Clear();//clear selection, WORKS kindof, after selecting a non-neighboring tile the selection is cleared and a new tile has to be selected
                    //ADD color to which tile is selected
                }
                //else to reset the player's tile selection when they click on any tile not in _Seleciton[0].Neighbours
            }
            else
            {
                _Selection.Add(tile);
            }
        }

        if (_Selection.Count < 2) return;

        Debug.Log($"Selected 2 Tiles @ ({_Selection[0].x}, {_Selection[0].y}) and ({_Selection[1].x}, {_Selection[1].y}) ");

        await Swap(_Selection[0], _Selection[1]);


        if (CanPop())  //Checks to see if it can be "popped" 
        {
            Pop();

            moveCount += 1; //adds one to the moveCount var

            if (moveCount >= moveLimit) //check if player's moveCount is greater than the moveLimit, if so end the level 
            {
                Debug.Log("moveCount has reached moveLimit"); //implement a scene index, sending the player to a end/retry scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Boots player to end scene on moveCount Limit reached (EDIT 2/12/22: Moved onto next scene in build index.)
            }
           
        }
        else
        {
            await Swap(_Selection[0], _Selection[1]); //Swaps back if no match is made 
            //clear the _Selection list
            _Selection.Clear();
            
        }

       

        _Selection.Clear();


    }



    public async Task Swap(Tile tile1, Tile tile2) //Method that implements the tile "swap" animation
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;




        var sequence = DOTween.Sequence();


        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

        await sequence.Play().AsyncWaitForCompletion();


        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;


            
    }

    private bool CanPop() //Checks to see if it's a match 
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                //adding an if check to make sure the connected tiles are not Type int value 4 (obstacle), EDIT only working when a match of 3 
                //triangles is trying to be swapped, is a match of more than 4 is made it will still swap AND if a triangle is spawned to create a 3
                //match it will still match
                if (Tiles[x, y].Item.type != 4)
                {
                    if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2) // && Tiles[x, y].Item.type != 4) //More than or equal to two equals a match: Will change later  
                        return true;
                }
                
        return false;
      
    }
   private async void Pop() //Removes icons from grid when matched 
    {
       for ( var y = 0; y < Height; y++ ) 
        {
            for ( var x = 0;x < Width; x++ )
            {
                //check to make sure the matched are not of item.type = 4 (obstacle)
                if (Tiles[x, y].Item.type != 4)
                {
                    var tile = Tiles[x, y]; //Tiles variable to use in THIS method 

                    var connectedTiles = tile.GetConnectedTiles(); //local variable for passing along GetConnectedTiles method 

                    //IF obstacle is adjacenet to a match, remove it and add to the obstacle objective counter

                    var connectedObstacleTiles = tile.GetObstacleTiles();
                    //Debug.Log(connectedObstacleTiles.ToString());
                    
                    if (connectedTiles.Skip(1).Count() < 2) continue; //If 3 tiles aren't connected, continue 

                    //making the connectedObstacleTiles be removed 
                    

                    var deflateSequence = DOTween.Sequence(); //Intialize pop sequence 

                    foreach (var connectedTile in connectedTiles) deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration)); //Pop sequence using DOTween

                    //making connectedObstableTiles also animate
                    foreach (var connectedObstacleTile in connectedObstacleTiles) deflateSequence.Join(connectedObstacleTile.icon.transform.DOScale(Vector3.zero, TweenDuration)); 

                    await deflateSequence.Play().AsyncWaitForCompletion();  //Waits until it's finished playing until doing it over again. 

                    //add to the score
                    Score_Script.instance.Score += tile.Item.value * connectedTiles.Count;

                    //get the Item Type which the connected tiles are
                    matchType = tile.Item.type;

                    
                    //Debug.Log(matchType + "is the Item matched");

                    //getting the amount of tiles matched
                    matchAmount = connectedTiles.Count;

                    //TESTING DROPPING TILES DOWN
                        //save the matched tiles x,y position as spawnTile and move it upto where the x,y will be the only empty space above



                    var inflateSequence = DOTween.Sequence();
                    foreach (var connectedTile in connectedTiles) //for each connected tile within our Pop method 
                    {
                        connectedTile.Item = Item_Database.Items[Random.Range(0, 4)]; // Item_Database.Items.Length)]; //repopulates the grid after a "pop"

                        inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration)); //actual code to animate in the repopulation utlilziing DOTween 
                    }

                    //for obstacles to repopulate with tiles
                    foreach (var connectedObstacleTile in connectedObstacleTiles) //for each connected tile within our Pop method 
                    {
                        connectedObstacleTile.Item = Item_Database.Items[Random.Range(0, 4)]; // Item_Database.Items.Length)]; //repopulates the grid after a "pop"

                        inflateSequence.Join(connectedObstacleTile.icon.transform.DOScale(Vector3.one, TweenDuration)); //actual code to animate in the repopulation utlilziing DOTween 
                    }

                    await inflateSequence.Play().AsyncWaitForCompletion();


                    x = 0;
                    y = 0;  //Reset
                }
                 
            }
        } 

    }

} 





      