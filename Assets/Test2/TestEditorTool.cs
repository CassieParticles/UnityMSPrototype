using UnityEditor;
using UnityEditor.EditorTools;

namespace Test2
{
    [EditorTool("Example",typeof(TestComponent))]
    public class TestEditorTool: EditorTool
    {
        public override void OnActivated()
        {
            ((TestComponent)target).testObj.a = 3;
            ((TestComponent)target).testObj.b = 4;
            ((TestComponent)target).testObj.c = 5;
            
            Undo.RecordObject((TestComponent)target, "Changed values");
        }
    }
}