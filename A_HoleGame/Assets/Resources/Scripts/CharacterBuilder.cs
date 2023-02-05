using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterBuilder : MonoBehaviour
{
    public Tile wallTile, selectionTile;
    public Tilemap wallTileMap, selectionTileMap;
    Collider2D lastSelectedTile;

    //Raycast Variables
    [SerializeField] float castDistance = 1.0f, blockDestroyTime = 0.0f;
    [SerializeField] LayerMask raycastLayer;
    
    Camera cam;
    Vector3 lastDirection, mousePos, inputDirection;
    Transform raycastPosition;
    RaycastHit2D hit;

    bool destroyingBlock = false, placingBlock = false, alternativeSelectMode = false;

    private void Start()
    {
        raycastPosition = transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                
                if (Input.GetAxisRaw("Vertical") == 0)
                {
                    lastDirection.x = Input.GetAxisRaw("Horizontal");
                    lastDirection.y = 0;
                }
                else if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    lastDirection.y = Input.GetAxisRaw("Vertical");
                    lastDirection.x = 0;
                }
                else
                {
                    lastDirection.x = Input.GetAxisRaw("Horizontal");
                    lastDirection.y = Input.GetAxisRaw("Vertical");
                }
                lastDirection.Normalize();             
            }
        }
    }
        
    private void RaycastDirection()
    {
        hit = Physics2D.Raycast(transform.position, lastDirection, castDistance, raycastLayer.value);
         
        //debug
        if(hit.point != Vector2.zero)
            Debug.DrawLine(raycastPosition.position, hit.point);

    }

    private void interactWithWall()
    {
        Vector2 endpos = raycastPosition.position + lastDirection;
        //Selecting
        if (hit.collider && !destroyingBlock)
        {
            lastSelectedTile = hit.collider;
            PlaceBlock(selectionTileMap, endpos, selectionTile);
        }
        if (lastSelectedTile != null && lastSelectedTile != hit.collider)
        {
            Debug.Log("Agora faz o L");
            DestroyBlock(selectionTileMap, lastSelectedTile.transform.position);
            lastSelectedTile = null;
        }

        //Pressing Commands
        if (Input.GetButton("Interact"))
        {
            if (hit.collider && !destroyingBlock)
            {
                Debug.Log("tentei destruir");
                //Debug.Log("Agora faz o L");
                destroyingBlock = true;
                StartCoroutine(DestroyBlock(wallTileMap, endpos));
            }
        }
    }

    IEnumerator DestroyBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y), null);

        destroyingBlock = false;
    }

    void PlaceBlock(Tilemap map, Vector2 pos, Tile tile)
    {

        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y), tile);

        placingBlock = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, lastDirection * castDistance);  
    }
}
