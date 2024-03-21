using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsibility is to record every object in a level
/// so that I know who to destroy when a level exits.
/// </summary>
public class Map
{
    /*********************************** Fields ***********************************/
    public Hero hero;
    public List<Enemy> enemies;
    public List<Obstacle> obstacles;

    /*********************************** Settings ***********************************/
    public static readonly float mapMinX = 0.0f; 
    public static readonly float mapMaxX = 23.0f; 
    public static readonly float mapMinY = 0.0f; 
    public static readonly float mapMaxY = 23.0f; 
    
    /*********************************** Ctor ***********************************/
    public Map()
    {
        hero = null;
        enemies = new List<Enemy>();
        obstacles = new List<Obstacle>();
    }

    /*********************************** Methods ***********************************/
    /// <summary>
    /// Destroys all objects on the map.
    /// </summary>
    public void clear()
    {
        if (hero != null)
        {
            hero.destroy();
            hero = null;
        }

        foreach(var e in enemies)
        {
            if (e != null) e.destroy();
        }
        enemies.Clear();

        foreach (var o in obstacles)
        {
            if (o != null) o.destroy();
        }
        obstacles.Clear();
    }

    /// <summary>
    /// Sets the hero in the map
    /// </summary>
    /// <param name="h">the hero</param>
    /// <exception cref="InvalidOperationException">if hero is already != null</exception>
    public void addHero(Hero h)
    {
        Debug.Assert(h != null);

        if (hero != null)
        {
            throw new InvalidOperationException();
        }
        hero = h;
    }

    public void addEnemy(Enemy e)
    {
        Debug.Assert(e != null);
        enemies.Add(e);
    }

    public void addObstacle(Obstacle o)
    {
        Debug.Assert(o != null);
        obstacles.Add(o);
    }
}
