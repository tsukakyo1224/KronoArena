using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif
/// <summary>
/// 見えないGraphic(uGUIでボタンの当たり判定を大きくする用)
/// </summary>

public class GraphicCast : Graphic
{
    //頂点を生成する必要があるときのコールバック関数
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);
        vh.Clear();//頂点を全てクリアし、何も表示されないように
    }
    //表示する必要がないので、インスペクターに何も表示しないように
    #if UNITY_EDITOR
    [CustomEditor(typeof(GraphicCast))]
    class GraphicCastEditor : Editor
    {
        public override void OnInspectorGUI()
        {

        }
    }
    #endif
}
