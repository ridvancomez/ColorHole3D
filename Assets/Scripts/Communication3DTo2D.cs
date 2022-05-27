using UnityEngine;

public class Communication3DTo2D : MonoBehaviour
{
    [SerializeField]
    private PolygonCollider2D hole2DCollider;

    [SerializeField]
    private PolygonCollider2D ground2DCollider;

    [SerializeField]
    private MeshCollider bridgeMeshCollider; // hole2DCollider ýn konumunu bridgeMeshCollider 3D uzayýnda animme edecek

    [SerializeField]
    private float scaleProportion = 0.6f;

    [SerializeField]
    private MeshCollider ground3DMeshCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.hasChanged) // if i yazmak zorunda deðiliz ama bu sefer programa fazlasýyla yük binecek
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.z) * scaleProportion;

            FromTheGroundTakeOutTheHole();
            SyncTheMeshColliderToGroundCollider();
        }
    }
    private void FromTheGroundTakeOutTheHole()
    {
        Vector2[] holePoints = hole2DCollider.GetPath(0);//ilk yolu alýyoruz

        for (int i = 0; i < holePoints.Length; i++)
        {
            holePoints[i] = hole2DCollider.transform.TransformPoint(holePoints[i]); //kara deliðin içindeki polygon colliderýn köþeleriyle birleþtiriyoruz
        }
        ground2DCollider.pathCount = 2; // bu birleþtimeyi 1. indexe yazýp basýyoruz
        ground2DCollider.SetPath(1, holePoints);
    }

    private void SyncTheMeshColliderToGroundCollider() 
    {
        ground3DMeshCollider.sharedMesh = ground2DCollider.CreateMesh(true, true); // polygon dan mesh e geçiþ
    }

}
