using UnityEngine;

public class ParticleSystemTracking : MonoBehaviour
{
    public Transform camera;
    public float Hleeway;
    public float Vleeway;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(camera.position.x, camera.position.y, -5);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (((transform.position.x) - (camera.position.x)) > Hleeway)
        {
            transform.position = new Vector3(transform.position.x - ((transform.position.x - camera.position.x) - Hleeway), transform.position.y, -5);
        }
        if (((transform.position.x) - (camera.position.x)) < -Hleeway)
        {
            transform.position = new Vector3(transform.position.x - ((transform.position.x - camera.position.x) + Hleeway), transform.position.y, -5);
        }
        if (((transform.position.y) - (camera.position.y)) > Vleeway)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - ((transform.position.y - camera.position.y) - Vleeway), -5);
        }
        if (((transform.position.y) - (camera.position.y)) < -Vleeway)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - ((transform.position.y - camera.position.y) + Vleeway), -5);
        }
        //transform.position = new Vector3(camera.position.x, camera.position.y, -5);
    }
}
