using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base class for any gameobject logic. A Design is the equivalent of an MVC's Model.
/// </summary>
public class Design : MonoBehaviour
{
    [Header("Prefabs")]
    public View mainView;

    [Header("Events")]
    [HideInInspector]
    public DesignEvent onValueChange = new DesignEvent();
    [HideInInspector]
    public DesignEvent onUIValueChange = new DesignEvent();
    [HideInInspector]
    public DesignEvent onWorldValueChange = new DesignEvent();
    [HideInInspector]
    public DesignEvent onDestroy = new DesignEvent();

    public virtual void Awake() { }
    public virtual void Start() { }
    public virtual void Update() { }

    /// <summary>
    /// Instantiates the given viewPrefab and initializes it using this Design.
    /// </summary>
    /// <param name="viewPrefab">The prefab to instantiate</param>
    /// <param name="view">Exposes the instantiating view to the calling scope</param>
    /// <param name="parent">The parent transform for the newly created view.</param>
    /// <returns>bool</returns>
    public virtual bool InstantiateView(View viewPrefab, out View view, Transform parent = null)
    {
        if (viewPrefab == null)
        {
            throw new ViewException("Tried to instantiate a view for " + name + " which was supplied as null!");
        }
        if (!View.GetView(Instantiate(viewPrefab.gameObject, parent), out view))
            return false;
        view.Init(this, true);
        return true;
    }

    /// <summary>
    /// Instantiates the given viewPrefab and initializes it using this Design.
    /// </summary>
    /// <param name="viewPrefab">The prefab to instantiate</param>
    /// <param name="parent">The parent transform for the newly created view.</param>
    /// <returns>bool</returns>
    public virtual bool InstantiateView(View viewPrefab, Transform parent = null)
    {
        if (viewPrefab == null)
        {
            throw new ViewException("Tried to instantiate a view for " + name + " which was supplied as null!");
        }
        if (!View.GetView(Instantiate(viewPrefab.gameObject, parent), out View view))
            return false;
        view.Init(this, true);
        return true;
    }

    /// <summary>
    /// Shorthand, and example case to instantiate the designs main view, which could be any view, ui, world, or otherwise
    /// </summary>
    /// <param name="view">Exposes the instance of the created view to the calling scope</param>
    /// <param name="parent">The parent transform for the newly created view.</param>
    /// <returns></returns>
    public virtual bool InstantiateMainView(out View view, Transform parent = null)
    {
        if (!InstantiateView(mainView, out view, parent))
            return false;
        return true;
    }

    /// <summary>
    /// Shorthand, and example case to instantiate the designs main view, which could be any view, ui, world, or otherwise
    /// </summary>
    /// <param name="parent">The parent transform for the newly created view.</param>
    /// <returns></returns>
    public virtual bool InstantiateMainView(Transform parent = null)
    {
        if (!InstantiateView(mainView, parent))
            return false;
        return true;
    }

    /// <summary>
    /// Updates all Views related to this design by calling their render method.
    /// </summary>
    public virtual void UpdateViews()
    {
        onValueChange.Invoke(this);
    }

    /// <summary>
    /// Updates all ViewUI related to this design by calling their render method.
    /// </summary>
    public virtual void UpdateUiViews()
    {
        onUIValueChange.Invoke(this);
    }

    /// <summary>
    /// Updates all ViewWorlds related to this design by calling their render method.
    /// </summary>
    public virtual void UpdateWorldViews()
    {
        onWorldValueChange.Invoke(this);
    }

    /// <summary>
    /// Calls OnDesignDestroy on all Views related to this design. By default this destroys the views
    /// </summary>
    protected virtual void OnDestroy()
    {
        onDestroy.Invoke(this);
    }

    /// <summary>
    /// Static getter to get a Design, if available from any gameobject, or otherwise return false
    /// </summary>
    /// <param name="go">The gameobject to check for any Design component</param>
    /// <param name="o">Exposes the gameobjects Design to the calling scope</param>
    /// <returns>bool</returns>
    public static bool GetDesign(GameObject go, out Design o)
    {
        o = go.GetComponent<Design>();
        if (o == null)
            return false;
        return true;
    }

    /// <summary>
    /// Called when a non-instantiated View finds this Design and initializes itself with it.
    /// Interface method for extending classes, no default behavious
    /// </summary>
    /// <param name="view">The View which found, and uses this Design</param>
    public virtual void OnFoundByView(View view) { }
}

/// <summary>
/// Event class for all Design related events. Takes a Design as its only parameter
/// </summary>
[System.Serializable]
public class DesignEvent : UnityEvent<Design> { }