using UnityEngine;

public class Collectable : MonoBehaviour
{
	[SerializeField] int valueMin;
    [SerializeField] int valueMax;
	public int value;

	[SerializeField] float weightMin;
	[SerializeField] float weightMax;
	public float weight;

    void Start()
    {
        value = Random.Range(valueMin, valueMax);
		weight = Random.Range(weightMin, weightMax);
    }

	public void Collect()
	{
		Destroy(this.gameObject);
	}
}
