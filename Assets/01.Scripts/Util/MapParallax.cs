using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapParallax : MonoBehaviour
{
    private float lengthX, lengthY, startposX, startposY;
    private GameObject cam;
    public float parallaxEffect;

    private void FixedUpdate()
    {
        SetBackground();
    }

    public void InitParallax()
    {
        cam = Camera.main.gameObject;
        SetPositionAndLength();
    }

    private void SetPositionAndLength()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        lengthX = tilemap.cellBounds.size.x * tilemap.cellSize.x;
        lengthY = tilemap.cellBounds.size.y * tilemap.cellSize.y;
    }

    private void SetBackground()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float distanceX = (cam.transform.position.x * parallaxEffect);
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));
        float distanceY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startposX + distanceX, startposY + distanceY, transform.position.z);
        
        if (tempX > startposX + lengthX / 2)
        {
            startposX += lengthX;
        }
        else if (tempX < startposX - lengthX / 2)
        {
            startposX -= lengthX;
        }

        if (tempY > startposY + lengthY / 2)
        {
            startposY += lengthY;
        }
        else if (tempY < startposY - lengthY / 2)
        {
            startposY -= lengthY;
        }


    }
}
