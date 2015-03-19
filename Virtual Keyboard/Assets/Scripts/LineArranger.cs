using UnityEngine;
using System.Collections;

public class LineArranger : MonoBehaviour {

	public enum LineOrientation {xAxis, yAxis, zAxis};

	public LineOrientation orientation = LineOrientation.xAxis;
	public Vector3 baseLocalPosition = Vector3.zero;
	public float itemSpacing = 1.0f;

	[ContextMenu("Arrange Children As Line")]
	void ArrangeAsChildren() {
		int childCount = transform.childCount;
		for( int i = 0; i < childCount; i++ ) {
			GameObject child = transform.GetChild(i).gameObject;
			switch(orientation) {
				case LineOrientation.xAxis:
					child.transform.localPosition = new Vector3( baseLocalPosition.x + i*(itemSpacing), 
				                                            baseLocalPosition.y, 
				                                            baseLocalPosition.z);
					break;
				case LineOrientation.yAxis: 
					child.transform.localPosition = new Vector3( baseLocalPosition.x, 
				                                            baseLocalPosition.y + i*(itemSpacing), 
				                                            baseLocalPosition.z);
				break;
				case LineOrientation.zAxis: 
					child.transform.localPosition = new Vector3( baseLocalPosition.x, 
				                                            baseLocalPosition.y, 
				                                            baseLocalPosition.z + i*(itemSpacing));
				break;
			}
		}
	}
}
