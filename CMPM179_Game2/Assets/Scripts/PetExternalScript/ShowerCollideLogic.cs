using UnityEngine;

public class ShowerCollideLogic : MonoBehaviour
{
    public ParticleSystem water;
    public PetStatusManager petStatusManager;
    public Canvas canvas;
    public Vector3 positionDif;
    public GameObject showerHead;
    private RectTransform uiObject;
    private void Awake()
    {
       // GameManager.Instance.startWater = false;
        positionDif = water.transform.position - showerHead.transform.position;
        if(showerHead)
        uiObject = showerHead.GetComponent<RectTransform>();
    }
    void FixedUpdate()
    {

        if (GameManager.Instance.startWater) water.Play();
        else water.Stop();

        //moves spray with the shower head itself
        
    }
    private void Update()
    {
        if (uiObject && water)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(uiObject.position);
            water.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        UnityEngine.Debug.Log("Particle collided with " + other.name);
        petStatusManager.ShowerPet(5f);
        // Additional logic can go here, like reducing health, triggering effects, etc.
    }
}
