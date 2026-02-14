using UnityEngine;

public class Shot : MonoBehaviour
{
public float timeToLive = 4.5f;  

private float _expireTime = 0f;
    public void Update()
    {
        _expireTime += Time.deltaTime;
        if (_expireTime >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

}
