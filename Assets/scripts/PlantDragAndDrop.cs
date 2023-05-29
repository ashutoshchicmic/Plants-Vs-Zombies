using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject gridData;
    public GameObject plantPrefab; 
    public GameObject cg;
    public int reqPoints;

    Vector2 startPosition;
    Transform startParent;
    List<Image> imageList = new List<Image>();
    gridListData gList;
    coinsGenerator coinGen;
    plantsCount plantCnt;
    CanvasGroup canvasGroup;
    bool dragged = false;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        coinGen = cg.GetComponent<coinsGenerator>();
        plantCnt = FindAnyObjectByType<plantsCount>();
        gList = gridData.GetComponent<gridListData>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.parent.parent); // This ensures that the plant is always on top of other objects while being dragged
        canvasGroup.blocksRaycasts = false; // This disables the raycast on the plant's collider so that it doesn't block mouse clicks
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("GridSquare"))
        {
            Image im = hit.collider.GetComponent<Image>();
            im.enabled = true;
            imageList.Add(im);
        }
        else
        {
            foreach (Image im in imageList)
            {
                im.enabled = false;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(startParent); // This sets the plant back to its original parent
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // This enables the raycast on the plant's collider again
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("GridSquare"))
        {
            RectTransform gridRectTransform = hit.collider.GetComponent<RectTransform>();
            Vector3 gridCenter = new Vector3(gridRectTransform.rect.center.x, gridRectTransform.rect.center.y, 0f);
            gridCenter += gridRectTransform.transform.position;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(gridCenter); // Convert the mouse position to world position
            worldPos.z = 0f;
            GameObject plantObject = Instantiate(plantPrefab, worldPos, Quaternion.identity); // Instantiate the plant prefab at the grid center position
            plantObject.transform.position = worldPos;
            Vector2 plantPos = plantObject.transform.position;
            Vector2Int plantPosInt= new Vector2Int(Mathf.RoundToInt(plantPos.x), Mathf.RoundToInt(plantPos.y));
            plantObject.SetActive(false);
            print("plant pos in plant drag and drop: " + plantPosInt);
            if (!gList.occupiedPositions.Contains(plantPosInt) && coinGen.points>=reqPoints)
            {
                print("point " + coinGen.points);
                gridData.GetComponent<gridListData>().occupiedPositions.Add(plantPosInt);
                coinGen.decreasePoints(reqPoints);
                plantObject.SetActive(true);
                plantCnt.incPlant();
                canvasGroup.blocksRaycasts = false;
                StartCoroutine(restartRaycast(canvasGroup));
            }
            else
            {
                Destroy(plantObject);
                Debug.Log("Grid already occupied!");
            }
        }
        else
        {
            transform.position = startPosition; // This puts the plant back to its original position if it's dropped outside of the grid
        }
        foreach (Image im in imageList)
        {
            im.enabled = false;
        }
        transform.position = startPosition;
    }
    private void OnDestroy()
    {
        gridData.GetComponent<gridListData>().occupiedPositions.Clear();
    }

    IEnumerator restartRaycast(CanvasGroup canvasGroup)
    {
        dragged = true;
        Image image = canvasGroup.gameObject.GetComponent<Image>();
        Color color = image.color;
        color.a = 0.5f;
        image.color = color;
        yield return new WaitForSeconds(5f);
        canvasGroup.blocksRaycasts = true;
        color.a = 1f;
        image.color = color;
        dragged = false;
    }

    private void Update()
    {
        if (coinGen.points >= reqPoints && !dragged)
        {
            Image image = GetComponent<Image>();
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
        else
        {
            Image image = GetComponent<Image>();
            Color color = image.color;
            color.a = 0.5f;
            image.color = color;
        }
    }
}