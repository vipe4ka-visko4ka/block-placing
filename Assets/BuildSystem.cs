using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField]
    private Transform hitter;

    [SerializeField]
    private GameObject block;

    GameObject lastHighlighted;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuildBlock();
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
    }

    private void BuildBlock()
    {
        if (Physics.Raycast(hitter.position, hitter.forward, out RaycastHit hit))
        {
            if (hit.transform.tag == "Block") {
                // TODO: how this formula works
                var position = Vector3Int.RoundToInt(hit.point + hit.normal / 2);
                Instantiate(block, position, Quaternion.identity);
            } else {
                var position = Vector3Int.RoundToInt(hit.point);
                Instantiate(block, position, Quaternion.identity);
            }
        }
    }

    private void DestroyBlock()
    {
        if (Physics.Raycast(hitter.position, hitter.forward, out RaycastHit hit))
        {
            if (hit.transform.tag == "Block")
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void HighlightBlock()
    {
        if (Physics.Raycast(hitter.position, hitter.forward, out RaycastHit hit))
        {
            if (hit.transform.tag != "Block")
            {
                if (lastHighlighted) {
                    lastHighlighted.GetComponent<Renderer>().material.color = Color.white;
                    lastHighlighted = null;
                }

                return;
            }

            if (lastHighlighted == null)
            {
                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.grey;
                lastHighlighted = hit.transform.gameObject;
            }
            else if (lastHighlighted != hit.transform.gameObject)
            {
                lastHighlighted.GetComponent<Renderer>().material.color = Color.white;
                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.grey;
                lastHighlighted = hit.transform.gameObject;
            }
        }
    }
}

// TODO:
// hit.point
// hit.transform
// hit.normal
