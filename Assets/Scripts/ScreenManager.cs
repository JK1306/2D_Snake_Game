using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    static ScreenManager screenManagerInstance;
    Vector2 minCoordinate,
            maxCoordinate;
    float mapX = 100,
            mapY = 100;
    public static ScreenManager instance{
        get{
            return screenManagerInstance;
        }
    }
    void Start()
    {
        float vertExtent = Camera.main.orthographicSize;    
        float horzExtent = vertExtent * Screen.width / Screen.height;
 
        minCoordinate = new Vector2(
            (float)(horzExtent - mapX / 2.0),
            (float)(vertExtent - mapY / 2.0)
        );
        maxCoordinate = new Vector2(
            (float)(mapX / 2.0 - horzExtent),
            (float)(mapY / 2.0 - vertExtent)
        );

        Debug.Log("Minimum Coordinates : "+minCoordinate);
        Debug.Log("Maximum Coordinates : "+maxCoordinate);

        if( screenManagerInstance == null){
            screenManagerInstance = this;
        }else{
            Destroy(this);
        }
    }
}
