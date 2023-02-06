using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterBuilder : MonoBehaviour
{
    //Character Variables
    bool haveWallBreakingTools = true;
    //Tile Variables
    [SerializeField] TileBase  wallTile, selectionTile;
    [SerializeField] Tilemap wallTileMap, selectionTileMap;
    //Raycast Variables
    [SerializeField] float castDistance = 1.0f, blockDestroyTime = 0.0f;
    [SerializeField] LayerMask raycastLayer;

    Vector2 endpos, lastSelectedTile;
    Camera cam;
    Vector3 mousePos, inputDirection;
    Transform raycastPosition;
    RaycastHit2D hit;
    //Command Variables
    bool isScratchingWall = false, isDestroyingBlock = false, isPlacingBlock = false, alternativeSelectMode = false;
    //Other Script Variables
    CharacterController characterController;
    Vector3 lastDirection;

    private void Start()
    {
        raycastPosition = transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Input.GetButton("AlternativeSelection"))
        {
            alternativeSelectMode = true;
        }
        else if(Input.GetButtonUp("AlternativeSelection"))
        {
            alternativeSelectMode = false;
        }
        findDirection();
        RaycastDirection();
        interactWithWall();
    }

    private void findDirection()
    {
        if (alternativeSelectMode)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            lastDirection = -(transform.position - mousePos);
        }
        else
        {
            lastDirection = characterController.directionNormalized();
        }
    }
    private void RaycastDirection()
    {
        hit = Physics2D.Raycast(raycastPosition.position, lastDirection, castDistance, raycastLayer.value);
         
        //debug
        if(hit.point != Vector2.zero)
            Debug.DrawLine(raycastPosition.position, hit.point);

    }
    private void interactWithWall()
    {
        endpos = raycastPosition.position + lastDirection;
        //Selecting
        if (hit.collider && !isDestroyingBlock)
        {
            Vector2 ExactEndPos = new Vector2(Mathf.Floor(endpos.x), Mathf.Floor(endpos.y));
            if (lastSelectedTile != ExactEndPos)
            {
                StartCoroutine(DestroyBlock(selectionTileMap, lastSelectedTile));
                lastSelectedTile = ExactEndPos;
            }         
            PlaceBlock(selectionTileMap, lastSelectedTile, selectionTile);
        }
        else
        {
            StartCoroutine(DestroyBlock(selectionTileMap, lastSelectedTile));
        }
        //Pressing Commands
        if (Input.GetButton("Interact"))
        {
            if (hit.collider && !isDestroyingBlock)
            {
                if (haveWallBreakingTools)
                {
                    isDestroyingBlock = true;
                    StartCoroutine(DestroyBlock(wallTileMap, endpos));
                }
                else if (!haveWallBreakingTools && isScratchingWall)
                {
                    
                    StartCoroutine(ScratchWall(wallTileMap, endpos));
                }
            }
        }
    }
    IEnumerator ScratchWall(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        Tile tileToScratch;

        //if (tileToScratch == map.GetTile<Tile>(new Vector3Int((int)pos.x, (int)pos.y), null))
        //{

        //}


        isScratchingWall = false;
    }
    IEnumerator DestroyBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y), null);

        isDestroyingBlock = false;
    }

    void PlaceBlock(Tilemap map, Vector2 pos, TileBase tile)
    {
        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y), tile);

        isPlacingBlock = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, lastDirection * castDistance);  
    }
}
