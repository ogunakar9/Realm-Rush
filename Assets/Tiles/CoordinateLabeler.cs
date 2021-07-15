using TMPro;
using UnityEngine;

    [ExecuteAlways]
    public class CoordinateLabeler : MonoBehaviour
    {
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color blockedColor = Color.grey;
        
        TextMeshPro label;
        Vector2Int coordinates = new Vector2Int();
        Waypoint waypoint;
        void Awake()
        {
            label = GetComponent<TextMeshPro>();
            label.enabled = false;
            
            waypoint = GetComponentInParent<Waypoint>();
            DisplayCoordinates();
        }

        void Update()
        {
            if (!Application.isPlaying)
            {
                DisplayCoordinates();
                UpdateObjectName();
            }

            ColorCoordinates();
            ToggleLabels();
        }

        void ToggleLabels()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                label.enabled = !label.IsActive();
            }
        }
        
        void ColorCoordinates()
        {
            label.color = waypoint.IsPlaceable ? defaultColor : blockedColor;
        }

        void DisplayCoordinates()
        {
            Vector3 parentPos = transform.parent.position;
            coordinates.x = Mathf.RoundToInt(parentPos.x / UnityEditor.EditorSnapSettings.move.x);
            coordinates.y = Mathf.RoundToInt(parentPos.z / UnityEditor.EditorSnapSettings.move.z);
        
            label.text = coordinates.x + "," + coordinates.y;
        }

        void UpdateObjectName()
        {
            transform.parent.name = coordinates.ToString();
        }
    }
