using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Processes mouse button inputs
/// </summary>
public class MouseButtonProcessor : MonoBehaviour
{
    [SerializeField]
    GameObject prefabExplosion;
    [SerializeField]
    GameObject prefabTeddyBear;

    // first frame input support
    bool spawnInputOnPreviousFrame = false;
	bool explodeInputOnPreviousFrame = false;

    // only trigger action on initial mouse button down trigger
    bool isLeftMouseButtonDown = false;
    bool isRightMouseButtonDown = false;

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // spawn teddy bear as appropriate
        if (Input.GetAxis("SpawnTeddyBear") > 0)
        {
            if (!isLeftMouseButtonDown)
            {
                isLeftMouseButtonDown = true;

                GameObject teddyBear;

                Vector3 mouseScreenPosition = Input.mousePosition;
                mouseScreenPosition.z = -Camera.main.transform.position.z;

                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
                    mouseScreenPosition
                );

                teddyBear = Instantiate<GameObject>(
                    prefabTeddyBear,
                    mouseWorldPosition,
                    Quaternion.identity
                );
            }
        } else
        {
            isLeftMouseButtonDown = false;
        }

        // explode teddy bear as appropriate
        if (Input.GetAxis("ExplodeTeddyBear") > 0)
        {
            if (!isRightMouseButtonDown)
            {
                isRightMouseButtonDown = true;

                GameObject[] allTeddyBears = GameObject.FindGameObjectsWithTag("TeddyBear");

                if (allTeddyBears.Length > 0)
                {
                    int randomTeddyBearIndex = Random.Range(0, allTeddyBears.Length);

                    GameObject randomTeddyBear = allTeddyBears[randomTeddyBearIndex];
                    GameObject explosion = Instantiate<GameObject>(
                        prefabExplosion,
                        randomTeddyBear.transform.position,
                        Quaternion.identity
                    );

                    Destroy(randomTeddyBear);
                }

            }
        } else
        {
            isRightMouseButtonDown = false;
        }
		
	}
}
