using System.Collections.Generic;
using Engine;
using Engine.Entity;
using Engine.Rendering;

public static class GameObjectManager
{
    private static List<GameObject> gameObjects;
    public static List<IRenderable> Renderables;
    public static List<IUpdatable> Updatables;
    public static List<ILoadable> Loadables;
    public static List<IPickable> Pickables;

    static GameObjectManager()
    {
        gameObjects = new List<GameObject>();
        Renderables = new List<IRenderable>();
        Updatables = new List<IUpdatable>();
        Loadables = new List<ILoadable>();
        Pickables = new List<IPickable>();
    }
    public static void Add(GameObject gameObject)
    {
        gameObjects.Add(gameObject);

        if (gameObject is IRenderable)
        {
            IRenderable renderable = (IRenderable)gameObject;
            Renderables.Add(renderable);
        }
        if (gameObject is IUpdatable)
        {
            IUpdatable updatable = (IUpdatable)gameObject;
            Updatables.Add(updatable);
        }

        if (gameObject is ILoadable)
        {
            ILoadable loadable = (ILoadable)gameObject;  
            Loadables.Add(loadable);
        }

        if (gameObject is IPickable)
        {
            IPickable pickable = (IPickable)gameObject;
            Pickables.Add(pickable);
        }
    }

    public static void Remove(GameObject gameObject)
    {
        gameObjects.Remove(gameObject);

        if (gameObject is IRenderable)
        {
            IRenderable renderable = (IRenderable)gameObject;
            Renderables.Remove(renderable);
        }
        if (gameObject is IUpdatable)
        {
            IUpdatable updatable = (IUpdatable)gameObject;
            Updatables.Remove(updatable);
        }

        if (gameObject is ILoadable)
        {
            ILoadable loadable = (ILoadable)gameObject;  
            Loadables.Add(loadable);
        }

        if (gameObject is IPickable)
        {
            IPickable pickable = (IPickable)gameObject;
            Pickables.Remove(pickable);
        }
        
    }
}