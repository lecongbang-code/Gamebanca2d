using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Player player;

    [HideInInspector]
    public bool Pressed; 
    [HideInInspector]
    public bool Setting = false;

    public void OnSetting()
    {
        Setting = true;
    }

    public void OffSetting()
    {
        Setting = false;
    }

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(Pressed && !Setting)
        {
            player.CallShoot();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}