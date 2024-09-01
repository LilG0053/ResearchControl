using UnityEngine;

public class displayObject : MonoBehaviour
{


    [Header("Images")]
    [SerializeField] private GameObject[] images;
    [SerializeField] private GameObject whiteImage;

    [Header("Choose a magnitute of Movement")]
    [SerializeField] private int movementMagnitute = 10;
    [Header("Choose a magnitute of scale")]
    [SerializeField] private float scaleMagnitute = 1.5f;

    private Vector3 changeVector;
    private int imageIndex;
    private bool isFlashing;
    

    void Start()
    {
        imageIndex = 0;
        isFlashing = false;
        for (int i = 1; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }
        images[0].SetActive(true);
        whiteImage.SetActive(false);

    }


    void Update()
    {
        flash();
    }


    public void moveRight()
    {
        changeVector = new Vector3(0.05f, 0, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void moveLeft()
    {
        changeVector = new Vector3(-0.05f, 0, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void moveUp()
    {
        changeVector = new Vector3(0, 0.05f, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void moveDown()
    {
        changeVector = new Vector3(0, -0.051f, 0);
        gameObject.transform.position += movementMagnitute * changeVector;
    }
    public void scaleUp()
    {
        gameObject.transform.localScale *= scaleMagnitute;
    }
    public void scaleDown()
    {
        gameObject.transform.localScale /= scaleMagnitute;
    }

    public void toggleFlashing()
    {
        if (!isFlashing)
        {
            images[imageIndex].SetActive(false);
            whiteImage.SetActive(true);
            isFlashing = true;
        }
        else
        {
            images[imageIndex].SetActive(true);
            whiteImage.SetActive(false);
            isFlashing = false;
        }
    }

    private void flash()
    {
        if (isFlashing)
        {
            whiteImage.SetActive(!whiteImage.activeSelf);
        }
    }
    public void nextImage()
    {
        if (imageIndex + 1 >= images.Length)
        {
            Debug.Log("No more images");
        }
        else
        {
            images[imageIndex].SetActive(false);
            imageIndex++;
            images[imageIndex].SetActive(true);
        }

    }
    public void previousImage()
    {
        if (imageIndex - 1 < 0)
        {
            Debug.Log("No more images");
        }
        else
        {
            images[imageIndex].SetActive(false);
            imageIndex--;
            images[imageIndex].SetActive(true);
        }

    }
    
}
