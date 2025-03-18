using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public Manager udpReceive;
    public GameObject[] handPoints;

    public GameObject WholeHand;




    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        data = data.Remove(0, 1);
        data = data.Remove(data.Length-1, 1);


        string[] points = data.Split(',');




        float x0 = float.Parse(points[5 * 3 + 0]) - float.Parse(points[17 * 3 + 0]);
        float y0 = float.Parse(points[5 * 3 + 1]) - float.Parse(points[17 * 3 + 1]);
        float z0 = float.Parse(points[5 * 3 + 2]) - float.Parse(points[17 * 3 + 2]);

        float width_pix = Mathf.Sqrt(Mathf.Pow(x0, 2) + Mathf.Pow(y0, 2) + Mathf.Pow(z0, 2));





        // float z_offset = 20 - (width_pix / 10);
        // float hand_size = 200/width_pix;

        // middle_width = 150, z = 0
        // max_width = 550, z = 20

        // Slope = (z1 - z2) / (x1 - x2)
        // Slope = (0 - 20) / (150 - 550) = 0.05
        // b = 150 * 0.05 + b = 0 <=> b = 7.5

        float z_hand = 0.05f * width_pix + 7.5f;



        Debug.Log(("hand size in pix: " + width_pix));

        float hand_size = 200 / width_pix;

        WholeHand.transform.localScale = new Vector3(hand_size, hand_size, hand_size);
        WholeHand.transform.position = new Vector3(0, 0, z_hand);




        for ( int i = 0; i<21; i++)
        {

            float x = 7 - (float.Parse(points[i * 3]) / 100);
            float y = float.Parse(points[i * 3 + 1]) / 100;
            float z = float.Parse(points[i * 3 + 2]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
            handPoints[i].transform.localScale = (new Vector3(0.5f / hand_size , 0.5f/ hand_size , 0.5f/ hand_size));


        }


    }
}