using UnityEngine ;

[System.Serializable]
public class WheelPiece {
   public UnityEngine.Sprite Icon ;
   public string Label ;

   [Tooltip ("Reward amount")] public int Amount ;
   [HideInInspector] public int Index ;
   [HideInInspector] public double _weight = 0f ;
}