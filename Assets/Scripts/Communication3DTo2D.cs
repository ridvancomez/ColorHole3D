using UnityEngine;

public class Communication3DTo2D : MonoBehaviour
{
    [SerializeField]
    private PolygonCollider2D hole2DCollider;

    [SerializeField]
    private PolygonCollider2D ground2DCollider;

    [SerializeField]
    private MeshCollider bridgeMeshCollider; // hole2DCollider �n konumunu bridgeMeshCollider 3D uzay�nda animme edecek

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
        if (transform.hasChanged) // if i yazmak zorunda de�iliz ama bu sefer programa fazlas�yla y�k binecek
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
        Vector2[] holePoints = hole2DCollider.GetPath(0);//ilk yolu al�yoruz

        for (int i = 0; i < holePoints.Length; i++)
        {
            holePoints[i] = hole2DCollider.transform.TransformPoint(holePoints[i]); //kara deli�in i�indeki polygon collider�n k��eleriyle birle�tiriyoruz
        }
        ground2DCollider.pathCount = 2; // bu birle�timeyi 1. indexe yaz�p bas�yoruz
        ground2DCollider.SetPath(1, holePoints);
    }

    private void SyncTheMeshColliderToGroundCollider() 
    {
        ground3DMeshCollider.sharedMesh = ground2DCollider.CreateMesh(true, true); // polygon dan mesh e ge�i�
    }

}
