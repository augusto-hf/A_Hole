using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterBuilder : MonoBehaviour
{
    public RuleTile wallTile, groundTile;
    public Tilemap wallTileMap;

    [SerializeField] float castDistance = 1.0f, blockDestroyTime = 0.0f;
    [SerializeField] LayerMask raycastLayer;

    Vector3 direction;
    Transform raycastPosition;
    RaycastHit2D hit;

    bool destroyingBlock = false, placingBlock = false;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
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
        Vector2 endpos = raycastPosition.position + direction;
        Debug.DrawLine(transform.position, hit.point);

        if (Input.GetButton("Interact"))
        {
            if (hit.collider && !destroyingBlock)
            {
                destroyingBlock = true;
                StartCoroutine(DestroyBlock(hit.collider.gameObject.GetComponent<Tilemap>(), endpos));
            }
        }
    }

    IEnumerator DestroyBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(blockDestroyTime);

        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y), null);

        destroyingBlock = true;
    }

    IEnumerator PlaceBlock(Tilemap map, Vector2 pos)
    {
        yield return new WaitForSeconds(0f);

        pos.x = Mathf.Floor(pos.x);
        pos.y = Mathf.Floor(pos.y);

        map.SetTile(new Vector3Int((int)pos.x, (int)pos.y), wallTile);

        placingBlock = true;
    }
}
