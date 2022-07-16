using UnityEngine;
using UnityEngine.UI;

public class PickerWheel : MonoBehaviour {

   [Header ("References :")]
   [SerializeField] private GameObject linePrefab = null;
   [SerializeField] private Transform linesParent = null;

   [Space]
   [SerializeField] private Transform PickerWheelTransform ;
   [SerializeField] private Transform wheelCircle ;
   [SerializeField] private GameObject wheelPiecePrefab ;
   [SerializeField] private Transform wheelPiecesParent = null;
   
   [Space]
   [Header ("Picker wheel pieces :")]
   public WheelPiece[] wheelPieces ;

   private Vector2 pieceMinSize = new Vector2 (81f, 146f) ;
   private Vector2 pieceMaxSize = new Vector2 (144f, 213f) ;
   private int piecesMin = 2 ;
   private int piecesMax = 12 ;

   private float pieceAngle ;
   private float halfPieceAngle ;
   private float halfPieceAngleWithPaddings ;

   private void Start () {
      pieceAngle = 360 / wheelPieces.Length ;
      halfPieceAngle = pieceAngle / 2f ;
      halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 4f) ;
      Generate () ;
   }

   private void Generate () {
      wheelPiecePrefab = InstantiatePiece () ;

      RectTransform rt = wheelPiecePrefab.transform.GetChild (0).GetComponent <RectTransform> () ;
      float pieceWidth = Mathf.Lerp (pieceMinSize.x, pieceMaxSize.x, 1f - Mathf.InverseLerp (piecesMin, piecesMax, wheelPieces.Length)) ;
      float pieceHeight = Mathf.Lerp (pieceMinSize.y, pieceMaxSize.y, 1f - Mathf.InverseLerp (piecesMin, piecesMax, wheelPieces.Length)) ;
      rt.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, pieceWidth) ;
      rt.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, pieceHeight) ;

      for (int i = 0; i < wheelPieces.Length; i++)
         DrawPiece (i) ;

      Destroy (wheelPiecePrefab) ;
   }

   private void DrawPiece (int index) {
      WheelPiece piece = wheelPieces [ index ] ;
      Transform pieceTrns = InstantiatePiece ().transform.GetChild (0) ;

      pieceTrns.GetChild(0).GetComponent<Image>().sprite = piece.Icon ;
      pieceTrns.GetChild(1).GetComponent<Text>().text = piece.Label ;
      if(piece.Amount == 10 || piece.Amount == 50 || piece.Amount == 100)
         pieceTrns.GetChild(2).GetComponent<Text>().text = piece.Amount + "K";
      else
         pieceTrns.GetChild(2).GetComponent<Text>().text = piece.Amount.ToString("N0");

      Transform lineTrns = Instantiate (linePrefab, linesParent.position, Quaternion.identity, linesParent).transform ;
      lineTrns.RotateAround (wheelPiecesParent.position, Vector3.back, (pieceAngle * index) + halfPieceAngle) ;

      pieceTrns.RotateAround (wheelPiecesParent.position, Vector3.back, pieceAngle * index) ;
   }

   private GameObject InstantiatePiece () {
      return Instantiate (wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent) ;
   }
}