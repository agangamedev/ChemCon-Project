using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class G3_ToolsUP : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private GameObject parrent;
    [SerializeField] private Canvas canvas;
    [SerializeField] private EnumToolsType eToolsType;
    [SerializeField] private string targetTag = "DiagramColumn";

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if(gameObject.GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts= false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        if(raycastResult.gameObject.tag == targetTag)
        {
            G3_DiagramColumn col = raycastResult.gameObject.GetComponent<G3_DiagramColumn>();
            switch (eToolsType)
            {
                case EnumToolsType.Up: col.DiagramUpEnable(); break; 
                case EnumToolsType.Down: col.DiagramDownEnable(); break; 
                case EnumToolsType.Delete: col.DisableDiagram(); break; 
            }
        }


        this.transform.position = parrent.transform.position;
        canvasGroup.blocksRaycasts = true;
    }
}

public enum EnumToolsType
{
    Up,
    Down,
    Delete
}