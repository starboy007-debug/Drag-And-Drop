using UnityEngine;

public class DragAndDropFactory : MonoBehaviour
{
    public GameObject draggablePrefab;
    public GameObject dropZonePrefab;

    // This method creates a draggable item
    public Draggable CreateDraggable(Vector3 position, Transform parent)
    {
        GameObject draggableInstance = Instantiate(draggablePrefab, position, Quaternion.identity, parent);
        Draggable draggableComponent = draggableInstance.GetComponent<Draggable>();
        if (draggableComponent == null)
        {
            Debug.LogError("Draggable component missing from prefab!");
        }
        return draggableComponent;
    }

    // This method creates a drop zone
    public Dropzone CreateDropzone(Vector3 position, Transform parent)
    {
        GameObject dropzoneInstance = Instantiate(dropZonePrefab, position, Quaternion.identity, parent);
        Dropzone dropzoneComponent = dropzoneInstance.GetComponent<Dropzone>();
        if (dropzoneComponent == null)
        {
            Debug.LogError("Dropzone component missing from prefab!");
        }
        return dropzoneComponent;
    }
}
