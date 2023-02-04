using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterInteractor : MonoBehaviour
{
    public RuleTile wallTile;
    public Tilemap wallTileMap;

    [SerializeField] float castDistance = 1.0f, blockDestroyTime = 0.0f;
    [SerializeField] LayerMask raycastLayer;

    Vector2 direction;
    RaycastHit2D hit;

    bool destroyingBlock = false, placingBlock = false;

    private void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
        }

    }

    private void FixedUpdate()
    {
        
    }

    private void RaycastDirection()
    {
        hit = Physics2D.Raycast(transform.position, direction, castDistance, raycastLayer.value);
        Debug.DrawLine(transform.position, hit.point);

        if(Input.GetButton("Interact"))
        {
            if(hit.collider && !destroyingBlock)
            {
                destroyingBlock = true;
            }
        }
    }

    IEnumerator DestroyObject(Tilemap )
}
