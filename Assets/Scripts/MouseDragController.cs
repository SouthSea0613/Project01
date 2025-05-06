using System.Collections.Generic;
using UnityEngine;

public class MouseDragController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AppleSpawner appleSpawner;
    [SerializeField]
    private RectTransform dragRectangle;

    private Rect dragRect;
    private Vector2 start = Vector2.zero;
    private Vector2 end = Vector2.zero;
    private int sum = 0;
    private List<Apple> selectedAppleList = new List<Apple>();

    private void Awake()
    {
        dragRect = new Rect();

        DrawDragRectangle();
    }

    private void Update()
    {
        if (gameController.IsGameStart == false)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            start = Input.mousePosition;

            dragRect.Set(0, 0, 0, 0);
        }

        if (Input.GetMouseButton(0))
        {
            end = Input.mousePosition;

            DrawDragRectangle();
            CalculateDragRect();
            SelectApples();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (sum == 10)
            {
                foreach (Apple apple in selectedAppleList)
                {
                    appleSpawner.DestroyApple(apple);
                }
            }
            else
            {
                foreach (Apple apple in selectedAppleList)
                {
                    apple.OnDeselected();
                }
            }

                start = end = Vector2.zero;
            DrawDragRectangle();
        }
    }

    private void DrawDragRectangle()
    {
        dragRectangle.position = (start + end) * 0.5f;
        dragRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
    }

    private void CalculateDragRect()
    {
        if (Input.mousePosition.x < start.x)
        {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else
        {
            dragRect.xMin = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < start.y)
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }

    private void SelectApples()
    {
        sum = 0;
        selectedAppleList.Clear();

        foreach (Apple apple in appleSpawner.AppleList)
        {
            if (dragRect.Contains(apple.Position))
            {
                apple.OnSelected();
                selectedAppleList.Add(apple);
                sum += apple.Number;
            }
            else
            {
                apple.OnDeselected();
            }
        }
    }
}
